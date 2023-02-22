using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PatikaFinalProject.Bussiness.Services;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;
using System.Data;

namespace PatikaFinalProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        CustomerService _customerService;

        private readonly ILogger<CustomerService> _logger;

        public CustomerController(ILogger<CustomerService> logger, CustomerService service)
        {
            _customerService = service;
            _logger = logger;
        }

        [HttpPost(Name = "AddCustomer")]
        public async Task<IResponse<CustomerCreateDTO>> AddCustomer(CustomerCreateDTO newCus)
        {
            return await _customerService.Create(newCus);
        }
        
        [HttpDelete(Name = "DeleteCustomer")]
        public async Task<IResponse> DeleteCustomer(int id)
        {
            return await _customerService.Remove(id);
        }
    }
}