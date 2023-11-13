using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UrlShortener.BLL.DTOs.Helpers;
using UrlShortener.DAL.Contexts;
using UrlShortener.DAL.Entities;

namespace UrlShortener.WEB;

public static class ServiceRegistration
{
  public static void ConfigurePages(this IServiceCollection services)
  {
    services.AddRouting(o => o.LowercaseUrls = true);
  }

  public static void ConfigureIdentity(this IServiceCollection services)
  {
    services.AddDefaultIdentity<User>(options =>
      {
        options.User.RequireUniqueEmail = true;
      options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

    services.AddHttpContextAccessor();

    services.TryAddScoped<UserManager<User>>();
    services.TryAddScoped<SignInManager<User>>();
  }

  public static void ConfigureSettings(this WebApplicationBuilder builder)
  {
    MailSettings mailSettings = new()
    {
      Host = builder.Configuration["Mail:Host"] ?? throw new NullReferenceException(),
      Port = int.Parse(builder.Configuration["Mail:Port"] ?? throw new NullReferenceException()),
      Ssl = (builder.Configuration["Mail:Ssl"] ?? throw new NullReferenceException()) == "true",
      User = builder.Configuration["Mail:User"] ?? throw new NullReferenceException(),
      Pass = builder.Configuration["Mail:Pass"] ?? throw new NullReferenceException(),
      FromMail = builder.Configuration["Mail:FromMail"] ?? throw new NullReferenceException(),
      FromName = builder.Configuration["Mail:FromName"] ?? throw new NullReferenceException(),
    };

    builder.Services.AddSingleton(mailSettings);
  }
}