using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Data.SqlClient;
using PatikaFinalProject.Bussiness.Services;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PatikaFinalProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingListController : ControllerBase
    {
        ShoppingListService shoppingListService;

        public ShoppingListController(ShoppingListService service)
        {
            shoppingListService = service;
        }

        [HttpPost(Name = "AddShoppingList")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse<ShoppingListCreateDTO>> AddShoppingList(ShoppingListCreateDTO newShoppingList)
        {
            return await shoppingListService.Create(newShoppingList);
        }
        
        [HttpDelete(Name = "DeleteShoppingList")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> DeleteShoppingList(int id)
        {
            return await shoppingListService.Remove(id);
        }

        [HttpPut(Name = "UpdateShoppingList")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse<ShoppingListDTO>> UpdateShoppingList(ShoppingListDTO newMovie)
        {
            return await shoppingListService.Update(newMovie);
        }

        [HttpGet("GetSingleShoppingList")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> GetSingle(int id)
        {
            return await shoppingListService.GetSingle(id);
        }

        [HttpGet("GetAllShoppingLists")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> GetAllShoppingLists( )
        {
            return await shoppingListService.GetAll();
        }
    }
}