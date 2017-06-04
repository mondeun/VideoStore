using System;

namespace VideoStore.Exceptions
{
    public class RentalException : Exception
    {
        public RentalException(string message) : base(message)
        {
        }
    }
}