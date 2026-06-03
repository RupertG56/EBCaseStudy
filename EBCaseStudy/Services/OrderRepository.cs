using EBCaseStudy.Data;
using EBCaseStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace EBCaseStudy.Services
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _db;
		public OrderRepository(ApplicationDbContext db) => _db = db;

		public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
		{
			_db.Orders.Add(order);
			await _db.SaveChangesAsync(cancellationToken);
			return order;
		}

		public Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken = default) =>
			_db.Orders.Include(o => o.Items).OrderByDescending(o => o.CreatedAt).ToListAsync(cancellationToken);

		public Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default) =>
			_db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
	}
}
