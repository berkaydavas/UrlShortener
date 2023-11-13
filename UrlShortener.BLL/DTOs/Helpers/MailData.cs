using System.Net.Mail;

namespace UrlShortener.BLL.DTOs.Helpers;

public class MailData
{
  public MailData(string toReceiver, string subject, string body)
  {
    ToReceivers = toReceiver.Split(';').ToList();
    Subject = subject;
    Body = body;
  }

  public MailData(string toReceiver, string? ccReceiver, string subject, string body) : this(toReceiver, subject, body)
  {
    if (ccReceiver != null) CcReceivers = ccReceiver.Split(';').ToList();
  }

  public MailData(string toReceiver, string? ccReceiver, string subject, string body, Attachment[] attachments) : this(toReceiver, ccReceiver, subject, body)
  {
    Attachments = attachments;
  }

  public MailData(string toReceiver, string subject, string body, Attachment[] attachments) : this(toReceiver, subject, body)
  {
    Attachments = attachments;
  }

  public List<string> ToReceivers { get; set; }

  public List<string>? CcReceivers { get; set; }

  public List<string>? ReplyTos { get; set; }

  public string Subject { get; set; }

  public string? Body { get; set; }

  public Attachment[]? Attachments { get; set; }
}
