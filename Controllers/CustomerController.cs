using CRM.API.Interfaces;
using CRM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // user must be logged in
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // Admin + SalesRep can view
        [HttpGet]
        [Authorize(Roles = "Admin,SalesRep")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAll();
            return Ok(customers);
        }

        // Admin + SalesRep can create
        [HttpPost]
        [Authorize(Roles = "Admin,SalesRep")]
        public async Task<IActionResult> Create(Customer customer)
        {
            var result = await _service.Add(customer);
            return Ok(result);
        }

        // Only Admin can update
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Customer customer)
        {
            var result = await _service.Update(customer);
            return Ok(result);
        }

        // Only Admin can delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}