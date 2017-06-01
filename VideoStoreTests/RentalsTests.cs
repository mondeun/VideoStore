using NUnit.Framework;
using VideoStore.Models;

namespace VideoStoreTests
{
    [TestFixture]
    public class RentalsTests
    {
        private Rental _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Rental();
        }
    }
}
