using System;
using System.Collections.Generic;
using VideoStore.Models;

namespace VideoStore.Exceptions
{
    public class RentalException : Exception
    {
        public RentalException(string message) : base(message)
        {
        }
    }
}