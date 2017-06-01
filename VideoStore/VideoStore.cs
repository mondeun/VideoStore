using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VideoStore.Exceptions;
using VideoStore.Interfaces;
using VideoStore.Models;

namespace VideoStore
{
    public class VideoStore : IVideoStore
    {
        private IRentals _rentals;
        private readonly List<Customer> _customers;

        public VideoStore(IRentals rentals)
        {
            _rentals = rentals;
            _customers = new List<Customer>();
        }

        public void RegisterCustomer(string name, string socialSecurityNumber)
        {
            VerifySocialSecurityNumberFormat(socialSecurityNumber);

            if (_customers.Exists(x => x.Name == name && x.SocialSecurityNumber == socialSecurityNumber))
                throw new CustomerException("Customer already exists");

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
            VerifySocialSecurityNumberFormat(socialSecurityNumber);
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            VerifySocialSecurityNumberFormat(socialSecurityNumber);
        }

        private static void VerifySocialSecurityNumberFormat(string ssn)
        {
            if (!Regex.IsMatch(ssn, @"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$"))
                throw new SocialSecurityNumberFormatException(ssn);
        }
    }
}
