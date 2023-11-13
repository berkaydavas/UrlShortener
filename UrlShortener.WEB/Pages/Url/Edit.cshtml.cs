using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener.BLL.Abstract;
using UrlShortener.BLL.Extensions;
using UrlShortener.DAL.Entities;

namespace UrlShortener.WEB.Pages.Url;

public class EditModel : PageModel
{
  private readonly IShortedUrlService _urlService;

  public EditModel(IShortedUrlService urlService)
  {
    _urlService = urlService;
  }

  [BindProperty, Required]
  public string NewCode { get; set; } = null!;

  public async Task<IActionResult> OnPostAsync(string code)
  {
    ShortedUrl? url = await _urlService.GetAsync(x => x.IsActive && x.UserId == User.GetUserId() && x.Code == code);
    if (url == null)
    {
      TempData["urlStatusMessage"] = "Kayýt bulunamadý.,danger";
      return RedirectToPage("/Index");
    }

    url.Code = NewCode.Replace(" ", "_").Replace("\"", "").Trim();
    _urlService.Update(url);
    await _urlService.SaveAsync();

    return RedirectToPage("/Index");
  }
}