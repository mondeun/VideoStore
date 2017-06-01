﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VideoStore.Exceptions;
using VideoStore.Interfaces;
using VideoStore.Models;

namespace VideoStore
{
    public class VideoStore : IVideoStore
    {
        private readonly IRentals _rentals;
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

            if (_customers.Exists(x => x.Name.Equals(name) && x.SocialSecurityNumber.Equals(socialSecurityNumber)))
                throw new CustomerException("Customer already exists");

            _customers.Add(new Customer
            {
                Name = name,
                SocialSecurityNumber = socialSecurityNumber
            });
        }

        public void AddMovie(Movie movie)
        {
            if (string.IsNullOrEmpty(movie.Title))
                throw new MovieException("Movie title is empty");

            if (_movies.Count(x => x.Title.Equals(movie.Title)) >= 3)
                throw new MovieException("Cannot add more than three of the same movie");

            _movies.Add(movie);
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            if (string.IsNullOrEmpty(movieTitle))
                throw new MovieException("Movie title is empty");

            VerifySocialSecurityNumberFormat(socialSecurityNumber);

            _rentals.AddRental(movieTitle, socialSecurityNumber);
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
