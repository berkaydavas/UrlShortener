using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UrlShortener.Models
{
    public class ShortedUrl
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}, en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 1)]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Url]
        [Required]
        [StringLength(9999, ErrorMessage = "{0}, en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 1)]
        public string Url { get; set; }

        public int Click { get; set; }

        public bool IsActive { get; set; }
    }
}