using System;
using System.Collections.Generic;

namespace DependencyInjection
{
    public abstract class RegisteredObject
    {
        public Type RegisteredType { get; }

        public Type ConcreteType { get; }

        public LifecycleType LifecycleType { get; }

        protected object Instance { get; set; }

        public RegisteredObject(Type registeredType, Type concreteType, LifecycleType lifecycleType)
        {
            RegisteredType = registeredType;
            ConcreteType = concreteType;
            LifecycleType = lifecycleType;
        }

        public abstract object GetInstance();
    }

    public class TransientRegisteredObject : RegisteredObject
   {
        public TransientRegisteredObject(Type registeredType, Type concreteType, LifecycleType lifecycleType)
            : base(registeredType, concreteType, lifecycleType)
        {
        }

        public override object GetInstance()
        {
            return Activator.CreateInstance(ConcreteType);
        }
    }

    public interface IRegisteredObject
    {
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
            RegisteredObjects.Add(typeof(TResolve), new TransientRegisteredObject(typeof(TResolve), typeof(TConcrete), lifecycleType));
        }

        public TResolve Resolve<TResolve>()
        {
            if (!RegisteredObjects.ContainsKey(typeof(TResolve)))
                throw new MissingRegisterException($"Type {typeof(TResolve).Name} not registered.");

            return (TResolve)RegisteredObjects[typeof(TResolve)].GetInstance();
        }

        public RegisteredObject Registered<TResolve>()
        {
            if (RegisteredObjects.ContainsKey(typeof(TResolve)))
                return RegisteredObjects[typeof(TResolve)];
            return null;
        }
    }

    public class MissingRegisterException : Exception
    {
        public MissingRegisterException(string message)
            : base(message)
        {
        }
    }
}
