using System.Collections.Generic;
using VideoStore.Models;

namespace VideoStore.Interfaces
{
    public interface IRentals
    {
        void AddRental(string movieTitle, string socialSecurityNumber);
        void RemoveRental(string movieTitle, string socialSecurityNumber);
        List<Rental> GetRentalsFor(string socialSecurityNumber);
    }
}
