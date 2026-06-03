using EBCaseStudy.Services.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace EBCaseStudy.Services
{
	public class ApiClient : IApiClient
	{
		private readonly HttpClient _http;
		private readonly JsonSerializerOptions _jsonOptions = new()
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		public ApiClient(HttpClient http) => _http = http;

		// Helper to build query string
		private static string BuildQuery(string path, Dictionary<string, string?> query)
		{
			var filtered = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
			foreach (var kv in query)
			{
				if (!string.IsNullOrEmpty(kv.Value))
				{
					filtered[kv.Key] = kv.Value;
				}
			}
			return filtered.Count == 0 ? path : QueryHelpers.AddQueryString(path, filtered!);
		}

		public Task<PaginatedResponse<PriceDto>?> GetPricesAsync(int? pageNumber = null, int? pageSize = null, int? productId = null,
			DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, double? minAmount = null, double? maxAmount = null,
			string? unitOfMeasure = null, string? sortBy = null, string? sortDirection = null, CancellationToken cancellationToken = default)
		{
			var q = new Dictionary<string, string?>
			{
				["PageNumber"] = pageNumber?.ToString(),
				["PageSize"] = pageSize?.ToString(),
				["productId"] = productId?.ToString(),
				["startDate"] = startDate?.ToString("yyyy-MM-dd"),
				["endDate"] = endDate?.ToString("yyyy-MM-dd"),
				["minAmount"] = minAmount?.ToString(),
				["maxAmount"] = maxAmount?.ToString(),
				["unitOfMeasure"] = unitOfMeasure,
				["sortBy"] = sortBy,
				["sortDirection"] = sortDirection
			};

			var url = BuildQuery("api/prices", q);
			return _http.GetFromJsonAsync<PaginatedResponse<PriceDto>>(url, _jsonOptions, cancellationToken);
		}

		public Task<PriceDto?> GetPriceAsync(int id, CancellationToken cancellationToken = default) =>
			_http.GetFromJsonAsync<PriceDto>($"api/prices/{id}", _jsonOptions, cancellationToken);

		public Task<PaginatedResponse<ProductDto>?> GetProductsAsync(int? pageNumber = null, int? pageSize = null,
			string? name = null, string? description = null, string? sku = null, string? category = null,
			string? sortBy = null, string? sortDirection = null, CancellationToken cancellationToken = default)
		{
			var q = new Dictionary<string, string?>
			{
				["PageNumber"] = pageNumber?.ToString(),
				["PageSize"] = pageSize?.ToString(),
				["name"] = name,
				["description"] = description,
				["sku"] = sku,
				["category"] = category,
				["sortBy"] = sortBy,
				["sortDirection"] = sortDirection
			};
			var url = BuildQuery("api/products", q);
			return _http.GetFromJsonAsync<PaginatedResponse<ProductDto>>(url, _jsonOptions, cancellationToken);
		}

		public Task<ProductDto?> GetProductAsync(int id, CancellationToken cancellationToken = default) =>
			_http.GetFromJsonAsync<ProductDto>($"api/products/{id}", _jsonOptions, cancellationToken);

		public Task<List<string>?> GetProductCategoriesAsync(CancellationToken cancellationToken = default) =>
			_http.GetFromJsonAsync<List<string>>("api/products/categories", _jsonOptions, cancellationToken);

		public Task<List<string>?> GetUnitOfMeasuresAsync(CancellationToken cancellationToken = default) =>
			_http.GetFromJsonAsync<List<string>>("api/unit-of-measures", _jsonOptions, cancellationToken);

		public async Task<bool> CheckHealthAsync(CancellationToken cancellationToken = default)
		{
			var resp = await _http.GetAsync("api/health", cancellationToken);
			return resp.IsSuccessStatusCode;
		}

		public async Task<JsonDocument?> GetStatsAsync(CancellationToken cancellationToken = default)
		{
			var resp = await _http.GetAsync("api/stats", cancellationToken);
			if (!resp.IsSuccessStatusCode) return null;
			await using var stream = await resp.Content.ReadAsStreamAsync(cancellationToken);
			return await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
		}
	}
}
