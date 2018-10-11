using System;

namespace DependencyInjection
{
    public class RegisteredObjectInfo
    {
        public Type RegisteredType { get; }
        public Type ConcreteType { get; }
        public LifecycleType LifecycleType { get; }

        private RegisteredObjectInfo()
        {
        }

        public RegisteredObjectInfo(Type registeredType, Type concreteType, LifecycleType lifecycleType)
        {
            RegisteredType = registeredType;
            ConcreteType = concreteType;
            LifecycleType = lifecycleType;
        }
    }
}



