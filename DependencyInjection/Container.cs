using System;

namespace DependencyInjection
{
    public class Container : IContainer
    {
        protected IRegisteredObjectLookup RegisteredObjects { get; }

        public Container()
        {
            RegisteredObjects = new RegisteredObjectLookup();
        }
        public void Register<TRegisteredType, TConcrete>()
        {
            Register<TRegisteredType, TConcrete>(LifecycleType.Transient);
        }

        public void Register<TResolve, TConcrete>(LifecycleType lifecycleType)
        {
            RegisteredObjects.AddObject(typeof(TResolve), typeof(TConcrete), lifecycleType);
        }

        public TResolve Resolve<TResolve>()
        {
            return (TResolve)Resolve(typeof(TResolve));
        }

        public object Resolve(Type resolveType)
        {
            if (!RegisteredObjects.ContainsType(resolveType))
                throw new MissingRegistryException($"Type {resolveType.Name} not registered.");

            return RegisteredObjects.ResolveObject(resolveType);
        }

        public RegisteredObjectInfo Registered<TResolve>()
        {
            return RegisteredObjects.GetRegisteredObjectInfo(typeof(TResolve));
        }
    }
}



