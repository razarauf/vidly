using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            // .Include(c => c.Membership) // eager loading - loading related objects 
            // by default entity framework only loads customer (main) object
            var customers = _context.Customers.Include(c => c.Membership).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.Membership).SingleOrDefault(x => x.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New ()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var customer = new Customer();

            var customerViewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", customerViewModel);
        }

        public ActionResult Edit(int id)
        {

            var customer = _context.Customers.Include(c => c.Membership).SingleOrDefault(x => x.Id == id);

            if (customer == null)
                return HttpNotFound();

            var customerViewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", customerViewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                // Approach #1: 
                // official microsoft suggest the following with property names whitelisted
                // but "magic strings":
                // TryUpdateModel(customerInDb, string.Empty, new string[] { "Name", "Email" });

                //Approach #2: manually set each individual library
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                //Approach #3: auto-mapper
                // Mapper.Map(customer, customerInDb);
                // pass a "data transfer object" to action which only includes properties that
                // needed to updated
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}