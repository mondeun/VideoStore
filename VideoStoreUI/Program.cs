using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO
            var quit = false;

            do
            {
                Console.Clear();
                PrintMenu();

                Console.Write("> ");
                var menuChoice = Console.ReadLine();

                switch (menuChoice?.ToLower())
                {
                    case "q":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine($"{menuChoice} is not a valid alternative\n");
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
            sb.AppendLine("3. Rent Movie(s)");
            sb.AppendLine("4. Receive rented movie");
            sb.AppendLine("q. Quit");

            Console.WriteLine(sb);
        }
    }
}
