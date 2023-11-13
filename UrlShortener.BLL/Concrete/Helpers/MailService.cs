using System.Net;
using System.Net.Mail;
using System.Text;
using UrlShortener.BLL.Abstract.Helpers;
using UrlShortener.BLL.DTOs.Helpers;

namespace UrlShortener.BLL.Concrete.Helpers;

public class MailService : IMailService
{
  readonly MailSettings _settings;

  public MailService(MailSettings settings)
  {
    _settings = settings;
  }

  public async Task<bool> SendAsync(MailData data)
  {
    try
    {
      MailMessage mail = new()
      {
        From = new(_settings.FromMail, _settings.FromName),
        Subject = data.Subject,
        SubjectEncoding = Encoding.UTF8,
        Body = data.Body,
        BodyEncoding = Encoding.UTF8,
        IsBodyHtml = true,
      };

      data.ToReceivers.ForEach(mail.To.Add);
      data.CcReceivers?.ForEach(mail.CC.Add);
      data.ReplyTos?.ForEach(mail.ReplyToList.Add);

      SmtpClient smtpClient = new(_settings.Host, _settings.Port)
      {
        EnableSsl = _settings.Ssl,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(_settings.User, _settings.Pass),
        DeliveryMethod = SmtpDeliveryMethod.Network,
      };

      await smtpClient.SendMailAsync(mail);

      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      return false;
    }
  }

  public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    => await SendAsync(new MailData(email, subject, htmlMessage));
}