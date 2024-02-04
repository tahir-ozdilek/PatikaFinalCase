using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatikaFinalProject.Bussiness.Services;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;


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
        public async Task<Response<ShoppingListCreateDTO>> AddShoppingList(ShoppingListCreateDTO newShoppingList)
        {
            return await shoppingListService.CreateShoppingList(newShoppingList);
        }

        [HttpPost("CreateCategory")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<Response<CategoryCreateDTO>> CreateCategory(CategoryCreateDTO newCategory)
        {
            return await shoppingListService.CreateCategory(newCategory);
        }

        [HttpDelete("DeleteShoppingList-{id}")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<Response> DeleteShoppingList(int id)
        {
            return await shoppingListService.RemoveShoppingList(id);
        }

        [HttpDelete("DeleteCategory-{id}")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<Response> DeleteCategory(int id)
        {
            return await shoppingListService.RemoveCategory(id);
        }

        [HttpPut("UpdateAllInOne-{id}")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<Response<ShoppingListDTO>> UpdateAllInOne(ShoppingListDTO newMovie)
        {
            return await shoppingListService.UpdateAllInOne(newMovie);
        }

        [HttpPut("UpdateOnlyShoppingList")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<Response<ShoppingListDTO>> UpdateOnlyShoppingList(ShoppingListDTO newMovie)
        {
            return await shoppingListService.UpdateOnlyShoppingList(newMovie);
        }

        [HttpGet("GetSingleShoppingList-{id}")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<Response> GetSingle(int id)
        {
            return await shoppingListService.GetSingle(id);
        }

        
        [HttpGet("GetAllUncompletedShoppingLists")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<Response> GetAllUncompletedShoppingLists( )
        {
            return await shoppingListService.GetUncompletedAll();
        }

        [HttpGet("GetAllCompletedShoppingLists")]
        //[Authorize(Roles = "Admin")]
        public async Task<Response<List<ShoppingListDTO>>> GetAllCompletedShoppingLists()
        {
            return await shoppingListService.GetCompletedAll();
        }

        [HttpGet("GetWithDevExFilters")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<object> GetWithDevExFilters([FromQuery] DataSourceLoadOptionsBase options)
        {
            return await shoppingListService.GetWithDevExFilters(options);
        }
    }
}