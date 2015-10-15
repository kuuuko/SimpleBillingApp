using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBillingApp.Models
{
    public class Customer
    {
        [Key]
        public int customerId { get; set; }

        [Required]
        [Display(Name = "Customer Name", Description = "Customer Name")]
        public string customerName { get; set; }

        [Required]
        [Display(Name = "Customer Adress", Description = "Customer Adress")]
        public string customerAdress { get; set; }

        [Required]
        [Display(Name = "Contact Info", Description = "Contact Info")]
        public string customerContact { get; set; }

        public void removeCustomer()
        {
            using (MyDBContext db = new MyDBContext())
            {
                var invoices = db.Invoices.Where(x => x.customerId == customerId);
                db.Customers.Attach(this);
                db.Customers.Remove(this);
                db.SaveChanges();
                foreach (var invoice in invoices)
                {
                    invoice.removeInvoice();
                }
            }
        }
    }
}