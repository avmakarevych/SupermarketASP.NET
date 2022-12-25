namespace SupermarketAPI;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Price { get; set; }
    public int? ProductCategoriesId { get; set; }
    public string? PhotoUrl { get; set; }
    public ProductCategory? ProductCategory { get; set; }
}