using System;

namespace Security.Core.Ioc.Exceptions
{
    internal class DependencyResolveException : Exception
    {
        public DependencyResolveException()
        {
        }

        public DependencyResolveException(string message) : base(message)
        {
        }

        public DependencyResolveException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}