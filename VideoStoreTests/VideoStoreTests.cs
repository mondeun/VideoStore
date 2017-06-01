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
            _rentals = Substitute.For<Rentals>();
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
            var movie = new Movie
            {
                Title = string.Empty,
                Year = 2000,
                Genre = Genre.Action
            };

            Assert.Throws<MovieException>(() =>
                _sut.AddMovie(movie)
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
            var customer = _sut.GetCustomers().Find(x => x.Name == "John Doe");

            Assert.AreEqual("John Doe", customer.Name);
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
    }
}
