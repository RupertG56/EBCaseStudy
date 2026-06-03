using EBCaseStudy.Models;

namespace EBCaseStudy.Services
{
	public interface IOrderRepository
	{
		Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default);
		Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken = default);
		Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default);
	}
}
