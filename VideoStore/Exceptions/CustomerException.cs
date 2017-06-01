using System;

namespace VideoStore.Exceptions
{
    public class CustomerException : Exception
    {
        public CustomerException(string message) : base(message)
        {
            
        }
    }
}