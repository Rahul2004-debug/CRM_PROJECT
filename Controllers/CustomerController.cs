using CRM.API.Interfaces;
using CRM.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAll();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var result = await _service.Add(customer);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            var result = await _service.Update(customer);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}