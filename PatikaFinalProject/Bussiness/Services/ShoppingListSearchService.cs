using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;

namespace PatikaFinalCase.Bussiness.Services
{
    public class ShoppingListSearchService
    {
        private readonly MyDbContext dbContext;
        private readonly IMapper mapper;

        public ShoppingListSearchService(MyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IResponse<List<ShoppingListDTO>>> SearchByCategory(string categoryName)
        {

            List<ShoppingList> shoppingList = await dbContext.Set<ShoppingList>().Include(y => y.Category).Where(y => y.Category !=null && y.Category.Name == categoryName).Include(x => x.ProductList).ToListAsync();

            List<ShoppingListDTO> data = mapper.Map<List<ShoppingListDTO>>(shoppingList);
            return new Response<List<ShoppingListDTO>>(ResponseType.Success, data);
        }

        public async Task<IResponse<List<ShoppingListDTO>>> SearchByListName(string categoryName)
        {
            List<ShoppingList> shoppingList = await dbContext.Set<ShoppingList>().Where(t => t.Name == categoryName).Include(y => y.Category).Include(x => x.ProductList).ToListAsync();

            List<ShoppingListDTO> data = mapper.Map<List<ShoppingListDTO>>(shoppingList);
            return new Response<List<ShoppingListDTO>>(ResponseType.Success, data);
        }

        public async Task<IResponse<List<ShoppingListDTO>>> FindShoppingListsWithCreationDateAfterThan(DateTime date)
        {
            List<ShoppingList> shoppingList = await dbContext.Set<ShoppingList>().Where(t => t.CreationDate > date).Include(y => y.Category).Include(x => x.ProductList).ToListAsync();

            List<ShoppingListDTO> data = mapper.Map<List<ShoppingListDTO>>(shoppingList);
            return new Response<List<ShoppingListDTO>>(ResponseType.Success, data);
        }

        public async Task<IResponse<List<ShoppingListDTO>>> FindShoppingListsWithCreationDateBeforeThan(DateTime date)
        {
            List<ShoppingList> shoppingList = await dbContext.Set<ShoppingList>().Where(t => t.CreationDate < date).Include(y => y.Category).Include(x => x.ProductList).ToListAsync();

            List<ShoppingListDTO> data = mapper.Map<List<ShoppingListDTO>>(shoppingList);
            return new Response<List<ShoppingListDTO>>(ResponseType.Success, data);
        }

        public async Task<IResponse<List<ShoppingListDTO>>> FindShoppingListsWithCompletionDateAfterThan(DateTime date)
        {
            List<ShoppingList> shoppingList = await dbContext.Set<ShoppingList>().Where(t => t.CompletedDate > date).Include(y => y.Category).Include(x => x.ProductList).ToListAsync();

            List<ShoppingListDTO> data = mapper.Map<List<ShoppingListDTO>>(shoppingList);
            return new Response<List<ShoppingListDTO>>(ResponseType.Success, data);
        }

        public async Task<IResponse<List<ShoppingListDTO>>> FindShoppingListsWithCompletionDateBeforeThan(DateTime date)
        {
            List<ShoppingList> shoppingList = await dbContext.Set<ShoppingList>().Where(t => t.CompletedDate < date).Include(y => y.Category).Include(x => x.ProductList).ToListAsync();

            List<ShoppingListDTO> data = mapper.Map<List<ShoppingListDTO>>(shoppingList);
            return new Response<List<ShoppingListDTO>>(ResponseType.Success, data);
        }
    }
}
