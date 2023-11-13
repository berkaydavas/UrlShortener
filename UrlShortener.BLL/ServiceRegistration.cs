using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UrlShortener.BLL.Abstract;
using UrlShortener.BLL.Abstract.Helpers;
using UrlShortener.BLL.Concrete;
using UrlShortener.BLL.Concrete.Helpers;

namespace UrlShortener.BLL;

public static class ServiceRegistration
{
  public static void AddBusinessServices(this IServiceCollection services)
  {
    services.AddTransient<IEmailSender, MailService>();
    services.TryAddScoped<IMailService, MailService>();

    services.TryAddScoped<IShortedUrlService, ShortedUrlService>();
  }
}