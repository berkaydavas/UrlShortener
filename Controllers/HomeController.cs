using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlShortener.Helpers;
using UrlShortener.Models;
using UrlShortener.ViewModels;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(UrlResults? m, string c)
        {
            ViewBag.StatusMessage =
                m == UrlResults.Success ? (string.IsNullOrEmpty(c) ? "" : c + " kodlu ") + "URL başarıyla eklendi.,success"
                : m == UrlResults.Active ? (string.IsNullOrEmpty(c) ? "" : c + " kodlu ") + "URL aktifleştirildi.,success"
                : m == UrlResults.Passive ? (string.IsNullOrEmpty(c) ? "" : c + " kodlu ") + "URL pasifleştirildi.,success"
                : m == UrlResults.Error ? "Hata oluştu.,danger"
                : "";

            using (ApplicationDbContext Db = new ApplicationDbContext())
            {
                List<ShortedUrl> Urls = new List<ShortedUrl>();
                ShortedUrl Url = null;

                if (User.Identity.IsAuthenticated)
                {
                    ApplicationUser ActiveUser = UserData.GetActiveUser();

                    Urls = Db.ShortedUrls.Where(x => x.UserId == ActiveUser.Id).OrderByDescending(x => x.Date).ToList();
                }
                else
                {
                    if (!string.IsNullOrEmpty(c))
                    {
                        Url = Db.ShortedUrls.Where(x => x.Code == c).FirstOrDefault();
                    }
                }

                HomeViewModel ViewModel = new HomeViewModel()
                {
                    Urls = Urls,
                    Url = Url
                };

                return View(ViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HomeViewModel Model)
        {
            string ActiveUserId = null;

            bool IsAuthenticated = User.Identity.IsAuthenticated;

            if (IsAuthenticated)
                ActiveUserId = UserData.GetActiveUser().Id;

            using (ApplicationDbContext Db = new ApplicationDbContext())
            {
                HomeViewModel ViewModel = new HomeViewModel()
                {
                    LongUrl = Model.LongUrl
                };

                if (IsAuthenticated)
                    ViewModel.Urls = Db.ShortedUrls.Where(x => x.UserId == ActiveUserId).OrderByDescending(x => x.Date).ToList();

                if (!ModelState.IsValid)
                    return View(ViewModel);

                try
                {
                    if (ViewModel.Urls.Where(x => x.Url == Model.LongUrl).Count() > 0)
                    {
                        ModelState.AddModelError("count_error", "Bu URL kaydını daha önce eklediniz.");
                        return View(ViewModel);
                    }

                    ShortedUrl NewUrl = new ShortedUrl()
                    {
                        Code = Cipher.CreateUrlCode(),
                        Date = DateTime.Now,
                        UserId = ActiveUserId,
                        Url = Model.LongUrl,
                        Click = 0,
                        IsActive = true
                    };
                    Db.ShortedUrls.Add(NewUrl);

                    Db.SaveChanges();

                    return RedirectToAction("Index", new { m = "Success", c = NewUrl.Code });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("exception_error", "Beklenmeyen bir hata oluştu. Hata: " + e.HResult + " Hata mesajı: " + e.Message);

                    return View(ViewModel);
                }
            }
        }

        public ActionResult MemberAdvantages(int layout = 1)
        {
            ViewBag.Layout = layout == 1;

            return View();
        }

        public enum UrlResults
        {
            Success,
            Active,
            Passive,
            Error
        }
    }
}