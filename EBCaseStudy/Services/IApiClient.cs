using EBCaseStudy.Services.Models;
using System.Text.Json;

namespace EBCaseStudy.Services
{
	public interface IApiClient
	{
		// Prices
		Task<PaginatedResponse<PriceDto>?> GetPricesAsync(
			int? pageNumber = null,
			int? pageSize = null,
			int? productId = null,
			DateTimeOffset? startDate = null,
			DateTimeOffset? endDate = null,
			double? minAmount = null,
			double? maxAmount = null,
			string? unitOfMeasure = null,
			string? sortBy = null,
			string? sortDirection = null,
			CancellationToken cancellationToken = default);

		Task<PriceDto?> GetPriceAsync(int id, CancellationToken cancellationToken = default);

		// Products
		Task<PaginatedResponse<ProductDto>?> GetProductsAsync(
			int? pageNumber = null,
			int? pageSize = null,
			string? name = null,
			string? description = null,
			string? sku = null,
			string? category = null,
			string? sortBy = null,
			string? sortDirection = null,
			CancellationToken cancellationToken = default);

		Task<ProductDto?> GetProductAsync(int id, CancellationToken cancellationToken = default);
		Task<List<string>?> GetProductCategoriesAsync(CancellationToken cancellationToken = default);

		// Utility endpoints
		Task<List<string>?> GetUnitOfMeasuresAsync(CancellationToken cancellationToken = default);
		Task<bool> CheckHealthAsync(CancellationToken cancellationToken = default);
		Task<JsonDocument?> GetStatsAsync(CancellationToken cancellationToken = default);
	}
}
