using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UrlShortener.Models;

namespace UrlShortener.ViewModels
{
    public class HomeViewModel
    {
        [Display(Name = "Kısaltılacak Link")]
        [Url(ErrorMessage = "{0}, bir link olmalıdır.")]
        [Required]
        [StringLength(9999, ErrorMessage = "{0}, en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 1)]
        public string LongUrl { get; set; }

        public ShortedUrl Url { get; set; }

        public List<ShortedUrl> Urls { get; set; }
    }
}