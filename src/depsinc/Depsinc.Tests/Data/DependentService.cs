namespace Depsinc.Tests.Data;

public class DependentService(
    IMySingletonService singletonService,
    IMyScopedService scopedService,
    IMyTransientService transientService)
    : IDependentService
{
    public IMySingletonService SingletonService { get; } = singletonService;
    public IMyScopedService ScopedService { get; } = scopedService;
    public IMyTransientService TransientService { get; } = transientService;
}