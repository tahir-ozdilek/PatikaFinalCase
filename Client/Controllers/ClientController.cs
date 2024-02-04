using Microsoft.AspNetCore.Mvc;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;
using Refit;

namespace Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWithHttpClient")]
        public async Task<dynamic> GetWithHttpClient()
        {
            using HttpClient client = new HttpClient();
           
            var response = await client.GetAsync(@"http://localhost:5122/ShoppingList/GetAllCompletedShoppingLists");

            return await response.Content.ReadAsStringAsync();
        }


        [HttpGet("GetwithRefit")]
        public async Task<dynamic> GetwithRefit()
        {
            var refitClient = RestService.For<RefitApi>("http://localhost:5122");

            var response = await refitClient.GetShoppingList();

            return response;
        }
    }

    public interface RefitApi
    {
        [Get("/ShoppingList/GetAllCompletedShoppingLists")]
        Task<dynamic> GetShoppingList();
    }
}
