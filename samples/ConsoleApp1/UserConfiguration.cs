using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp1;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    // we can use an EntityTypeConfiguration,
    // BUT we don't have to (or shouldn't) worry about temporal config here.

    builder.ToTable("MyWeirdlyNamedUsersTable");
    builder.HasKey(u => u.UserId);
  }
}