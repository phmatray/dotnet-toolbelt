using CqrsHelpers.Abstractions;
using CqrsHelpers.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace CqrsHelpers.Tests;

public class IntegrationTests
{
    [Fact]
    public async Task FullPipeline_WithLoggingBehavior_ShouldWork()
    {
        var services = new ServiceCollection();
        var logs = new List<string>();
        
        services.AddSingleton(logs);
        services.AddCqrsHelpers(typeof(IntegrationTests).Assembly);
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        
        var command = new CreateUserCommand { Name = "John Doe" };
        var result = await dispatcher.SendAsync(command);
        
        result.ShouldBe(123);
        logs.ShouldContain("Before handling CreateUserCommand");
        logs.ShouldContain("After handling CreateUserCommand");
    }
    
    [Fact]
    public async Task QueryWithCaching_ShouldReturnCachedResult()
    {
        var services = new ServiceCollection();
        var cache = new Dictionary<string, object>();
        
        services.AddSingleton(cache);
        services.AddCqrsHelpers(typeof(IntegrationTests).Assembly);
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        
        var query = new GetUserQuery { Id = 1 };
        
        var result1 = await dispatcher.QueryAsync(query);
        var result2 = await dispatcher.QueryAsync(query);
        
        result1.ShouldBe(result2);
        cache.Count.ShouldBe(1);
    }
    
    public record CreateUserCommand : ICommand<int>
    {
        public required string Name { get; init; }
    }
    
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, int>
    {
        public Task<int> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(123);
        }
    }
    
    public record GetUserQuery : IQuery<User>
    {
        public required int Id { get; init; }
    }
    
    public record User(int Id, string Name);
    
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
    {
        public Task<User> HandleAsync(GetUserQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new User(query.Id, "John Doe"));
        }
    }
    
    public class CommandLoggingBehavior : IPipelineBehavior<ICommand<int>, int>
    {
        private readonly List<string> _logs;
        
        public CommandLoggingBehavior(List<string> logs)
        {
            _logs = logs;
        }
        
        public async Task<int> HandleAsync(ICommand<int> request, RequestHandlerDelegate<int> next, CancellationToken cancellationToken = default)
        {
            _logs.Add($"Before handling {request.GetType().Name}");
            var result = await next();
            _logs.Add($"After handling {request.GetType().Name}");
            return result;
        }
    }
    
    public class QueryCachingBehavior : IPipelineBehavior<IQuery<User>, User>
    {
        private readonly Dictionary<string, object> _cache;
        
        public QueryCachingBehavior(Dictionary<string, object> cache)
        {
            _cache = cache;
        }
        
        public async Task<User> HandleAsync(IQuery<User> request, RequestHandlerDelegate<User> next, CancellationToken cancellationToken = default)
        {
            var key = $"{request.GetType().Name}:{request.GetHashCode()}";
            
            if (_cache.TryGetValue(key, out var cached))
            {
                return (User)cached;
            }
            
            var result = await next();
            _cache[key] = result!;
            return result;
        }
    }
}