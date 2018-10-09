using System;
using System.Collections.Generic;

namespace DependencyInjection
{
    public class RegisteredObject
    {
        public Type RegisteredType { get; }

        public Type ConcreteType { get; }

        public LifecycleType LifecycleType { get; }

        public RegisteredObject(Type registeredType, Type concreteType, LifecycleType lifecycleType)
        {
            RegisteredType = registeredType;
            ConcreteType = concreteType;
            LifecycleType = lifecycleType;
        }
    }

    
    public enum LifecycleType
    {
        Transient,
        Singleton
    }

    public interface IContainer
    {
        void Register<TRegisteredType, TConcrete>();
        void Register<TRegisteredType, TConcrete>(LifecycleType lifecycleType);
    }
    public class Container : IContainer
    {
        /// <summary>
        /// Dictionary of RegisteredType, RegisteredObject for faster lookups
        /// </summary>
        protected IDictionary<Type, RegisteredObject> RegisteredObjects { get; private set; }

        public Container()
        {
            RegisteredObjects = new Dictionary<Type, RegisteredObject>();
        }
        public void Register<TRegisteredType, TConcrete>()
        {
            Register<TRegisteredType, TConcrete>(LifecycleType.Transient);
        }

        public void Register<TResolve, TConcrete>(LifecycleType lifecycleType)
        {
            RegisteredObjects.Add(typeof(TResolve), new RegisteredObject(typeof(TResolve), typeof(TConcrete), lifecycleType));
        }

        public RegisteredObject Registered<TResolve>()
        {
            if (RegisteredObjects.ContainsKey(typeof(TResolve)))
                return RegisteredObjects[typeof(TResolve)];
            return null;
        }
    }
}
