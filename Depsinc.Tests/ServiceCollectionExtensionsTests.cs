using Depsinc.Tests.Data;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;

namespace Depsinc.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void Test_SingletonServiceRegistration()
    {
        var services = new ServiceCollection();
        services.AddServicesFromAssembly(typeof(MySingletonService).Assembly);

        var serviceProvider = services.BuildServiceProvider();
        var service1 = serviceProvider.GetService<IMySingletonService>();
        var service2 = serviceProvider.GetService<IMySingletonService>();

        service1.ShouldNotBeNull();
        service1.ShouldBeSameAs(service2); // Singleton services should be the same instance
    }

    [Fact]
    public void Test_ScopedServiceRegistration()
    {
        var services = new ServiceCollection();
        services.AddServicesFromAssembly(typeof(MyScopedService).Assembly);

        var serviceProvider = services.BuildServiceProvider();
        using var scope1 = serviceProvider.CreateScope();
        using var scope2 = serviceProvider.CreateScope();
        var service1 = scope1.ServiceProvider.GetService<IMyScopedService>();
        var service2 = scope1.ServiceProvider.GetService<IMyScopedService>();
        var service3 = scope2.ServiceProvider.GetService<IMyScopedService>();

        service1.ShouldNotBeNull();
        service3.ShouldNotBeNull();
        service1.ShouldBeSameAs(service2); // Scoped services should be the same within the same scope
        service1.ShouldNotBeSameAs(service3); // Scoped services should be different across scopes
    }

    [Fact]
    public void Test_TransientServiceRegistration()
    {
        var services = new ServiceCollection();
        services.AddServicesFromAssembly(typeof(MyTransientService).Assembly);

        var serviceProvider = services.BuildServiceProvider();
        var service1 = serviceProvider.GetService<IMyTransientService>();
        var service2 = serviceProvider.GetService<IMyTransientService>();

        service1.ShouldNotBeNull();
        service2.ShouldNotBeNull();
        service1.ShouldNotBeSameAs(service2); // Transient services should always be different instances
    }
}

// Sample service interfaces and implementations for testing