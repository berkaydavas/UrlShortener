using System.ComponentModel.DataAnnotations;

namespace UrlShortener.DAL.Entities;

public class Base
{
  [Key]
  public int Id { get; set; }

  public bool IsActive { get; set; } = true;

  public DateTime CreatedDate { get; set; } = DateTime.Now;

  public DateTime? UpdatedDate { get; set; }
}