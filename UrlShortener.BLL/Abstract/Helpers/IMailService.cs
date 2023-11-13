using Microsoft.AspNetCore.Identity.UI.Services;
using UrlShortener.BLL.DTOs.Helpers;

namespace UrlShortener.BLL.Abstract.Helpers;

public interface IMailService : IEmailSender
{
  Task<bool> SendAsync(MailData data);
}