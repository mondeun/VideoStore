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
    public class RentalsTests
    {
        private Rentals _sut;
        private IDateTime dateMock;

        [SetUp]
        public void Setup()
        {
            dateMock = Substitute.For<IDateTime>();
            _sut = new Rentals(dateMock);

            dateMock.Now().Returns(new System.DateTime(1222, 12, 12));
        }

        [Test]
        public void CanAddNewRental()
        {
            string title = "Lord of the ringhs";
            string socialNumber = "2000-01-01";

            _sut.AddRental(title, socialNumber);

            Assert.AreEqual(1, _sut.GetRentalsFor(socialNumber).Count);
        }

        [Test]
        public void CannotRentMovieWithPendingDate()
        {
            dateMock.Now().Returns(new System.DateTime(2000, 1, 1));

            _sut.AddRental("Batman v superman", "2000-01-01");
            dateMock.Now().Returns(new System.DateTime(2000, 1, 4));

            Assert.Throws<RentalException>(() => _sut.AddRental("Rambo", "2000-01-01"));
        }

        [Test]
        public void GetRentalsBySocialNumber()
        {
            _sut.AddRental("Batman v superman", "2000-01-01");
            var result =_sut.GetRentalsFor("2000-01-01");
            Assert.AreEqual(1 , result.Count);
        }

        [Test]
        public void CanRentManyMovies()
        {
            _sut.AddRental("Batman v superman", "2000-01-01");
            _sut.AddRental("Suicide Squad", "2000-01-01");
            _sut.AddRental("Lord of the rings", "2000-01-01");

            var result = _sut.GetRentalsFor("2000-01-01");
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void LimitOnRentedMoviesAllowed()
        {
            _sut.AddRental("Batman v superman", "2000-01-01");
            _sut.AddRental("Suicide Squad", "2000-01-01");
            _sut.AddRental("Lord of the rings", "2000-01-01");
            
            Assert.Throws<RentalException>(() => _sut.AddRental("Superman", "2000-01-01"));
        }
    }
}
