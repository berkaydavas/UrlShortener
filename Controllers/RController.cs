using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class RController : Controller
    {
        // GET: R
        [Route("r/{id}")]
        public ActionResult Index(string id)
        {
            using (ApplicationDbContext Db = new ApplicationDbContext())
            {
                ShortedUrl Url = Db.ShortedUrls.Where(x => x.Code == id && x.IsActive == true).FirstOrDefault();

                if (Url == null)
                    return HttpNotFound();

                Url.Click += 1;

                Db.SaveChanges();

                return Redirect(Url.Url);
            }
        }
    }
}