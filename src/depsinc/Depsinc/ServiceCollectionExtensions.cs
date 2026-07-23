using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Depsinc;

/// <summary>
/// Provides extension methods for registering services with dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers services from the specified assembly that have the Singleton, Scoped, or Transient attributes.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="assembly">The assembly to scan for services.</param>
    public static void AddServicesFromAssembly(
        this IServiceCollection services, Assembly assembly)
    {
        var typesWithAttributes = assembly.GetTypes()
            .Where(t => t.GetCustomAttributes<SingletonAttribute>().Any() ||
                        t.GetCustomAttributes<ScopedAttribute>().Any() ||
                        t.GetCustomAttributes<TransientAttribute>().Any())
            .ToList();

        RegisterServices(services, typesWithAttributes);
    }
    
    /// <summary>
    /// Registers services from the specified assembly that implement the given interface
    /// and have the Singleton, Scoped, or Transient attributes.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="assembly">The assembly to scan for services.</param>
    /// <param name="conditionInterface">The interface that services must implement to be registered.</param>
    public static void AddConditionalServicesFromAssembly(
        this IServiceCollection services, Assembly assembly, Type conditionInterface)
    {
        var typesWithAttributes = assembly.GetTypes()
            .Where(t => t.GetCustomAttributes<SingletonAttribute>().Any() ||
                        t.GetCustomAttributes<ScopedAttribute>().Any() ||
                        t.GetCustomAttributes<TransientAttribute>().Any())
            .Where(conditionInterface.IsAssignableFrom)
            .ToList();

        RegisterServices(services, typesWithAttributes, conditionInterface);
    }
    
    private static void RegisterServices(IServiceCollection services, List<Type> typesWithAttributes, Type? conditionInterface = null)
    {
        foreach (var type in typesWithAttributes)
        {
            var interfaces = type.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (conditionInterface == null || conditionInterface.IsAssignableFrom(@interface))
                {
                    if (type.GetCustomAttribute<SingletonAttribute>() != null)
                    {
                        services.AddSingleton(@interface, type);
                    }
                    else if (type.GetCustomAttribute<ScopedAttribute>() != null)
                    {
                        services.AddScoped(@interface, type);
                    }
                    else if (type.GetCustomAttribute<TransientAttribute>() != null)
                    {
                        services.AddTransient(@interface, type);
                    }
                }
            }
        }
    }
}