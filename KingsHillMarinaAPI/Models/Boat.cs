using KingsHillMarinaAPI.Models;

public class Boat
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double Length { get; set; }
    public required string Make { get; set; }
    public required string Type { get; set; }

    // Reference OwnerId instead of Owner to reduce JSON depth
    public int OwnerId { get; set; }

    // Use BerthId rather than the Berth object for simplified JSON
    public int? BerthId { get; set; }
}