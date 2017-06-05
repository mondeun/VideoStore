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
        private readonly IDateTime _dateTime;
        private readonly List<Rental> _rentals;

        public Rentals(IDateTime dateTime)
        {
            _dateTime = dateTime;
            _rentals = new List<Rental>();
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
            if (GetRentalsFor(socialSecurityNumber).Exists(x => x.Movie == movieTitle))
            {
                throw new RentalException("You already have acopy of this movie");
            }
            if (GetRentalsFor(socialSecurityNumber).Any(x => ))
            {
                
            }


            var rental = new Rental
            {
                Customer = socialSecurityNumber,
                Movie = movieTitle,
                RentedAt = _dateTime.Now()
            };

            _rentals.Add(rental);
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            return _rentals.Where(r => r.Customer.Equals(socialSecurityNumber)).ToList();
        }
    }
}
