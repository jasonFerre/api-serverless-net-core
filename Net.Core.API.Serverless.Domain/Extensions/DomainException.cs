using System;

namespace Net.Core.API.Serverless.Domain.Extensions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
