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

        [HttpPost("AddShoppingList")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse<ShoppingListCreateDTO>> AddShoppingList(ShoppingListCreateDTO newShoppingList)
        {
            return await shoppingListService.CreateShoppingList(newShoppingList);
        }

        [HttpPost("CreateCategory")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse<CategoryCreateDTO>> CreateCategory(CategoryCreateDTO newCategory)
        {
            return await shoppingListService.CreateCategory(newCategory);
        }

        [HttpDelete(Name = "DeleteShoppingList-{id}")]
        //[Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> DeleteShoppingList(int id)
        {
            return await shoppingListService.Remove(id);
        }

        [HttpPut("UpdateAllInOne-{id}")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse<ShoppingListDTO>> UpdateAllInOne(ShoppingListDTO newMovie)
        {
            return await shoppingListService.UpdateAllInOne(newMovie);
        }

        [HttpPut("UpdateOnlyShoppingList")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse<ShoppingListDTO>> UpdateOnlyShoppingList(ShoppingListDTO newMovie)
        {
            return await shoppingListService.UpdateOnlyShoppingList(newMovie);
        }

        [HttpGet("GetSingleShoppingList-{id}")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> GetSingle(int id)
        {
            return await shoppingListService.GetSingle(id);
        }

        [HttpGet("GetAllUncompletedShoppingLists")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> GetAllUncompletedShoppingLists( )
        {
            return await shoppingListService.GetUncompletedAll();
        }

        [HttpGet("GetAllCompletedShoppingLists")]
        [Authorize(Roles = "Admin")]
        public async Task<IResponse> GetAllCompletedShoppingLists()
        {
            return await shoppingListService.GetCompletedAll();
        }
    }
}