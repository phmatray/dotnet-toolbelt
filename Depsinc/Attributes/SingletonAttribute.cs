namespace Depsinc;

/// <summary>
/// Indicates that a service should be registered with a singleton lifetime.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class SingletonAttribute : Attribute;