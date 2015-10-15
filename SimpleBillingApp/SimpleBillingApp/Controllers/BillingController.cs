using SimpleBillingApp.Models;
using SimpleBillingApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBillingApp.Controllers
{
    public class BillingController : Controller
    {
        MyDBContext db = new MyDBContext();

        public ActionResult InvoiceList(int customerId)
        {
            List<InvoiceViewModel> invoiceList = new List<InvoiceViewModel>();
            var dbInvoiceList = db.Invoices.Where(i => i.customerId == customerId);
            foreach (var invoice in dbInvoiceList)
            {
                using (MyDBContext dbCustomer = new MyDBContext())
                {
                    var customer = dbCustomer.Customers.Find(invoice.customerId);
                    invoiceList.Add(new InvoiceViewModel
                    {
                        viewInvoiceId = invoice.invoiceId,
                        viewCustomerName = customer.customerName,
                        viewInvoiceDate = invoice.invoiceDate,
                        viewCustomerAdress = customer.customerAdress,
                        viewCustomerContact = customer.customerContact
                    });
                }
            }
            return PartialView("_invoiceList", invoiceList);
        }

        public ActionResult InvoiceDetails(int invoiceId)
        {
            Invoice invoice = db.Invoices.Find(invoiceId);
            var customer = db.Customers.Find(invoice.customerId);

            if (invoice == null)
            {
                return HttpNotFound();
            }

            ViewData["customerId"] = invoice.customerId;
            return View(new InvoiceViewModel
            {
                viewCustomerName = customer.customerName,
                viewInvoiceId = invoice.invoiceId,
                viewInvoiceDate = invoice.invoiceDate,
                viewCustomerAdress = customer.customerAdress,
                viewCustomerContact = customer.customerContact
            });
        }

        [HttpGet]
        public ActionResult AddInvoice(int customerId)
        {
            Session["TransactionList"] = new List<TransactionViewModel>();
            Session["currentTransactionId"] = 0;
            ViewData["customerId"] = customerId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInvoice(InvoiceViewModel newInvoice, int customerId)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(new Invoice { customerId = customerId, });
                db.SaveChanges();
                return RedirectToAction("InvoiceList", "Billing", new { customerId = customerId });
            }
            return View(newInvoice);
        }

        public ActionResult DeleteInvoice(int invoiceId)
        {
            Invoice invoice = db.Invoices.Find(invoiceId);
            invoice.removeInvoice();
            return RedirectToAction("CustomerDetails", "Customer", new { customerId = invoice.customerId });
        }

        public ActionResult SaveInvoice(int customerId)
        {
            Invoice newInvoice = new Invoice { customerId = customerId, invoiceDate = System.DateTime.Now };
            db.Invoices.Add(newInvoice);
            db.SaveChanges();
            var transactionList = (List<TransactionViewModel>)Session["TransactionList"];

            foreach (var transaction in transactionList)
            {
                db.Transactions.Add(new Transaction
                {
                    articleId = transaction.viewArticleId,
                    invoiceId = newInvoice.invoiceId,
                    quantity = transaction.quantity
                });
                db.SaveChanges();
            }

            return RedirectToAction("CustomerDetails", "Customer", new { customerId = customerId });
        }

        public ActionResult PostTransactionField()
        {
            ViewData["Articles"] = db.Articles.ToList();
            TransactionViewModel newTransaction = new TransactionViewModel();
            return PartialView("_postTransactionField", newTransaction);
        }

        [HttpPost]
        public ActionResult PostTransaction(TransactionViewModel newTransaction)
        {
            if (ModelState.IsValid)
            {
                var article = db.Articles.Find(newTransaction.viewArticleId);
                string articleName = article.articleName;
                decimal articlePrice = article.articlePrice;
                var transactionList = (List<TransactionViewModel>)Session["TransactionList"];
                Session["currentTransactionId"] = (int)Session["currentTransactionId"] + 1;

                newTransaction.viewTransactionId = ((int)Session["currentTransactionId"]);
                newTransaction.viewArticleName = articleName;
                newTransaction.viewPrice = articlePrice;
                transactionList.Add(newTransaction);
            }

            return CurrentTransactions();
        }

        public ActionResult InvoiceTransactions(int invoiceId)
        {
            List<Transaction> transactionList = db.Transactions.Where(t => t.invoiceId == invoiceId).ToList();
            List<TransactionViewModel> viewTransactionList = new List<TransactionViewModel>();

            foreach (var transaction in transactionList)
            {
                using (MyDBContext dbArticle = new MyDBContext())
                {
                    var article = dbArticle.Articles.Find(transaction.articleId);
                    viewTransactionList.Add(new TransactionViewModel
                    {
                        viewTransactionId = transaction.transactionId,
                        viewArticleName = article.articleName,
                        viewPrice = article.articlePrice,
                        quantity = transaction.quantity
                    });
                }
            }
            return PartialView("_invoiceTransactions", viewTransactionList);
        }

        public ActionResult CurrentTransactions()
        {
            var currentTransactios = (List<TransactionViewModel>)Session["TransactionList"];
            return PartialView("_currentTransactions", currentTransactios);
        }

        public ActionResult RemoveCurrentTransaction(int viewTransactionId)
        {
            ((List<TransactionViewModel>)Session["TransactionList"]).RemoveAll(item => item.viewTransactionId == viewTransactionId);
            return CurrentTransactions();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
