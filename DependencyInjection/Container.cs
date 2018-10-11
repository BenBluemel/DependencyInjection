using System;
using System.Collections.Generic;
using System.Linq;

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

    public abstract class RegisteredObject
    {
        public Type RegisteredType { get; }

        public Type ConcreteType { get; }

        public LifecycleType LifecycleType { get; }

        protected ObjectRegistry Registry { get; set; }

        public RegisteredObject(Type registeredType, Type concreteType, LifecycleType lifecycleType, ObjectRegistry registry)
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
                    if (!Registry.ContainsResolveType(parameter.ParameterType))
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

    public class TransientRegisteredObject : RegisteredObject
    {
        public TransientRegisteredObject(Type registeredType, Type concreteType, LifecycleType lifecycleType, ObjectRegistry registry)
            : base(registeredType, concreteType, lifecycleType, registry)
        {
        }

        public override object GetInstance()
        {
            return CreateInstance(ConcreteType);
        }
    }

    public class SingletonRegisteredObject : RegisteredObject
    {
        Dictionary<Type, object> RegisteredSingletons { get; set; }

        public SingletonRegisteredObject(Type registeredType, Type concreteType, LifecycleType lifecycleType, ObjectRegistry registry)
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

    public enum LifecycleType
    {
        Transient,
        Singleton
    }

    public class ObjectRegistry
    {
        /// <summary>
        /// Dictionary of RegisteredType, RegisteredObject for faster lookups
        /// </summary>

        protected IDictionary<Type, RegisteredObject> RegisteredObjects { get; private set; }

        public ObjectRegistry()
        {
            RegisteredObjects = new Dictionary<Type, RegisteredObject>();
        }
        public void AddObject(Type resolveType, Type concreteType, LifecycleType lifecycleType)
        {
            if (lifecycleType == LifecycleType.Transient)
            {
                RegisteredObjects.Add(resolveType, new TransientRegisteredObject(resolveType, concreteType, lifecycleType, this));
            }
            else if (lifecycleType == LifecycleType.Singleton)
            {
                RegisteredObjects.Add(resolveType, new SingletonRegisteredObject(resolveType, concreteType, lifecycleType, this));
            }

        }

        public bool ContainsResolveType(Type resolveType)
        {
            return RegisteredObjects.ContainsKey(resolveType);
        }

        public object ResolveObject(Type resolveType)
        {
            if (ContainsResolveType(resolveType))
            {
                return RegisteredObjects[resolveType].GetInstance();
            }

            throw new MissingRegistryException($"Could not find a registered type for {resolveType.Name}");
        }

        public RegisteredObjectInfo GetRegisteredObjectInfo(Type resolveType)
        {
            if (ContainsResolveType(resolveType))
            {
                return RegisteredObjects[resolveType].GetRegisteredObjectInfo();
            }
            return null;
        }
    }

    public interface IContainer
    {
        void Register<TRegisteredType, TConcrete>();
        void Register<TRegisteredType, TConcrete>(LifecycleType lifecycleType);
        TResolve Resolve<TResolve>();
        RegisteredObjectInfo Registered<TResolve>();
    }

    public class Container : IContainer
    {
        protected ObjectRegistry Registry { get; }

        public Container()
        {
            Registry = new ObjectRegistry();
        }
        public void Register<TRegisteredType, TConcrete>()
        {
            Register<TRegisteredType, TConcrete>(LifecycleType.Transient);
        }

        public void Register<TResolve, TConcrete>(LifecycleType lifecycleType)
        {
            Registry.AddObject(typeof(TResolve), typeof(TConcrete), lifecycleType);
        }

        public TResolve Resolve<TResolve>()
        {
            if (!Registry.ContainsResolveType(typeof(TResolve)))
                throw new MissingRegistryException($"Type {typeof(TResolve).Name} not registered.");

            return (TResolve)Registry.ResolveObject(typeof(TResolve));
        }

        public RegisteredObjectInfo Registered<TResolve>()
        {
            return Registry.GetRegisteredObjectInfo(typeof(TResolve));
        }
    }

    public class MissingRegistryException : Exception
    {
        public MissingRegistryException(string message)
            : base(message)
        {
        }
    }
}



