using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBillingApp.Models
{
    public class Article
    {
        [Key]
        public int articleId { get; set; }

        [Required]
        [Display(Name = "Article Name", Description = "Article")]
        public string articleName { get; set; }

        [Required]
        [Range(0.01, 1000, ErrorMessage = "Not Acceptable")]
        [Display(Name = "Price", Description = "Price")]
        public decimal articlePrice { get; set; }
    }
}