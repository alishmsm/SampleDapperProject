using Microsoft.AspNetCore.Mvc;
using Repository;

namespace DapperApp.Controllers;

public class CustomerController : Controller
{

    private readonly ICustomerRepository _customerRepository;
    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }


    // public IActionResult GetAll()
    // {
    //     var result = _customerRepository.GetCustomers();
    //     return View(result);
    // }

        public ActionResult Index()
        {

            return View(_customerRepository.GetCustomers());
            //_customerRepository.Add(new CustomerDto
            //{
            //    Name = "Ali",
            //    LastName = "Karimi",
            //});


            //_customerRepository.Add(new List<CustomerDto>()
            //{
            //  new CustomerDto
            //  {
            //        Name = "Ali",
            //    LastName = "Ahmadi",
            //  },
            //  new CustomerDto
            //  {
            //     Name = "Jamshid",
            //    LastName = "Jahani",
            //  },
            //});


            //_customerRepository.Delete(1);

            //_customerRepository.Update(new CustomerDto
            //{
            //    Id = 2,
            //    Name = "Sasan",
            //    LastName = "Safi"
            //});


        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View(_customerRepository.Find(id));
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerDto customer)
        {
            try
            {
                _customerRepository.Add(customer);
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View( _customerRepository.Find(id));
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( CustomerDto customer)
        {
            try
            {
                _customerRepository.Update(customer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_customerRepository.Find(id));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomerDto customer)
        {
            try
            {
                _customerRepository.Delete(customer.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
}