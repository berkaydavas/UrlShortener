using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Abstract;
using UrlShortener.BLL.Extensions;
using UrlShortener.DAL.Entities;

namespace UrlShortener.WEB.Pages.Url;

public class DeleteModel : PageModel
{
  private readonly IShortedUrlService _urlService;

  public DeleteModel(IShortedUrlService urlService)
  {
    _urlService = urlService;
  }

  public async Task<IActionResult> OnPostAsync(string? code)
  {
    List<ShortedUrl> urls = await _urlService.GetWhere(x => x.IsActive && x.UserId == User.GetUserId() && (code == null || x.Code == code)).ToListAsync();

    urls.ForEach(url => _urlService.Delete(url));
    await _urlService.SaveAsync();

    TempData["urlStatusMessage"] = string.IsNullOrEmpty(code) ? "Tüm URL kayýtlarý baþarýyla silindi." : $"{code} kodlu URL baþarýyla silindi.,success";

    return RedirectToPage("/Index");
  }
}