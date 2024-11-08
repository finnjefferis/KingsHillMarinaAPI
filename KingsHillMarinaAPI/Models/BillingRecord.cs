namespace KingsHillMarinaAPI.Models
{
    public class BillingRecord
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public Boat Boat { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillingDate { get; set; }
    }


}
