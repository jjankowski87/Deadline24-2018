using System;

namespace Deadline24.Core
{
    public class Response
    {
        public TResponse As<TResponse>()
        {
            return (TResponse)Convert.ChangeType(this, typeof(TResponse));
        }
    }
}