using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBillingApp.Models.ViewModels
{
    public class TransactionViewModel
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0000000000}")]
        [Display(Name = "Transaction Code", Description = "Transaction Code")]
        public int viewTransactionId { get; set; }

        [Display(Name = "Article Code", Description = "Article Code")]
        public int viewArticleId { get; set; }

        [Range(1, 100, ErrorMessage="Not Acceptable")]
        [Display(Name = "Quantity", Description = "Quantity")]
        public int quantity { get; set; }

        [Display(Name = "Price", Description = "Price")]
        public decimal viewPrice { get; set; }

        [Display(Name = "Article", Description = "Article")]
        public string viewArticleName { get; set; }
        
    }
}