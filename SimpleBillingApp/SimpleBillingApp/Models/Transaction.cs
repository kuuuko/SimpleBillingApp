using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBillingApp.Models
{
    public class Transaction
    {
        [Key]
        public int transactionId { get; set; }

        [Required]
        public int articleId { get; set; }

        [Required]
        public int invoiceId { get; set; }

        [Required]
        [Range(1, 100,ErrorMessage="Not Acceptable")]
        public int quantity { get; set; }

        public void removeTransaction()
        {
            using (MyDBContext db = new MyDBContext())
            {
                db.Transactions.Attach(this);
                db.Transactions.Remove(this);
                db.SaveChanges();
            }
        }
    }
}