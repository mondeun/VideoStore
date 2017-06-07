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
        private readonly Dictionary<string, List<Movie>> _movies;

        public VideoStore(IRentals rentals)
        {
            _rentals = rentals;
            _customers = new List<Customer>();
            _movies = new Dictionary<string, List<Movie>>();
        }

        /// <summary>
        /// Add new customer to the register
        /// Throws CustomerException
        /// </summary>
        /// <param name="name">Customers name</param>
        /// <param name="socialSecurityNumber">Customers social security number</param>
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

        /// <summary>
        /// Add movie to stock
        /// Throws MovieException
        /// </summary>
        /// <param name="movie">Movie to be added</param>
        public void AddMovie(Movie movie)
        {
            var title = movie.Title;

            if (_movies.ContainsKey(title))
            {
                if (_movies[title].Count(x => x.Title.Equals(title)) >= 3)
                    throw new MovieException("Cannot add more than three of the same movie");

                _movies[title].Add(movie);
            }
            else
            {
                _movies[title] = new List<Movie> {movie};
            }
        }

        /// <summary>
        /// Rent out a movie to a registered customer
        /// </summary>
        /// <param name="movieTitle">Title of the movie</param>
        /// <param name="socialSecurityNumber">Customers social security number</param>
        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            if (string.IsNullOrEmpty(movieTitle))
                throw new MovieException("Movie title is empty");

            VerifySocialSecurityNumberFormat(socialSecurityNumber);
            if (!_customers.Any(x => x.SocialSecurityNumber.Equals(socialSecurityNumber)))
                throw new CustomerException("Unregistered customer cannot rent movies");

            if (!_movies.ContainsKey(movieTitle))
                throw new MovieException("Cannot rent non existent movie");

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

        /// <summary>
        /// Return rented movie
        /// </summary>
        /// <param name="movieTitle">Title of movie</param>
        /// <param name="socialSecurityNumber">Customers social security number</param>
        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            if (string.IsNullOrEmpty(movieTitle))
                throw new MovieException("Movie title is empty");

            if (!_movies.ContainsKey(movieTitle))
                throw new MovieException("Cannot return non-existent movie");

            VerifySocialSecurityNumberFormat(socialSecurityNumber);

            if (!_rentals.GetRentalsFor(socialSecurityNumber).Any(x => x.Movie.Equals(movieTitle)))
                throw new RentalException($"{movieTitle} is not rented by {socialSecurityNumber}");

            _rentals.RemoveRental(movieTitle, socialSecurityNumber);
        }

        /// <summary>
        /// Get all movies from stock
        /// </summary>
        /// <returns></returns>
        public List<Movie> GetMovies()
        {
            return _movies.SelectMany(x => x.Value).ToList();
        }

        private static void VerifySocialSecurityNumberFormat(string socialSecurityNumber)
        {
            if (!Regex.IsMatch(socialSecurityNumber, @"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$"))
                throw new SocialSecurityNumberFormatException(socialSecurityNumber);
        }
    }
}
