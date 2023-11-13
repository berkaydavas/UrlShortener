using UrlShortener.BLL.Repos;
using UrlShortener.DAL.Entities;

namespace UrlShortener.BLL.Abstract;

public interface IShortedUrlService : IRepository<ShortedUrl>
{
}