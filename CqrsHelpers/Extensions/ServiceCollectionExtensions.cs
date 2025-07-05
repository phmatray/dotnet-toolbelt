using System.Reflection;
using CqrsHelpers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsHelpers.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCqrsHelpers(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
        {
            assemblies = new[] { Assembly.GetCallingAssembly() };
        }

        services.AddScoped<IDispatcher, Dispatcher>();

        foreach (var assembly in assemblies)
        {
            RegisterHandlers(services, assembly);
        }

        return services;
    }

    private static void RegisterHandlers(IServiceCollection services, Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition)
            .ToList();

        foreach (var handlerType in handlerTypes)
        {
            var interfaces = handlerType.GetInterfaces()
                .Where(i => i.IsGenericType)
                .ToList();

            foreach (var @interface in interfaces)
            {
                var genericTypeDefinition = @interface.GetGenericTypeDefinition();
                
                if (genericTypeDefinition == typeof(ICommandHandler<>) ||
                    genericTypeDefinition == typeof(ICommandHandler<,>) ||
                    genericTypeDefinition == typeof(IQueryHandler<,>) ||
                    genericTypeDefinition == typeof(IPipelineBehavior<,>))
                {
                    services.AddScoped(@interface, handlerType);
                }
            }
        }
    }
}