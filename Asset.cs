using SQLite;

namespace MyMauiApp.Models;

[Table("Assets")]
public class Asset
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Value { get; set; }
    public string? Icon { get; set; }
    public string? Category { get; set; }
    public string? LocalFilePath { get; set; } // Path to Image or 3D Model file
    public string? Url { get; set; }           // For website references
}