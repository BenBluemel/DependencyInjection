using System;
using System.Collections.Generic;

namespace DependencyInjection
{
    public class SingletonRegisteredObject : RegisteredObject
    {
        IDictionary<Type, object> RegisteredSingletons { get; set; }

        public SingletonRegisteredObject(Type registeredType, Type concreteType, LifecycleType lifecycleType, RegisteredObjectLookup registry)
            : base(registeredType, concreteType, lifecycleType, registry)
        {
            RegisteredSingletons = new Dictionary<Type, object>();
        }

        public override object GetInstance()
        {
            if (RegisteredSingletons.ContainsKey(RegisteredType))
            {
                return RegisteredSingletons[RegisteredType];
            }

            var instance = CreateInstance(ConcreteType);
            RegisteredSingletons.Add(RegisteredType, instance);

            return instance;
        }
    }
}



