namespace MyMauiApp.Shared.Models;

public class Asset
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? ModelUrl { get; set; }
    public string? Author { get; set; }
    public string? Category { get; set; }
}