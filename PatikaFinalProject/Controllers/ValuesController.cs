using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PatikaFinalCase.Bussiness.Services;
using PatikaFinalProject.Bussiness.Services;
using PatikaFinalProject.Common;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatikaFinalCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListSearchController : ControllerBase
    {
        ShoppingListSearchService searchService;

        public ShoppingListSearchController(ShoppingListSearchService service)
        {
            searchService = service;
        }


        [HttpPost("SearchByCategory")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> SearchByCategory(string categoryName)
        {
            return await searchService.SearchByCategory(categoryName);
        }

        [HttpPost("SearchByListName")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> SearchByListName(string listName)
        {
            return await searchService.SearchByListName(listName);
        }

        [OutputCache(Duration = 100)]
        [HttpPost("FindShoppingListsWithCreationDateAfterThan")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> FindShoppingListsWithCreationDateAfterThan(DateTime date)
        {
            return await searchService.FindShoppingListsWithCreationDateAfterThan(date);
        }

        [OutputCache(Duration = 100)]
        [HttpPost("FindShoppingListsWithCreationDateBeforeThan")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> FindShoppingListsWithCreationDateBeforeThan(DateTime date)
        {
            return await searchService.FindShoppingListsWithCreationDateBeforeThan(date);
        }

        [OutputCache(Duration = 100)]
        [HttpPost("FindShoppingListsWithCompletionDateAfterThan")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> FindShoppingListsWithCompletionDateAfterThan(DateTime date)
        {
            return await searchService.FindShoppingListsWithCompletionDateAfterThan(date);
        }

        [OutputCache(Duration = 100)]
        [HttpPost("FindShoppingListsWithCompletionDateBeforeThan")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IResponse> FindShoppingListsWithCompletionDateBeforeThan(DateTime date)
        {
            return await searchService.FindShoppingListsWithCompletionDateBeforeThan(date);
        }
    }
}
