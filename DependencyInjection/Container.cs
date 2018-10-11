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
            if (!RegisteredObjects.ContainsType(typeof(TResolve)))
                throw new MissingRegistryException($"Type {typeof(TResolve).Name} not registered.");

            return (TResolve)RegisteredObjects.ResolveObject(typeof(TResolve));
        }

        public RegisteredObjectInfo Registered<TResolve>()
        {
            return RegisteredObjects.GetRegisteredObjectInfo(typeof(TResolve));
        }
    }
}



