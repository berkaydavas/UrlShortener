using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlShortener.Helpers;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [Authorize]
    public class UrlController : Controller
    {
        // POST: Url/DeleteAll
        [HttpPost]
        public ActionResult DeleteAll()
        {
            string ActiveUserId = UserData.GetActiveUser().Id;

            using (ApplicationDbContext Db = new ApplicationDbContext())
            {
                try
                {
                    List<ShortedUrl> Urls = Db.ShortedUrls.Where(x => x.UserId == ActiveUserId).ToList();

                    if (Urls.Count == 0)
                    {
                        return Json(JsonConvert.SerializeObject(new
                        {
                            title = "Uyarı!",
                            text = "Herhangi bir kayıt bulunamadı.",
                            icon = "warning"
                        }));
                    }

                    Db.ShortedUrls.RemoveRange(Urls);
                    Db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new
                    {
                        title = "Başarılı",
                        text = "Tüm kayıtlar silindi.",
                        icon = "success"
                    }));
                }
                catch (Exception e)
                {
                    return Json(JsonConvert.SerializeObject(new
                    {
                        title = "Hata!",
                        text = "Beklenmeyen bir hata oluştu. Hata: " + e.HResult + " Hata mesajı: " + e.Message,
                        icon = "error"
                    }));
                }
            }
        }

        // POST: Url/Delete
        [HttpPost]
        public ActionResult Delete()
        {
            string Code = Request["code"];
            string ActiveUserId = UserData.GetActiveUser().Id;

            using (ApplicationDbContext Db = new ApplicationDbContext())
            {
                try
                {
                    ShortedUrl Url = Db.ShortedUrls.Where(x => x.Code == Code && x.UserId == ActiveUserId).FirstOrDefault();

                    if (Url == null)
                    {
                        return Json(JsonConvert.SerializeObject(new
                        {
                            title = "Uyarı!",
                            text = "Böyle bir kayıt bulunamadı.",
                            icon = "warning"
                        }));
                    }

                    Db.ShortedUrls.Remove(Url);
                    Db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new
                    {
                        title = "Başarılı",
                        text = "Kayıt silindi.",
                        icon = "success"
                    }));
                }
                catch (Exception e)
                {
                    return Json(JsonConvert.SerializeObject(new
                    {
                        title = "Hata!",
                        text = "Beklenmeyen bir hata oluştu. Hata: " + e.HResult + " Hata mesajı: " + e.Message,
                        icon = "error"
                    }));
                }
            }
        }

        // POST: Url/Edit
        [HttpPost]
        public ActionResult Edit()
        {
            string Code = Request["code"];
            string NewCode = Request["newCode"];
            string ActiveUserId = UserData.GetActiveUser().Id;

            using (ApplicationDbContext Db = new ApplicationDbContext())
            {
                try
                {
                    ShortedUrl Url = Db.ShortedUrls.Where(x => x.Code == Code && x.UserId == ActiveUserId).FirstOrDefault();

                    if (Url == null)
                    {
                        return Json(JsonConvert.SerializeObject(new
                        {
                            title = "Uyarı!",
                            text = "Böyle bir kayıt bulunamadı.",
                            icon = "warning"
                        }));
                    }

                    Url.Code = NewCode.Replace(" ", "_").Replace("\"", "").Trim();

                    Db.SaveChanges();

                    return Json(JsonConvert.SerializeObject(new
                    {
                        title = "Başarılı",
                        text = "Kayıt düzenlendi.",
                        icon = "success"
                    }));
                }
                catch (Exception e)
                {
                    return Json(JsonConvert.SerializeObject(new
                    {
                        title = "Hata!",
                        text = "Beklenmeyen bir hata oluştu. Hata: " + e.HResult + " Hata mesajı: " + e.Message,
                        icon = "error"
                    }));
                }
            }
        }

        // GET: Url/Passive
        public ActionResult Passive(string id)
        {
            string ActiveUserId = UserData.GetActiveUser().Id;

            using (ApplicationDbContext Db = new ApplicationDbContext())
            {
                try
                {
                    ShortedUrl Url = Db.ShortedUrls.Where(x => x.Code == id && x.UserId == ActiveUserId).FirstOrDefault();

                    if (Url == null)
                        return HttpNotFound();

                    Url.IsActive = false;

                    Db.SaveChanges();

                    return RedirectToAction("Index", "Home", new { m = "Passive", c = Url.Code });
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index", "Home", new { m = "Error" });
                }
            }
        }

        // GET: Url/Active
        public ActionResult Active(string id)
        {
            string ActiveUserId = UserData.GetActiveUser().Id;

            using (ApplicationDbContext Db = new ApplicationDbContext())
            {
                try
                {
                    ShortedUrl Url = Db.ShortedUrls.Where(x => x.Code == id && x.UserId == ActiveUserId).FirstOrDefault();

                    if (Url == null)
                        return HttpNotFound();

                    Url.IsActive = true;

                    Db.SaveChanges();

                    return RedirectToAction("Index", "Home", new { m = "Active", c = Url.Code });
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index", "Home", new { m = "Error" });
                }
            }
        }
    }
}