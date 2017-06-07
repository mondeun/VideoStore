using System;
using System.Text;
using VideoStore;
using VideoStore.Exceptions;
using VideoStore.Interfaces;
using VideoStore.Models;

namespace VideoStoreUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new VideoStore.VideoStore(new Rentals(new VideoStore.DateTime()));
            var quit = false;
            do
            {
                Console.Clear();
                PrintMenu();

                Console.Write("> ");
                var menuChoice = Console.ReadLine();

                switch (menuChoice?.ToLower())
                {
                    case "1":
                        RegisterCustomer(store);
                        break;
                    case "2":
                        AddMovie(store);
                        break;
                    case "3":
                        RentMovie(store);
                        break;
                    case "4":
                        ReturnMovie(store);
                        break;
                    case "5":
                        ViewMovies(store);
                        break;
                    case "q":
                        quit = true;
                        break;
                }
                Console.ReadLine();
            } while (!quit);
        }

        private static void PrintMenu()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Welcome to 'Video Store'");
            sb.AppendLine("1. Register customer");
            sb.AppendLine("2. Add new movie");
            sb.AppendLine("3. Rent Movie");
            sb.AppendLine("4. Receive rented movie");
            sb.AppendLine("5. View movie library");
            sb.AppendLine("q. Quit");

            Console.WriteLine(sb);
        }

        private static void RegisterCustomer(IVideoStore store)
        {
            Console.Write("Enter Name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Social security number: ");
            var ssn = Console.ReadLine();

            try
            {
                store.RegisterCustomer(name, ssn);
                Console.WriteLine($"{name} added");
            }
            catch (SocialSecurityNumberFormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (CustomerException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void AddMovie(IVideoStore store)
        {
            var movie = new Movie();
            Console.Write("Enter title: ");
            movie.Title = Console.ReadLine();
            Console.Write("Enter year: ");
            movie.Year = int.TryParse(Console.ReadLine(), out int year) ? year : 2017;
            Console.Write("Enter genre: ");
            movie.Genre = Enum.TryParse(Console.ReadLine(), out Genre genre) ? genre : Genre.Action;

            try
            {
                store.AddMovie(movie);
                Console.WriteLine($"{movie.Title} Added");
            }
            catch (MovieException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void RentMovie(IVideoStore store)
        {
            Console.Write("Enter title: ");
            var title = Console.ReadLine();
            Console.WriteLine("Enter social security number: ");
            var ssn = Console.ReadLine();

            try
            {
                store.RentMovie(title, ssn);
                Console.WriteLine($"{title} rented");
            }
            catch (SocialSecurityNumberFormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (MovieException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (CustomerException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (LateRentalException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (RentalException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ReturnMovie(IVideoStore store)
        {
            Console.Write("Enter title: ");
            var title = Console.ReadLine();
            Console.Write("Enter social security number: ");
            var ssn = Console.ReadLine();

            try
            {
                store.ReturnMovie(title, ssn);
                Console.WriteLine($"{title} received");
            }
            catch (SocialSecurityNumberFormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (RentalException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ViewMovies(IVideoStore store)
        {
            var sb = new StringBuilder();
            foreach (var movie in store.GetMovies())
            {
                sb.AppendLine($"{movie.Title} - {movie.Year} - {movie.Genre}");
            }

            Console.WriteLine(sb);
        }
    }
}
