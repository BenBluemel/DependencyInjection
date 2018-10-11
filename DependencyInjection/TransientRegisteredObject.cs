using System;

namespace DependencyInjection
{
    public class TransientRegisteredObject : RegisteredObject
    {
        public TransientRegisteredObject(Type registeredType, Type concreteType, LifecycleType lifecycleType, RegisteredObjectLookup registry)
            : base(registeredType, concreteType, lifecycleType, registry)
        {
        }

        public override object GetInstance()
        {
            return CreateInstance(ConcreteType);
        }
    }
}



