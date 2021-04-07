using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UrlShortener.Models;

namespace UrlShortener.Helpers
{
    public class UserData
    {
        private static ApplicationUserManager _userManager;

        public static ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public static ApplicationUser GetActiveUser()
        {
            return UserManager.FindByName(HttpContext.Current.User.Identity.Name);
        }

        public static ApplicationUser GetUserById(string Id)
        {
            return UserManager.FindById(Id);
        }
    }
}