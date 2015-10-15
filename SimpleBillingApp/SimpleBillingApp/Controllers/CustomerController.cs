using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBillingApp.Models;
using SimpleBillingApp.Models.ViewModels;

namespace SimpleBillingApp.Controllers
{
    public class CustomerController : Controller
    {
        private MyDBContext db = new MyDBContext();


        [HttpGet]
        public ActionResult CustomerList()
        {   
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            var dbCustomerList = db.Customers.ToList();

            foreach(var customer in dbCustomerList)
            {
                customers.Add( new CustomerViewModel {  viewCustomerId = customer.customerId, 
                                                        viewCustomerName = customer.customerName, 
                                                        viewCustomerAdress = customer.customerAdress,
                                                        viewCustomerContact = customer.customerContact});
            }
            return PartialView("_customerList", customers);
        }

        [HttpGet]
        public ActionResult AddCustomer() 
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(Customer newCustomer) 
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(newCustomer);
        }

        [HttpGet]
        public ActionResult CustomerDetails(int customerId )
        {
            Customer dbCustomer = db.Customers.Find(customerId);
            if (dbCustomer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel customer = new CustomerViewModel {    viewCustomerId = dbCustomer.customerId,
                                                                    viewCustomerName = dbCustomer.customerName,
                                                                    viewCustomerAdress = dbCustomer.customerAdress,
                                                                    viewCustomerContact = dbCustomer.customerContact};
            return View(customer);
        }


        [HttpGet]
        public ActionResult EditCustomer(int customerId)
        {
            Customer customer = db.Customers.Find( customerId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("EditCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomerDetails", "Customer", new { customerId = customer.customerId });
            }
            return View( customer);
        }

        public ActionResult DeleteCustomer(int customerId)
        {
            Customer customer = db.Customers.Find( customerId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int customerId)
        {
            Customer customer = db.Customers.Find(customerId);
            customer.removeCustomer();
            return RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}