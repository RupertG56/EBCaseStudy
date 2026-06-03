using EBCaseStudy.Components;
using EBCaseStudy.Data;
using EBCaseStudy.Services;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
{
	client.BaseAddress = new Uri("https://api.casestudy.enterbridge.com/");
});

// Add EF Core with SQLite (local file)
Batteries.Init();
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "orders.db");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlite($"Data Source={dbPath}"));

//Register Repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
