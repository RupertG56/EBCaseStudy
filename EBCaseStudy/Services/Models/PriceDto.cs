namespace EBCaseStudy.Services.Models
{
	public class PriceDto
	{
		public int Id { get; set; }
		public double Amount { get; set; }
		public DateTime DateTime { get; set; }
		public double Quantity { get; set; }
		public string? UnitOfMeasure { get; set; }
		public int ProductId { get; set; }
		public ProductDto? Product { get; set; }
	}
}
