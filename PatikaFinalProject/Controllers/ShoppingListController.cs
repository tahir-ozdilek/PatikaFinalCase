using Microsoft.AspNetCore.Authorization;
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

        //liste adı, kategori veya oluşturulma, tamamlanma tarihine göre arama
        [HttpPost("SearchByCategory")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> SearchByCategory()
        {
            return await shoppingListService.SearchByCategory();
        }

        [HttpPost("SearchByListName")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> SearchByListName()
        {
            return await shoppingListService.SearchByListName();
        }

        [HttpPost("SearchByCompletionDate")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> SearchByCreationDate()
        {
            return await shoppingListService.SearchByCreationDate();
        }

        [HttpPost("SearchByCompletionDate")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> SearchByCompletionDate()
        {
            return await shoppingListService.SearchByCompletionDate();
        }
    }
}