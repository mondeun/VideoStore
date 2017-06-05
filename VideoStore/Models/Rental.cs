namespace VideoStore.Models
{
    public class Rental
    {
        public string Movie { get; set; }
        public string Customer { get; set; }
        public System.DateTime RentedAt { get; set; }
        public System.DateTime DueDate => RentedAt.AddDays(3);
    }
}