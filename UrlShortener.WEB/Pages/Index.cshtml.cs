using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UrlShortener.BLL.Abstract;
using UrlShortener.BLL.Extensions;
using UrlShortener.DAL.Entities;

namespace UrlShortener.WEB.Pages;

public class IndexModel : PageModel
{
  private readonly IShortedUrlService _urlService;

  public IndexModel(IShortedUrlService urlService)
  {
    _urlService = urlService;
  }

  [BindProperty, Required, Url, Display(Name = "URL")]
  public string? LongUrl { get; set; }

  public ShortedUrl[]? Urls { get; set; }

  public string? StatusMessage { get; set; }

  public async Task SetPageDataAsync(string? c = null)
  {
    var urlsQuery = _urlService.Table.AsQueryable();

    if (User.Identity?.IsAuthenticated ?? false)
      urlsQuery = urlsQuery.Where(x => x.UserId == User.GetUserId()).OrderByDescending(x => x.CreatedDate);
    else
      urlsQuery = urlsQuery.Where(x => x.Code == c);

    Urls = await urlsQuery.ToArrayAsync();
  }

  public async Task OnGetAsync(string? c = null)
  {
    await SetPageDataAsync(c);

    if (TempData.ContainsKey("urlStatusMessage"))
      StatusMessage = TempData["urlStatusMessage"]?.ToString();
  }

  public async Task<IActionResult> OnPostAsync(string? c = null)
  {
    await SetPageDataAsync(c);

    if (LongUrl?.Contains(Request.Host.Value) ?? false)
    {
      ModelState.AddModelError(string.Empty, "Kısaltılmış bir URL'i tekrar kısaltamazsınız.");
      return Page();
    }

    if (!ModelState.IsValid)
      return Page();

    if (Urls?.Where(x => x.Url == LongUrl).Any() ?? false)
    {
      ModelState.AddModelError(string.Empty, "Bu URL kaydını daha önce eklediniz.");
      return Page();
    }

    ShortedUrl newUrl = new()
    {
      Code = ShortedUrl.GenerateCode(),
      UserId = User.GetUserId(),
      Url = LongUrl!,
    };
    await _urlService.CreateAsync(newUrl);
    await _urlService.SaveAsync();

    TempData["urlStatusMessage"] = "URL başarıyla eklendi.,success";

    return RedirectToPage("", new { c = newUrl.Code });
  }
}