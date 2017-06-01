using System;
using System.Collections.Generic;
using VideoStore.Interfaces;
using VideoStore.Models;

namespace VideoStore
{
    public class Rentals : IRentals
    {
        private IDateTime _dateTime;

        public Rentals(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }
    }
}
