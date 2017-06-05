using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using VideoStore.Exceptions;
using VideoStore.Interfaces;
using VideoStore.Models;

namespace VideoStore
{
    public class Rentals : IRentals
    {
        private IDateTime _dateTime;
        private List<Rental> rentals = new List<Rental>();

        public Rentals(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
            if (GetRentalsFor(socialSecurityNumber).Any(x => x.DueDate <= _dateTime.Now()))
            {
                throw new RentalException("You are late in returning the movie");
            }
            if (GetRentalsFor(socialSecurityNumber).Count >= 3)
            {
                throw new RentalException("You cannot rent more then 3 movies!");
            }

            var rental = new Rental
            {
                Customer = socialSecurityNumber,
                Movie = movieTitle,
                RentedAt = _dateTime.Now()
            };

            rentals.Add(rental);
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            return rentals.Where(r => r.Customer == socialSecurityNumber).ToList();
        }
    }
}
