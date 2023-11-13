using Microsoft.AspNetCore.Identity;

namespace UrlShortener.DAL.Entities;

public class User : IdentityUser<int>
{
  public bool? IsActive { get; set; } = true;

  public DateTime CreatedDate { get; set; } = DateTime.Now;

  public DateTime? UpdatedDate { get; set; }
}