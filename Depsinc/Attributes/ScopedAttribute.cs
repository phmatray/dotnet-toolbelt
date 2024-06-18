namespace Depsinc;

/// <summary>
/// Indicates that a service should be registered with a scoped lifetime.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ScopedAttribute : Attribute;