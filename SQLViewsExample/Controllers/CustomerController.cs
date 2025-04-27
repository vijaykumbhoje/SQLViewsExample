using Microsoft.AspNetCore.Mvc;
using SQLViewsExample.Application.Interfaces;
using SQLViewsExample.Domain.Entities;

namespace SQLViewsExample.API.Controllers
{
    public class CustomerController : Controller
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CustomersController : ControllerBase
        {
            private readonly ICustomerService _customerService;

            public CustomersController(ICustomerService customerService)
            {
                _customerService = customerService;
            }

            [HttpGet("summaries")]
            public async Task<IActionResult> GetCustomerSummaries()
            {
                var summaries = await _customerService.GetCustomerSummariesAsync();
                return Ok(summaries);
            }

            [HttpGet("summaries/{customerId}")]
            public async Task<IActionResult> GetCustomerSummary(int customerId)
            {
                var summary = await _customerService.GetCustomerSummaryAsync(customerId);
                if (summary == null) return NotFound();
                return Ok(summary);
            }

            [HttpPost]
            public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
            {
                await _customerService.AddCustomerAsync(customer);
                return CreatedAtAction(nameof(GetCustomerSummary),
                    new { customerId = customer.Id }, customer);
            }

            [HttpPost("{customerId}/orders")]
            public async Task<IActionResult> AddOrder(int customerId, [FromBody] Order order)
            {
                order.CustomerId = customerId;
                await _customerService.AddOrderAsync(order);
                return CreatedAtAction(nameof(GetCustomerSummary),
                    new { customerId }, order);
            }
        }

    }
}
