using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBillingApp.Models
{
    public class Invoice
    {
        [Key]
        public int invoiceId { get; set; }

        public int customerId { get; set; }

        [Required]
        public DateTime invoiceDate { get; set; }

        public void removeInvoice()
        {
            using (MyDBContext db = new MyDBContext())
            {
                var transactions = db.Transactions.Where(x => x.invoiceId == invoiceId);
                db.Invoices.Attach(this);
                db.Invoices.Remove(this);
                db.SaveChanges();
                foreach (var transaction in transactions)
                {
                    transaction.removeTransaction();
                }
            }
        }
    }
}