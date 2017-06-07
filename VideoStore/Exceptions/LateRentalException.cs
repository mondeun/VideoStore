using System;
using System.Collections.Generic;
using System.Text;
using VideoStore.Models;

namespace VideoStore.Exceptions
{
    public class LateRentalException : Exception
    {
        private readonly List<Rental> _rentals;

        public LateRentalException(List<Rental> rentals)
        {
            _rentals = rentals;
        }

        public override string Message => LateRentalsTostring();

        private string LateRentalsTostring()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Late Returns;");
            foreach (var rental in _rentals)
            {
                sb.AppendLine($"Title: {rental.Movie} - Due Date: {rental.DueDate}");
            }
            return sb.ToString();
        }
    }
}
