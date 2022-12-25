namespace Supermarket.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int Price { get; set; }
		public int? ProductCategoryId { get; set; }
		public string? PhotoUrl { get; set; }
		public ProductCategory? ProductCategory { get; set; }
	}
}
