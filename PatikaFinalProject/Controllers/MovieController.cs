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
    public class MovieController : ControllerBase
    {
        ShoppingListService shoppingListService;

        private readonly ILogger<ShoppingListService> logger;

        public MovieController(ILogger<ShoppingListService> logger, ShoppingListService service)
        {
            shoppingListService = service;
            this.logger = logger;
        }

        [HttpPost(Name = "AddMovie")]
        public async Task<IResponse<ShoppingListCreateDTO>> AddMovie(ShoppingListCreateDTO newShoppingList)
        {
            return await shoppingListService.Create(newShoppingList);
        }
        
        [HttpDelete(Name = "DeleteMovie")]
        public async Task<IResponse> DeleteMovie(int id)
        {
            return await shoppingListService.Remove(id);
        }

        [HttpPut(Name = "UpdateMovie")]
        public async Task<IResponse<ShoppingListDTO>> UpdateMovie(ShoppingListDTO newMovie)
        {
            return await shoppingListService.Update(newMovie);
        }

        [HttpGet("GetAllMovies")]
        public async Task<IResponse> GetMovies( )
        {
            return await shoppingListService.GetAll();
        }
    }
}