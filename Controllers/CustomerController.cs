using CRM.API.Interfaces;
using CRM.API.Models;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Authorization;
>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
<<<<<<< HEAD
=======
    [Authorize] // user must be logged in
>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

<<<<<<< HEAD
        [HttpGet]
=======
        // Admin + SalesRep can view
        [HttpGet]
        [Authorize(Roles = "Admin,SalesRep")]
>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAll();
            return Ok(customers);
        }

<<<<<<< HEAD
        [HttpPost]
=======
        // Admin + SalesRep can create
        [HttpPost]
        [Authorize(Roles = "Admin,SalesRep")]
>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc
        public async Task<IActionResult> Create(Customer customer)
        {
            var result = await _service.Add(customer);
            return Ok(result);
        }

<<<<<<< HEAD
        [HttpPut]
=======
        // Only Admin can update
        [HttpPut]
        [Authorize(Roles = "Admin")]
>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc
        public async Task<IActionResult> Update(Customer customer)
        {
            var result = await _service.Update(customer);
            return Ok(result);
        }

<<<<<<< HEAD
        [HttpDelete("{id}")]
=======
        // Only Admin can delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}