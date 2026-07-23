using CqrsHelpers.Abstractions;
using CqrsHelpers.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace CqrsHelpers.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddCqrsHelpers_ShouldRegisterDispatcher()
    {
        var services = new ServiceCollection();
        
        services.AddCqrsHelpers();
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetService<IDispatcher>();
        
        dispatcher.ShouldNotBeNull();
        dispatcher.ShouldBeOfType<Dispatcher>();
    }
    
    [Fact]
    public void AddCqrsHelpers_ShouldRegisterHandlersFromAssembly()
    {
        var services = new ServiceCollection();
        
        services.AddCqrsHelpers(typeof(ServiceCollectionExtensionsTests).Assembly);
        
        var serviceProvider = services.BuildServiceProvider();
        
        var commandHandler = serviceProvider.GetService<ICommandHandler<TestCommand>>();
        commandHandler.ShouldNotBeNull();
        commandHandler.ShouldBeOfType<TestCommandHandler>();
        
        var queryHandler = serviceProvider.GetService<IQueryHandler<TestQuery, string>>();
        queryHandler.ShouldNotBeNull();
        queryHandler.ShouldBeOfType<TestQueryHandler>();
    }
    
    [Fact]
    public void AddCqrsHelpers_ShouldRegisterPipelineBehaviors()
    {
        var services = new ServiceCollection();
        
        services.AddCqrsHelpers(typeof(ServiceCollectionExtensionsTests).Assembly);
        
        var serviceProvider = services.BuildServiceProvider();
        
        var behaviors = serviceProvider.GetServices<IPipelineBehavior<ICommand<string>, string>>();
        behaviors.ShouldNotBeEmpty();
        behaviors.Any(b => b.GetType() == typeof(TestPipelineBehavior)).ShouldBeTrue();
    }
    
    [Fact]
    public void AddCqrsHelpers_WithNoAssemblies_ShouldUseCallingAssembly()
    {
        var services = new ServiceCollection();
        
        services.AddCqrsHelpers();
        
        var serviceProvider = services.BuildServiceProvider();
        
        var commandHandler = serviceProvider.GetService<ICommandHandler<TestCommand>>();
        commandHandler.ShouldNotBeNull();
    }
    
    public class TestCommand : ICommand { }
    
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public Task HandleAsync(TestCommand command, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
    
    public class TestQuery : IQuery<string> { }
    
    public class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
        public Task<string> HandleAsync(TestQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult("test result");
        }
    }
    
    public class TestPipelineBehavior : IPipelineBehavior<ICommand<string>, string>
    {
        public async Task<string> HandleAsync(ICommand<string> request, RequestHandlerDelegate<string> next, CancellationToken cancellationToken = default)
        {
            return await next();
        }
    }
}