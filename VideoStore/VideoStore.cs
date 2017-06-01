using System;
using System.Collections.Generic;
using VideoStore.Interfaces;
using VideoStore.Models;

namespace VideoStore
{
    public class VideoStore : IVideoStore
    {
        private IRentals _rentals;
        private List<Customer> _customers;

        public VideoStore(IRentals rentals)
        {
            _rentals = rentals;
            _customers = new List<Customer>();
        }

        public void RegisterCustomer(string name, string socialSecurityNumber)
        {
            _customers.Add(new Customer
            {
                Name = name,
                SocialSecurityNumber = socialSecurityNumber
            });
        }

        public void AddMovie(Movie movie)
        {
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
        }
    }
}
