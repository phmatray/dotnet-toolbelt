namespace Depsinc.Tests.Data;

public interface IDependentService
{
    IMySingletonService SingletonService { get; }
    IMyScopedService ScopedService { get; }
    IMyTransientService TransientService { get; }
}