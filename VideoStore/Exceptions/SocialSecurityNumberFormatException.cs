using System;

namespace VideoStore.Exceptions
{
    [Serializable]
    public class SocialSecurityNumberFormatException : Exception
    {
        private readonly string _message;

        public SocialSecurityNumberFormatException(string message)
        {
            _message =
                $"{message} is in incorrect format. Social security number has to be formatted as following 'YYYY-MM-DD'";
        }

        public override string Message => _message;
    }
}