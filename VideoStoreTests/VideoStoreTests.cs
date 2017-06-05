using System;
using NSubstitute;
using NUnit.Framework;
using VideoStore;
using VideoStore.Exceptions;
using VideoStore.Interfaces;
using VideoStore.Models;

namespace VideoStoreTests
{
    [TestFixture]
    public class VideoStoreTests
    {
        private IRentals _rentals;
        private VideoStore.VideoStore _sut;

        [SetUp]
        public void Setup()
        {
            _rentals = Substitute.For<Rentals>(Substitute.For<IDateTime>());
            _sut = new VideoStore.VideoStore(_rentals);
        }

        #region General tests
 
        [Test]
        public void IncorrectSsnFormatThrowsException()
        {
            Assert.Throws<SocialSecurityNumberFormatException>(() =>
                _sut.RegisterCustomer("John Doe", "20000101")
            );
            Assert.Throws<SocialSecurityNumberFormatException>(() => 
                _sut.RentMovie("Rambo", "20000101")
            );
            Assert.Throws<SocialSecurityNumberFormatException>(() =>
                _sut.ReturnMovie("Rambo", "20000101")
            );
        }

        [Test]
        public void MovieTitleThrowsExceptionOnEmpty()
        {
            _sut.RegisterCustomer("John Doe", "2000-01-01");

            Assert.Throws<MovieException>(() =>
                _sut.AddMovie(new Movie
                {
                    Title = string.Empty,
                    Year = 1988,
                    Genre = Genre.Action
                })
            );
            Assert.Throws<MovieException>(() =>
                _sut.RentMovie(string.Empty, "2000-01-01")
            );
            Assert.Throws<MovieException>(() =>
                _sut.ReturnMovie(string.Empty, "2000-01-01")
            );
        }

        #endregion

        #region Register customer

        [Test]
        public void CanRegisterNewCustomer()
        {
            _sut.RegisterCustomer("John Doe", "2000-01-01");
            var customers = _sut.GetCustomers();

            Assert.AreEqual(1, customers.Count);
            Assert.AreEqual("John Doe", customers[0].Name);
        }

        [Test]
        public void NameCannotBeEmpty()
        {
            Assert.Throws<CustomerException>(() => 
                _sut.RegisterCustomer(string.Empty, "2000-01-01")
            );
        }

        [Test]
        public void CannotRegisterSameCustomertwice()
        {
            _sut.RegisterCustomer("John Doe", "2000-01-01");

            Assert.Throws<CustomerException>(() =>
                _sut.RegisterCustomer("John Doe", "2000-01-01")
            );
        }
        #endregion

        #region Add Movie

        [Test]
        public void CanAddMovie()
        {
            var movie = new Movie
            {
                Title = "Rambo",
                Year = 2000,
                Genre = Genre.Action
            };

            _sut.AddMovie(movie);
            var movies = _sut.GetMovies();

            Assert.AreEqual(1, movies.Count);
            Assert.Contains(movie, movies);
        }

        [Test]
        public void CanAddSameMovieUpToThreeTimes()
        {
            var movie = new Movie
            {
                Title = "Rambo",
                Year = 2000,
                Genre = Genre.Action
            };

            _sut.AddMovie(movie);
            _sut.AddMovie(movie);
            _sut.AddMovie(movie);
            var movies = _sut.GetMovies();

            Assert.AreEqual(3, movies.Count);
        }

        [Test]
        public void AddingFourthOfSameMovieThrowsException()
        {
            var movie = new Movie
            {
                Title = "Rambo",
                Year = 2000,
                Genre = Genre.Action
            };

            _sut.AddMovie(movie);
            _sut.AddMovie(movie);
            _sut.AddMovie(movie);

            Assert.Throws<MovieException>(() =>
                _sut.AddMovie(movie)
            );
        }
        #endregion

        #region Rent Movie

        [Test]
        public void CanRentMovie()
        {
            var movie = new Movie
            {
                Title = "Rambo",
                Year = 2000,
                Genre = Genre.Action
            };

            _sut.RegisterCustomer("John Doe", "2000-01-01");
            _sut.AddMovie(movie);
            _sut.RentMovie("Rambo", "2000-01-01");

            _rentals.Received().AddRental(Arg.Is("Rambo"), Arg.Is("2000-01-01"));
        }

        [Test]
        public void CannotRentNonExistentMovie()
        {
            _sut.RegisterCustomer("John Doe", "2000-01-01");

            var exception = Assert.Throws<MovieException>(() => 
                _sut.RentMovie("Rambo", "2000-01-01")
            );
            Assert.AreEqual("Cannot rent non existent movie", exception.Message);
            _rentals.DidNotReceive().AddRental(Arg.Is("Rambo"), Arg.Is("2000-01-01"));
        }

        [Test]
        public void UnregisteredCustomerCannotRentMovie()
        {
            var movie = new Movie
            {
                Title = "Rambo",
                Year = 2000,
                Genre = Genre.Action
            };

            _sut.AddMovie(movie);

            Assert.Throws<CustomerException>(() => 
                _sut.RentMovie("Rambo", "2000-01-01")
            );
            _rentals.DidNotReceive().AddRental("Rambo", "2000-01-01");
        }

        #endregion

        #region Return Rental

        [Test]
        public void CanReturnRental()
        {
            var movie = new Movie
            {
                Title = "Rambo",
                Year = 2000,
                Genre = Genre.Action
            };

            _sut.RegisterCustomer("John Doe", "2000-01-01");
            _sut.AddMovie(movie);
            _sut.RentMovie("Rambo", "2000-01-01");

            _sut.ReturnMovie("Rambo", "2000-01-01");

            _rentals.Received().RemoveRental("Rambo", "2000-01-01");
            Assert.AreEqual(0, _rentals.GetRentalsFor("2000-01-01").Count);
        }

        [Test]
        public void CannotReturnNonRentedMovie()
        {
            var movie = new Movie
            {
                Title = "Rambo",
                Year = 2000,
                Genre = Genre.Action
            };

            _sut.RegisterCustomer("John Doe", "2000-01-01");
            _sut.AddMovie(movie);

            Assert.Throws<RentalException>(() => 
                _sut.ReturnMovie("Rambo", "2000-01-01")
            );
        }

        #endregion
    }
}
