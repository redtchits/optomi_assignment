using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // Get all customers
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new Customer().GetCustomers());
        }

        // Get customer by id
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(new Customer().GetCustomers(id));
        }

        // Add a new customer to datatbase
        [HttpPost]
        public ActionResult Post([FromBody] Person v)
        {
            return Ok(new Customer().AddCustomer(v));
        }

        // Update customer by id
        [HttpPut]
        public ActionResult Put([FromBody] Person v)
        {
            return Ok(new Customer().UpdateCustomer(v));
        }

        // Delete customer by id
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(new Customer().DeleteCustomer(id));
        }
    }
}
