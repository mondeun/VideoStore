using VideoStore.Exceptions;

namespace VideoStore.Models
{
    public class Movie
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new MovieException("Movie title is empty");
                _title = value;
            }
        }
        public int Year { get; set; }
        public Genre Genre { get; set; }
    }
}