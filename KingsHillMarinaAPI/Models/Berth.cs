namespace KingsHillMarinaAPI.Models
{

    public class Berth
    {
        public int Id { get; set; }
        public required string Location { get; set; }

  
        public int? BoatId { get; set; }
    }
}