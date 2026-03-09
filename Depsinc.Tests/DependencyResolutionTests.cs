using Depsinc.Tests.Data;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;

namespace Depsinc.Tests;

public class DependencyResolutionTests
{
    [Fact]
    public void Test_DependentServiceResolution()
    {
        var services = new ServiceCollection();

        // Register the dependent service
        services.AddServicesFromAssembly(typeof(DependentService).Assembly);
        services.AddTransient<IDependentService, DependentService>();

        var serviceProvider = services.BuildServiceProvider();

        var dependentService = serviceProvider.GetService<IDependentService>();

        dependentService.ShouldNotBeNull();
        if (dependentService == null) return;
        dependentService.SingletonService.ShouldNotBeNull();
        dependentService.ScopedService.ShouldNotBeNull();
        dependentService.TransientService.ShouldNotBeNull();
    }
}