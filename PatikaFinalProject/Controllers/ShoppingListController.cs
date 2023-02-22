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
    public class ShoppingListController : ControllerBase
    {
        ShoppingListService shoppingListService;

        private readonly ILogger<ShoppingListService> logger;

        public ShoppingListController(ILogger<ShoppingListService> logger, ShoppingListService service)
        {
            shoppingListService = service;
            this.logger = logger;
        }

        [HttpPost(Name = "AddShoppingList")]
        public async Task<IResponse<ShoppingListCreateDTO>> AddShoppingList(ShoppingListCreateDTO newShoppingList)
        {
            return await shoppingListService.Create(newShoppingList);
        }
        
        [HttpDelete(Name = "DeleteShoppingList")]
        public async Task<IResponse> DeleteShoppingList(int id)
        {
            return await shoppingListService.Remove(id);
        }

        [HttpPut(Name = "UpdateShoppingList")]
        public async Task<IResponse<ShoppingListDTO>> UpdateShoppingList(ShoppingListDTO newMovie)
        {
            return await shoppingListService.Update(newMovie);
        }

        [HttpGet("GetSingleShoppingList")]
        public async Task<IResponse> GetSingle(int id)
        {
            return await shoppingListService.GetSingle(id);
        }

        [HttpGet("GeAlltShoppingLists")]
        public async Task<IResponse> GetAllShoppingLists( )
        {
            return await shoppingListService.GetAll();
        }
    }
}