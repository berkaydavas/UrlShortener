using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL;
using UrlShortener.DAL.Contexts;
using UrlShortener.WEB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.ConfigureSettings();

builder.Services.AddBusinessServices();

builder.Services.ConfigurePages();
builder.Services.ConfigureIdentity();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
