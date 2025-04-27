using Microsoft.AspNetCore.Mvc;
using SQLViewsExample.Application.Services;

namespace SQLViewsExample.API.Controllers
{
    public class CustomersViewController
    {
        // API/Controllers/CustomerViewsController.cs
        [ApiController]
        [Route("api/customer-views")]
        public class CustomerViewsController : ControllerBase
        {
            private readonly CustomerViewService _service;

            public CustomerViewsController(CustomerViewService service)
            {
                _service = service;
            }

            [HttpGet]
            public IActionResult QueryCustomers(
                [FromQuery] decimal? minAmount,
                [FromQuery] int? topRecords)
            {
                var query = _service.GetQueryable();

                if (minAmount.HasValue)
                {
                    query = query.Where(c => c.TotalSpent >= minAmount.Value);
                }

                if (topRecords.HasValue)
                {
                    query = query.Take(topRecords.Value);
                }

                return Ok(query.ToList());
            }

            [HttpGet("high-value")]
            public async Task<IActionResult> GetHighValueCustomers()
            {
                var result = await _service.GetTopSpendersAsync(10);
                return Ok(result);
            }
        }
    }
}

