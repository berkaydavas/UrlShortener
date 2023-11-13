using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.DAL.Entities;

[Index(nameof(Code), IsUnique = true)]
public class ShortedUrl : Base
{
  [StringLength(100)]
  public string Code { get; set; } = null!;

  public int? UserId { get; set; }

  public virtual User? User { get; set; }

  [Url]
  public string Url { get; set; } = null!;

  public int Click { get; set; } = 0;

  public static string GenerateCode()
  {
    string dateString = DateTime.Now.GetHashCode().ToString();
    byte[] data = MD5.HashData(Encoding.Default.GetBytes(dateString));
    StringBuilder builder = new();

    foreach (byte dataByte in data)
      builder.Append(dataByte.ToString("x2"));

    return builder.ToString().Substring(new Random().Next(0, 24), 8);
  }
}