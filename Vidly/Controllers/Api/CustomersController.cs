using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        ApplicationDbContext _contextCustomer;

        public CustomersController()
        {
            _contextCustomer = new ApplicationDbContext();   
        }


        //GET api/customers
        [HttpGet]
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _contextCustomer.Customers
              .Include(c => c.MemberShipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.customerName.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        //GET api/customers/1
        [HttpGet]
        public IHttpActionResult GetCustomer(int id) {
            var customer = _contextCustomer.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
               return BadRequest();

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        //POST api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _contextCustomer.Customers.Add(customer);
            _contextCustomer.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id),customerDto);
        }

        // PUT api/customers
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerinDB = _contextCustomer.Customers.SingleOrDefault(c => c.Id == id);

            if (customerinDB == null)
                return NotFound();

            Mapper.Map(customerDto, customerinDB);
            _contextCustomer.SaveChanges();
            return Ok();
        }

        //DELETE api/customer/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerinDB = _contextCustomer.Customers.SingleOrDefault(c => c.Id == id);

            if (!ModelState.IsValid)
                return BadRequest();

            if (customerinDB == null)
                return NotFound();

            _contextCustomer.Customers.Remove(customerinDB);
            _contextCustomer.SaveChanges();
            return Ok();
        }

    }
}
