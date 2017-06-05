using VideoStore.Exceptions;

namespace VideoStore.Models
{
    public class Customer
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new CustomerException("Customer name cannot be empty or white space");
                _name = value;
            }
        }
        public string SocialSecurityNumber { get; set; }
    }
}