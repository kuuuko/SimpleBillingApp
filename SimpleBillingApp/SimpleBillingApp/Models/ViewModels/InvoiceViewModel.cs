using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBillingApp.Models.ViewModels
{
    public class InvoiceViewModel
    {
        [Display(Name = "Invoice Code", Description = "Invoice Code")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0000000000}")]
        public int viewInvoiceId { get; set; }

        [Display(Name = "Name", Description = "Customer Name")]
        public string viewCustomerName { get; set; }

        [Display(Name = "Date", Description = "Invoice Date")]
        public DateTime viewInvoiceDate { get; set; }

        [Display(Name = "Adress", Description = "Adress")]
        public string viewCustomerAdress { get; set; }

        [Display(Name = "Contact Info", Description = "Contact Info")]
        public string viewCustomerContact { get; set; }
    }
}