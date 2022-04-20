using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TheLibrary.Data;
using TheLibrary.Models;
using TheLibrary.ViewModels;

namespace TheLibrary.Controllers
{
    public class CustomersController : Controller
    {
        private TheLibraryDbContext _theLibraryDbContext;

        public CustomersController()
        {
            _theLibraryDbContext = new TheLibraryDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _theLibraryDbContext.Dispose();
        }

        public IActionResult New()
        {
            var membershipTypes = _theLibraryDbContext.MembershipTypes.ToList();

            var customerFormViewModel = new CustomerFormViewModel
            { 
                Customer = new Customer(),
                MembershipTypes = membershipTypes,
            };

            return View("CustomerForm", customerFormViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var customerFormViewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _theLibraryDbContext.MembershipTypes.ToList(),
                };

                return View("CustomerForm", customerFormViewModel);
            }

            if(customer.Id == 0)
            {
                _theLibraryDbContext.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _theLibraryDbContext.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            _theLibraryDbContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        
        public IActionResult Edit(int id)
        {
            var customer = _theLibraryDbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var customerFormViewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _theLibraryDbContext.MembershipTypes.ToList(),
            };

            return View("CustomerForm", customerFormViewModel);
        }

        public ViewResult Index()
        {
            //var customers = _theLibraryDbContext.Customers.Include(c => c.MembershipType).ToList();

            return View(/*customers*/);
        }

        public IActionResult Details(int id)
        {
            var customer = _theLibraryDbContext.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // Old methods to get customers (pre db)
        private IEnumerable<Customer> GetCustomers()
        {
            yield return new Customer { Id = 1, Name = "Customer1", };
            yield return new Customer { Id = 2, Name = "Customer2", };
            yield return new Customer { Id = 3, Name = "Customer3", };
        }
    }
}
