using System;

namespace DependencyInjection
{
    public interface IContainer
    {
        void Register<TRegisteredType, TConcrete>();
        void Register<TRegisteredType, TConcrete>(LifecycleType lifecycleType);
        TResolve Resolve<TResolve>();
        object Resolve(Type resolveType);
        RegisteredObjectInfo Registered<TResolve>();
    }
}



