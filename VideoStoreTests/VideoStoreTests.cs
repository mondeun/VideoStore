using NSubstitute;
using NUnit.Framework;
using VideoStore;
using VideoStore.Exceptions;
using VideoStore.Interfaces;

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

        [Test]
        public void CanRegisterNewCustomer()
        {
            _sut.RegisterCustomer("John Doe", "1234567890");
            var customer = _sut.GetCustomers().Find(x => x.Name == "John Doe");

            Assert.AreEqual("John Doe", customer.Name);
        }

        [Test]
        public void CannotRegisterSameCustomertwice()
        {
            _sut.RegisterCustomer("John Doe", "1234567890");

            Assert.Throws<CustomerException>(() =>
                _sut.RegisterCustomer("John Doe", "1234567890")
            );
        }
    }
}
