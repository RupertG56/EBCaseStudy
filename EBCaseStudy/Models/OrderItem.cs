namespace EBCaseStudy.Models
{
	public class OrderItem
	{
		public int Id { get; set; }

		// FK
		public int OrderId { get; set; }
		public Order? Order { get; set; }

		// Snapshot of product at order time
		public int ProductId { get; set; }
		public string? ProductName { get; set; }
		public double UnitPrice { get; set; }
		public double Quantity { get; set; }
		public string? UnitOfMeasure { get; set; }
		public double LineTotal => UnitPrice * Quantity;
	}
}
