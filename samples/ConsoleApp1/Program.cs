using ConsoleApp1;
using Microsoft.EntityFrameworkCore;

var optionsBuilder = new DbContextOptionsBuilder<SampleDbContext>();
optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=ConsoleApp1Db;Trusted_Connection=true;");

var currentUserAccessor = new CurrentUserAccessor();

using (var context = new SampleDbContext(optionsBuilder.Options, currentUserAccessor))
{
    context.BlogPosts.Add(new BlogPost() { Title = "New Blog Post", Body = "Hello, World!" });
    context.SaveChanges();
}

using (var context = new SampleDbContext(optionsBuilder.Options, currentUserAccessor))
{
    var post = context.BlogPosts
        .Select(p => new BlogPost() {
            Title = p.Title,
            Body = p.Body,
            ModifiedBy = p.ModifiedBy,
            LastModifiedDate = EF.Property<DateTime>(p, AuditingEntityBase.PeriodStart)
        })
        .FirstOrDefault();

    Console.WriteLine($"post was created by {post.ModifiedBy} on {post.LastModifiedDate}");
}


using (var context = new SampleDbContext(optionsBuilder.Options, currentUserAccessor))
{
    var post = context.BlogPosts.FirstOrDefault();
    post.Title = post.Title + " (modified)";
    context.SaveChanges();
}

using (var context = new SampleDbContext(optionsBuilder.Options, currentUserAccessor))
{
    var post = context.BlogPosts
        .Select(p => new BlogPost() {
            Title = p.Title,
            Body = p.Body,
            ModifiedBy = p.ModifiedBy,
            LastModifiedDate = EF.Property<DateTime>(p, AuditingEntityBase.PeriodStart)
        })
        .FirstOrDefault();

    Console.WriteLine($"post was created by {post.ModifiedBy} on {post.LastModifiedDate}");
}


using (var context = new SampleDbContext(optionsBuilder.Options, currentUserAccessor))
{
    var post = context.BlogPosts.FirstOrDefault();
    context.BlogPosts.Remove(post);
    context.SaveChanges();
}

using (var context = new SampleDbContext(optionsBuilder.Options, currentUserAccessor))
{
    var post = context.BlogPosts.TemporalAll()
        .Where(p => EF.Property<DateTime?>(p, AuditingEntityBase.PeriodEnd) != null)
        .Select(p => new BlogPost() {
            Title = p.Title,
            Body = p.Body,
            ModifiedBy = p.ModifiedBy,
            LastModifiedDate = EF.Property<DateTime>(p, AuditingEntityBase.PeriodStart),
            DeleteDate = EF.Property<DateTime>(p, AuditingEntityBase.PeriodEnd)
        })
        .FirstOrDefault();

    Console.WriteLine($"post was deleted by {post.ModifiedBy} on {post.DeleteDate}");
}


