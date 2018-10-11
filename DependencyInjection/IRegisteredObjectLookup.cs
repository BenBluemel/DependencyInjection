using System;

namespace DependencyInjection
{
    public interface IRegisteredObjectLookup
    {
        void AddObject(Type resolveType, Type concreteType, LifecycleType lifecycleType);
        bool ContainsType(Type resolveType);
        object ResolveObject(Type resolveType);
        RegisteredObjectInfo GetRegisteredObjectInfo(Type resolveType);
    }
}