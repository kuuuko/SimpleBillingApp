using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBillingApp.Models
{
    public class MyDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Article> Articles { get; set; }
    }
}