using Microsoft.EntityFrameworkCore;
using EBCaseStudy.Models;

namespace EBCaseStudy.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		public DbSet<Order> Orders => Set<Order>();
		public DbSet<OrderItem> OrderItems => Set<OrderItem>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>(b =>
			{
				b.HasKey(o => o.Id);
				b.Property(o => o.CustomerName).IsRequired();
				b.HasMany(o => o.Items).WithOne(i => i.Order!).HasForeignKey(i => i.OrderId).OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<OrderItem>(b =>
			{
				b.HasKey(i => i.Id);
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
