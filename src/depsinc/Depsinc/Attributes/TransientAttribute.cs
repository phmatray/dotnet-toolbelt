namespace Depsinc;

/// <summary>
/// Indicates that a service should be registered with a transient lifetime.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class TransientAttribute : Attribute;