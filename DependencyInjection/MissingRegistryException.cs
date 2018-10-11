using System;

namespace DependencyInjection
{
    [Serializable]
    public class MissingRegistryException : Exception
    {
        public MissingRegistryException(string message)
            : base(message)
        {
        }
    }
}



