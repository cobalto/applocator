using System;

namespace AppLocator.Domain
{
    public sealed class DomainException : Exception
    {
        internal DomainException(string domainMessage)
            : base(domainMessage)
        {
        }

        internal DomainException(string domainMessage, Exception innerException)
            : base(domainMessage, innerException)
        {
        }
    }
}
