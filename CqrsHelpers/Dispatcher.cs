using CqrsHelpers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsHelpers;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public async Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
        
        var handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for command type {commandType.Name}");
        }

        var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand>.HandleAsync));
        if (handleMethod == null)
        {
            throw new InvalidOperationException($"HandleAsync method not found on handler type {handlerType.Name}");
        }

        await (Task)handleMethod.Invoke(handler, new object[] { command, cancellationToken })!;
    }

    public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var commandType = command.GetType();
        var behaviors = _serviceProvider.GetServices<IPipelineBehavior<ICommand<TResult>, TResult>>().Reverse().ToList();
        
        async Task<TResult> Handler()
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResult));
            
            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException($"No handler registered for command type {commandType.Name}");
            }

            var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync));
            if (handleMethod == null)
            {
                throw new InvalidOperationException($"HandleAsync method not found on handler type {handlerType.Name}");
            }

            return await (Task<TResult>)handleMethod.Invoke(handler, new object[] { command, cancellationToken })!;
        }

        return await behaviors
            .Aggregate((RequestHandlerDelegate<TResult>)Handler, (next, behavior) => () => behavior.HandleAsync(command, next, cancellationToken))
            .Invoke();
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        if (query == null) throw new ArgumentNullException(nameof(query));

        var queryType = query.GetType();
        var behaviors = _serviceProvider.GetServices<IPipelineBehavior<IQuery<TResult>, TResult>>().Reverse().ToList();
        
        async Task<TResult> Handler()
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
            
            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException($"No handler registered for query type {queryType.Name}");
            }

            var handleMethod = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));
            if (handleMethod == null)
            {
                throw new InvalidOperationException($"HandleAsync method not found on handler type {handlerType.Name}");
            }

            return await (Task<TResult>)handleMethod.Invoke(handler, new object[] { query, cancellationToken })!;
        }

        return await behaviors
            .Aggregate((RequestHandlerDelegate<TResult>)Handler, (next, behavior) => () => behavior.HandleAsync(query, next, cancellationToken))
            .Invoke();
    }
}