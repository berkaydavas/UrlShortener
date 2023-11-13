using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener.BLL.Abstract;
using UrlShortener.BLL.Extensions;
using UrlShortener.DAL.Entities;

namespace UrlShortener.WEB.Pages.Url;

public class ToggleModel : PageModel
{
  private readonly IShortedUrlService _urlService;

  public ToggleModel(IShortedUrlService urlService)
  {
    _urlService = urlService;
  }

  public async Task<IActionResult> OnPostActiveAsync(string code)
  {
    ShortedUrl? url = await _urlService.GetAsync(x => !x.IsActive && x.UserId == User.GetUserId() && x.Code == code);
    if (url == null)
    {
      TempData["urlStatusMessage"] = "Kayýt bulunamadý.,danger";
      return RedirectToPage("/Index");
    }

    url.IsActive = true;
    _urlService.Update(url);
    await _urlService.SaveAsync();

    TempData["urlStatusMessage"] = $"{code} kodlu URL aktif.,success";

    return RedirectToPage("/Index");
  }

  public async Task<IActionResult> OnPostPassiveAsync(string code)
  {
    ShortedUrl? url = await _urlService.GetAsync(x => x.IsActive && x.UserId == User.GetUserId() && x.Code == code);
    if (url == null)
    {
      TempData["urlStatusMessage"] = "Kayýt bulunamadý.,danger";
      return RedirectToPage("/Index");
    }

    url.IsActive = false;
    _urlService.Update(url);
    await _urlService.SaveAsync();

    TempData["urlStatusMessage"] = $"{code} kodlu URL pasif.,success";

    return RedirectToPage("/Index");
  }
}