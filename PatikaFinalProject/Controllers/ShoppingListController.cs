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

        private readonly ILogger<ShoppingListService> logger;

        public ShoppingListController(ILogger<ShoppingListService> logger, ShoppingListService service)
        {
            shoppingListService = service;
            this.logger = logger;
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

        
        [HttpPost("SearchByCategory")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> SearchByCategory(string categoryName)
        {
            return await shoppingListService.SearchByCategory(categoryName);
        }

        [HttpPost("SearchByListName")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> SearchByListName(string listName)
        {
            return await shoppingListService.SearchByListName(listName);
        }

        [OutputCache(Duration = 100)]
        [HttpPost("FindShoppingListsWithCreationDateAfterThan")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> FindShoppingListsWithCreationDateAfterThan(DateTime date)
        {
            return await shoppingListService.FindShoppingListsWithCreationDateAfterThan(date);
        }

        [OutputCache(Duration = 100)]
        [HttpPost("FindShoppingListsWithCreationDateBeforeThan")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> FindShoppingListsWithCreationDateBeforeThan(DateTime date)
        {
            return await shoppingListService.FindShoppingListsWithCreationDateBeforeThan(date);
        }

        [OutputCache(Duration = 100)]
        [HttpPost("FindShoppingListsWithCompletionDateAfterThan")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> FindShoppingListsWithCompletionDateAfterThan(DateTime date)
        {
            return await shoppingListService.FindShoppingListsWithCompletionDateAfterThan(date);
        }

        [OutputCache(Duration = 100)]
        [HttpPost("FindShoppingListsWithCompletionDateBeforeThan")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> FindShoppingListsWithCompletionDateBeforeThan(DateTime date)
        {
            return await shoppingListService.FindShoppingListsWithCompletionDateBeforeThan(date);
        }
    }
}