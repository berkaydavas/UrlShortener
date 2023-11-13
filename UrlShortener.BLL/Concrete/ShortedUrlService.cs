using UrlShortener.BLL.Abstract;
using UrlShortener.BLL.Repos;
using UrlShortener.DAL.Contexts;
using UrlShortener.DAL.Entities;

namespace UrlShortener.BLL.Concrete;

public class ShortedUrlService : Repository<ShortedUrl>, IShortedUrlService
{
  public ShortedUrlService(ApplicationDbContext db) : base(db)
  {
  }
}