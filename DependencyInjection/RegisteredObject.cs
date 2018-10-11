using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection
{
    public abstract class RegisteredObject : IRegisteredObject
    {
        protected Type RegisteredType { get; }

        protected Type ConcreteType { get; }

        protected LifecycleType LifecycleType { get; }

        protected RegisteredObjectLookup Registry { get; set; }

        public RegisteredObject(Type registeredType, Type concreteType, LifecycleType lifecycleType, RegisteredObjectLookup registry)
        {
            RegisteredType = registeredType;
            ConcreteType = concreteType;
            LifecycleType = lifecycleType;
            Registry = registry;
        }

        public abstract object GetInstance();

        public RegisteredObjectInfo GetRegisteredObjectInfo()
        {
            return new RegisteredObjectInfo(RegisteredType, ConcreteType, LifecycleType);
        }

        protected object CreateInstance(Type type)
        {
            var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
            if (defaultConstructor != null)
            {
                return Activator.CreateInstance(type);
            }

            var allContructors = type.GetConstructors();
            foreach (var constructor in allContructors.OrderByDescending(p => p.GetParameters().Length))
            {
                bool foundConstructor = true;
                foreach (var parameter in constructor.GetParameters())
                {
                    if (!Registry.ContainsType(parameter.ParameterType))
                    {
                        foundConstructor = false;
                        break;
                    }
                }

                if (foundConstructor)
                {
                    var parameterObjects = new List<object>();

                    foreach (var parameter in constructor.GetParameters())
                    {
                        parameterObjects.Add(Registry.ResolveObject(parameter.ParameterType));
                    }

                    return constructor.Invoke(parameterObjects.ToArray());
                }
            }

            throw new MissingRegistryException($"Could not find a resolvable constructor for {type.Name}");
        }
    }
}



