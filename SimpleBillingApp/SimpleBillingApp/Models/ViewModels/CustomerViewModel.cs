using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBillingApp.Models.ViewModels
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer Code", Description = "Customer Code")]
        public int viewCustomerId { get; set; }

        [Display(Name = "Name", Description = "Name")]
        public string viewCustomerName { get; set; }

        [Display(Name = "Adress", Description = "Adress")]
        public string viewCustomerAdress { get; set; }

        [Display(Name = "Contact Info", Description = "Contact Info")]
        public string viewCustomerContact { get; set; } 
    }
}