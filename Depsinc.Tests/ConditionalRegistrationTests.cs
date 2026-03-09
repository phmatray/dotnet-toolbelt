using Shouldly;
using Microsoft.Extensions.DependencyInjection;

namespace Depsinc.Tests;

public class ConditionalRegistrationTests
{
    [Fact]
    public void Test_ConditionalServiceRegistration()
    {
        var services = new ServiceCollection();

        // Register services conditionally based on IConditionalService interface
        services.AddConditionalServicesFromAssembly(
            typeof(ConditionalSingletonService).Assembly,
            typeof(IConditionalService)
        );

        var serviceProvider = services.BuildServiceProvider();

        // Services implementing IConditionalService should be registered
        serviceProvider.GetService<IConditionalService>().ShouldNotBeNull();

        // Services not implementing IConditionalService should not be registered
        serviceProvider.GetService<NonConditionalService>().ShouldBeNull();
    }
}

// Sample interface for conditional registration
public interface IConditionalService;

[Singleton]
public class ConditionalSingletonService : IConditionalService;

[Scoped]
public class ConditionalScopedService : IConditionalService;

[Transient]
public class ConditionalTransientService : IConditionalService;

[Singleton]
public class NonConditionalService;