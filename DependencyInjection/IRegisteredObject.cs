using System;

namespace DependencyInjection
{
    public interface IRegisteredObject
    {
        RegisteredObjectInfo GetRegisteredObjectInfo();

        object GetInstance();
    }
}