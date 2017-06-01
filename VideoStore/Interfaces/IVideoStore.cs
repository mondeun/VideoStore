using System.Collections.Generic;
using VideoStore.Models;

namespace VideoStore.Interfaces
{
    public interface IVideoStore
    {
        void RegisterCustomer(string name, string socialSecurityNumber);
        void AddMovie(Movie movie);
        void RentMovie(string movieTitle, string socialSecurityNumber);
        List<Customer> GetCustomers();
        void ReturnMovie(string movieTitle, string socialSecurityNumber);
        List<Movie> GetMovies();
    }
}
