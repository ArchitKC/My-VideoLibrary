using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels; 

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        ApplicationDbContext _context;
        // GET: Customers
        public CustomersController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult CustomerForm()
        {
            var membershipType = _context.MemberShipTypes.ToList();
            var ViewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MemberShipTypes = membershipType
            };
            return View(ViewModel);
        }
        public ViewResult Index()
        {
            //var customers = _context.Customers.Include(c=>c.MemberShipType).ToList(); 

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MemberShipTypes = _context.MemberShipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerinDB = _context.Customers.Single(c => c.Id == customer.Id);
                customerinDB.customerName = customer.customerName;
                customerinDB.BirthDate = customer.BirthDate;
                customerinDB.IsSubscriberToNewsLetter = customer.IsSubscriberToNewsLetter;
                customerinDB.MemberShipTypeId = customer.MemberShipTypeId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MemberShipTypes = _context.MemberShipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}