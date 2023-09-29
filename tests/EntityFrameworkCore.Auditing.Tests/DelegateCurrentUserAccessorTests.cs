namespace burtonrodman.EntityFrameworkCore.Auditing;

public class DelegateCurrentUserAccessorTests
{
    [Fact]
    public void FuncStringConstructorThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => {
            var accessor = new DelegateCurrentUserAccessor((null as Func<string>)!);
        });
    }

    [Fact]
    public void FuncServiceProviderStringConstructorThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => {
            var accessor = new DelegateCurrentUserAccessor((null as Func<IServiceProvider, string>)!, (null as IServiceProvider)!);
        });
    }

    [Fact]
    public void FuncStringReturnsValue() 
    {
        var accessor = new DelegateCurrentUserAccessor(() => "unit.test");

        Assert.Equal("unit.test", accessor.GetUserName());
    }

    [Fact]
    public void FuncServiceProviderStringReturnsValue()
    {
        var serviceProvider = new MockServiceProvider();
        var accessor = new DelegateCurrentUserAccessor((sp) => {
            var thingy = sp.GetService(typeof(UnitTestContextyServiceThingy)) as UnitTestContextyServiceThingy;
            return thingy!.UserName;
        }, serviceProvider);
        
        Assert.Equal("from.the.service", accessor.GetUserName());
    }
}

internal class MockServiceProvider : IServiceProvider
{
    public object? GetService(Type serviceType)
    {
        return new UnitTestContextyServiceThingy();
    }
}

internal class UnitTestContextyServiceThingy
{
    public string UserName => "from.the.service";
}