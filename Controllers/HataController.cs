using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlShortener.ViewModels;

namespace UrlShortener.App_Start
{
    public class HataController : Controller
    {
        // GET: Hata
        public ActionResult Index()
        {
            ErrorViewModel ViewModel = new ErrorViewModel()
            {
                SiteTitle = "Hata",
                PageTitle = "Hata!",
                ErrorDesc = "İsteğiniz işlenirken bir hata oluştu."
            };

            return View(ViewModel);
        }

        // GET: Hata
        [Route("Hata/{type}")]
        public ActionResult Index(int type)
        {
            ErrorViewModel ViewModel = new ErrorViewModel()
            {
                SiteTitle = "Hata",
                PageTitle = "Hata!",
                ErrorDesc = "İsteğiniz işlenirken bir hata oluştu."
            };

            switch (type)
            {
                case 404:
                    ViewModel.SiteTitle = "Sayfa Bulunamadı";
                    ViewModel.PageTitle = "Hata 404!";
                    ViewModel.ErrorDesc = "Aradığınız kaynak kaldırılmış, adı değiştirilmiş ya da geçici olarak kullanım dışı.";
                    break;
                case 403:
                    ViewModel.SiteTitle = "Erişim Yetkiniz Yok";
                    ViewModel.PageTitle = "Hata 403!";
                    ViewModel.ErrorDesc = "Bu sayfa içeriğini görmek için yetkiniz yok.";
                    break;
            }

            Response.StatusCode = type;
            Response.TrySkipIisCustomErrors = true;

            return View(ViewModel);
        }
    }
}