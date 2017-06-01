using System;

namespace VideoStore.Exceptions
{
    public class MovieException : Exception
    {
        public MovieException(string message) : base(message)
        {
            
        }
    }
}
