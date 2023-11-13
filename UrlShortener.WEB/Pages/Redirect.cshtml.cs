using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener.BLL.Abstract;
using UrlShortener.DAL.Entities;

namespace UrlShortener.WEB.Pages;

public class RedirectModel : PageModel
{
  private readonly IShortedUrlService _urlService;

  public RedirectModel(IShortedUrlService urlService)
  {
    _urlService = urlService;
  }

  public async Task<IActionResult> OnGetAsync(string code)
  {
    ShortedUrl? url = await _urlService.GetAsync(x => x.IsActive && x.Code == code);
    if (url == null)
    {
      return NotFound();
    }

    url.Click++;
    _urlService.Update(url);
    await _urlService.SaveAsync();

    return Redirect(url.Url);
  }
}