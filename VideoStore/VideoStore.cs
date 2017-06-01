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
        private readonly List<Movie> _movies;

        public VideoStore(IRentals rentals)
        {
            _rentals = rentals;
            _customers = new List<Customer>();
            _movies = new List<Movie>();
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
            if(string.IsNullOrEmpty(movie.Title))
                throw new MovieException("Movie title is empty");

            _movies.Add(movie);
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            if (string.IsNullOrEmpty(movieTitle))
                throw new MovieException("Movie title is empty");

            VerifySocialSecurityNumberFormat(socialSecurityNumber);
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>All registered customers</returns>
        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            if (string.IsNullOrEmpty(movieTitle))
                throw new MovieException("Movie title is empty");

            VerifySocialSecurityNumberFormat(socialSecurityNumber);
        }

        public List<Movie> GetMovies()
        {
            return _movies;
        }

        private static void VerifySocialSecurityNumberFormat(string socialSecurityNumber)
        {
            if (!Regex.IsMatch(socialSecurityNumber, @"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$"))
                throw new SocialSecurityNumberFormatException(socialSecurityNumber);
        }
    }
}
