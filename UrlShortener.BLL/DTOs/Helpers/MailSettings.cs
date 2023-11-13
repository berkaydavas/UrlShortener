namespace UrlShortener.BLL.DTOs.Helpers;

public class MailSettings
{
  public string Host { get; set; } = null!;

  public int Port { get; set; }

  public bool Ssl { get; set; }

  public string User { get; set; } = null!;

  public string Pass { get; set; } = null!;

  public string FromMail { get; set; } = null!;

  public string FromName { get; set; } = null!;
}
