namespace DependencyInjection
{
    public interface IContainer
    {
        void Register<TRegisteredType, TConcrete>();
        void Register<TRegisteredType, TConcrete>(LifecycleType lifecycleType);
        TResolve Resolve<TResolve>();
        RegisteredObjectInfo Registered<TResolve>();
    }
}



