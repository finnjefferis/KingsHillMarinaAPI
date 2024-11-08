namespace KingsHillMarinaAPI.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ContactInfo { get; set; }

        // Use BoatIds collection instead of Boat objects for reference to prevent circular data sturcute
        public List<int> BoatIds { get; set; } = new List<int>();
    }

}
