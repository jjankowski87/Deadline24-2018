using System;

namespace Deadline24.Core.Exceptions
{
    public class ServerExceptionBase : Exception
    {
        public ServerExceptionBase(string message) : base(message)
        {
        }
    }
}