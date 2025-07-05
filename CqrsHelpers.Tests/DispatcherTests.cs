using CqrsHelpers.Abstractions;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace CqrsHelpers.Tests;

public class DispatcherTests
{
    [Fact]
    public async Task SendAsync_WithCommand_ShouldCallHandler()
    {
        var services = new ServiceCollection();
        var command = new TestCommand();
        var handler = A.Fake<ICommandHandler<TestCommand>>();
        
        services.AddSingleton(handler);
        services.AddScoped<IDispatcher, Dispatcher>();
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        
        await dispatcher.SendAsync(command);
        
        A.CallTo(() => handler.HandleAsync(command, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task SendAsync_WithCommandReturningResult_ShouldReturnResult()
    {
        var services = new ServiceCollection();
        var command = new TestCommandWithResult();
        var handler = A.Fake<ICommandHandler<TestCommandWithResult, string>>();
        var expectedResult = "test result";
        
        A.CallTo(() => handler.HandleAsync(command, A<CancellationToken>._))
            .Returns(Task.FromResult(expectedResult));
        
        services.AddSingleton(handler);
        services.AddScoped<IDispatcher, Dispatcher>();
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        
        var result = await dispatcher.SendAsync(command);
        
        result.ShouldBe(expectedResult);
    }
    
    [Fact]
    public async Task QueryAsync_ShouldReturnQueryResult()
    {
        var services = new ServiceCollection();
        var query = new TestQuery();
        var handler = A.Fake<IQueryHandler<TestQuery, string>>();
        var expectedResult = "query result";
        
        A.CallTo(() => handler.HandleAsync(query, A<CancellationToken>._))
            .Returns(Task.FromResult(expectedResult));
        
        services.AddSingleton(handler);
        services.AddScoped<IDispatcher, Dispatcher>();
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        
        var result = await dispatcher.QueryAsync(query);
        
        result.ShouldBe(expectedResult);
    }
    
    [Fact]
    public async Task SendAsync_WithNullCommand_ShouldThrowArgumentNullException()
    {
        var services = new ServiceCollection();
        services.AddScoped<IDispatcher, Dispatcher>();
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        
        await Should.ThrowAsync<ArgumentNullException>(async () => 
            await dispatcher.SendAsync(null!));
    }
    
    [Fact]
    public async Task SendAsync_WithNoHandlerRegistered_ShouldThrowInvalidOperationException()
    {
        var services = new ServiceCollection();
        services.AddScoped<IDispatcher, Dispatcher>();
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        var command = new TestCommand();
        
        var exception = await Should.ThrowAsync<InvalidOperationException>(async () => 
            await dispatcher.SendAsync(command));
        
        exception.Message.ShouldContain("No handler registered for command type TestCommand");
    }
    
    [Fact]
    public async Task SendAsync_WithPipelineBehavior_ShouldExecuteBehaviorBeforeHandler()
    {
        var services = new ServiceCollection();
        var command = new TestCommandWithResult();
        var handler = A.Fake<ICommandHandler<TestCommandWithResult, string>>();
        var behavior = A.Fake<IPipelineBehavior<ICommand<string>, string>>();
        var callOrder = new List<string>();
        
        A.CallTo(() => behavior.HandleAsync(command, A<RequestHandlerDelegate<string>>._, A<CancellationToken>._))
            .ReturnsLazily(async call =>
            {
                callOrder.Add("behavior");
                var next = call.GetArgument<RequestHandlerDelegate<string>>(1);
                return await next();
            });
        
        A.CallTo(() => handler.HandleAsync(command, A<CancellationToken>._))
            .ReturnsLazily(() =>
            {
                callOrder.Add("handler");
                return Task.FromResult("result");
            });
        
        services.AddSingleton(handler);
        services.AddSingleton(behavior);
        services.AddScoped<IDispatcher, Dispatcher>();
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        
        await dispatcher.SendAsync(command);
        
        callOrder.ShouldBe(new[] { "behavior", "handler" });
    }
    
    public class TestCommand : ICommand { }
    
    public class TestCommandWithResult : ICommand<string> { }
    
    public class TestQuery : IQuery<string> { }
}