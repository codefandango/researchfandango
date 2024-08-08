using CodeFandango.Flamenco.Models.Customers;
using CodeFandango.Flamenco.Web.Portal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFandango.Flamenco.Web.Portal.Controllers.API
{
    [Authorize]
    [Route("api/customers")]
    public class CustomersApiController : FlamencoApiController
    {
        private readonly ICustomerService customers;

        public CustomersApiController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers() => SuccessResponse(await customers.GetObjects());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(long id) => SuccessResponse(await customers.GetObject(id));

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerModel customer) => SuccessResponse(await customers.CreateOrUpdateObject(customer));

        [HttpGet("definition")]
        public async Task<IActionResult> GetCustomerDefinition() => SuccessResponse(await customers.GetDefinition());
    }
}
