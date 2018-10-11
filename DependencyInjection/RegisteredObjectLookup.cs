using System;
using System.Collections.Generic;

namespace DependencyInjection
{
    public enum LifecycleType
    {
        Transient,
        Singleton
    }

    /// <summary>
    /// Stores and resolves registered types 
    /// </summary>
    public class RegisteredObjectLookup : IRegisteredObjectLookup
    {
        /// <summary>
        /// Dictionary of RegisteredType, RegisteredObject for faster lookups
        /// </summary>
        protected IDictionary<Type, IRegisteredObject> RegisteredObjects { get; private set; }

        public RegisteredObjectLookup()
        {
            RegisteredObjects = new Dictionary<Type, IRegisteredObject>();
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

        public bool ContainsType(Type resolveType)
        {
            return RegisteredObjects.ContainsKey(resolveType);
        }

        public object ResolveObject(Type resolveType)
        {
            if (ContainsType(resolveType))
            {
                return RegisteredObjects[resolveType].GetInstance();
            }

            throw new MissingRegistryException($"Could not find a registered type for {resolveType.Name}");
        }

        /// <summary>
        /// Used to get RegisteredType, ConcreteType, LifecycleType for a resolved type
        /// </summary>
        public RegisteredObjectInfo GetRegisteredObjectInfo(Type resolveType)
        {
            if (ContainsType(resolveType))
            {
                return RegisteredObjects[resolveType].GetRegisteredObjectInfo();
            }

            return null;
        }
    }
}



