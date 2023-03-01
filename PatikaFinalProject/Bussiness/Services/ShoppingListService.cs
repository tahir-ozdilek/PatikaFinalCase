using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;
using PatikaFinalProject.Services.Validators;
using System.ComponentModel.Design;
using System.Linq;

namespace PatikaFinalProject.Bussiness.Services
{
    public class ShoppingListService
    {
        private readonly MyDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IValidator<ShoppingListDTO> shopppingListDTOValidator;
        private readonly IValidator<ShoppingListCreateDTO> shoppingListCreateDTOValidator;
        public ShoppingListService(MyDbContext dbContext, IMapper mapper, IValidator<ShoppingListCreateDTO> createDTOValidator, IValidator<ShoppingListDTO> DTOValidator)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.shoppingListCreateDTOValidator = createDTOValidator;
            this.shopppingListDTOValidator = DTOValidator;
        }

        public async Task<IResponse<ShoppingListCreateDTO>> CreateShoppingList(ShoppingListCreateDTO dto)
        {
            ValidationResult validationResult = shoppingListCreateDTOValidator.Validate(dto);

            if (validationResult.IsValid)
            {
                await dbContext.Set<ShoppingList>().AddAsync(mapper.Map<ShoppingList>(dto));
                await dbContext.SaveChangesAsync();
                return new Response<ShoppingListCreateDTO>(ResponseType.Success, dto);
            }
            else
            {
                return new Response<ShoppingListCreateDTO>(ResponseType.ValidationError, dto, createValidationResult(validationResult));
            }
        }

        public async Task<IResponse<CategoryCreateDTO>> CreateCategory(CategoryCreateDTO dto)
        {
            CategoryCreateDTOValidator validator = new CategoryCreateDTOValidator();
            ValidationResult validationResult = validator.Validate(dto);

            if (validationResult.IsValid)
            {
                await dbContext.Set<Category>().AddAsync(mapper.Map<Category>(dto));
                await dbContext.SaveChangesAsync();
                return new Response<CategoryCreateDTO>(ResponseType.Success, dto);
            }
            else
            {
                return new Response<CategoryCreateDTO>(ResponseType.ValidationError, dto, createValidationResult(validationResult));
            }
        }

        public async Task<IResponse> RemoveShoppingList(int id)
        {
            ShoppingList? entityToRemove = dbContext.ShoppingList.Include(x=> x.ProductList).SingleOrDefault(x => x.ID == id);
            if (entityToRemove != null)
            {
                dbContext.RemoveRange(entityToRemove.ProductList);
                dbContext.Remove(entityToRemove);
                await dbContext.SaveChangesAsync();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, "Not Found");
        }

        public async Task<IResponse<List<ShoppingList>>> RemoveCategory(int id)
        {
            List<ShoppingList>? listsContainsReletedCategory = await dbContext.ShoppingList.Include(x=> x.Category).Include(x=>x.ProductList).Where(x => x.CategoryID == id).ToListAsync();
            if(listsContainsReletedCategory.Count == 0) 
            { 
                Category? entityToRemove = dbContext.Category.SingleOrDefault(x => x.ID == id);
                if (entityToRemove != null)
                {
                    EntityEntry<Category> a = dbContext.Remove(entityToRemove);
                
                    await dbContext.SaveChangesAsync();
                    return new Response<List<ShoppingList>>(ResponseType.Success, "Successfully removed.");
                }
                return new Response<List<ShoppingList>>(ResponseType.NotFound, "Not Found");
            }
            else
            {
                string Message = "First, you have to delete these related shopping lists to be able to remove this category";
                return new Response<List<ShoppingList>>(ResponseType.ValidationError, listsContainsReletedCategory, Message);
            }
        }

        public async Task<IResponse<ShoppingListDTO>> GetSingle(int id)
        {
            ShoppingList? shoppingList = await dbContext.ShoppingList.Include(x => x.Category).Include(y => y.ProductList).SingleOrDefaultAsync(z => z.ID == id && z.isBought == false);

            if(shoppingList == null)
            {
                new Response<ShoppingListDTO>(ResponseType.NotFound, "Not Found");
            }

            ShoppingListDTO data = mapper.Map<ShoppingListDTO>(shoppingList);
            return new Response<ShoppingListDTO>(ResponseType.Success, data);
        }

        public async Task<IResponse<List<ShoppingListDTO>>> GetUncompletedAll()
        {
            List<ShoppingList> shoppingList = await dbContext.Set<ShoppingList>().Where(z => z.isBought == false).Include(y => y.Category).Include(x => x.ProductList).ToListAsync();

            List<ShoppingListDTO> data = mapper.Map<List<ShoppingListDTO>>(shoppingList);
            return new Response<List<ShoppingListDTO>>(ResponseType.Success, data);
        }

        public async Task<IResponse<List<ShoppingListDTO>>> GetCompletedAll()
        {
            List<ShoppingList> shoppingList = await dbContext.Set<ShoppingList>().Where(z => z.isBought == true).Include(y => y.Category).Include(x => x.ProductList).ToListAsync();

            List<ShoppingListDTO> data = mapper.Map<List<ShoppingListDTO>>(shoppingList);
            return new Response<List<ShoppingListDTO>>(ResponseType.Success, data);
        }

        public async Task<IResponse<ShoppingListDTO>> UpdateAllInOne(ShoppingListDTO dto)
        {
            var result = shopppingListDTOValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await dbContext.Set<ShoppingList>().Include(x => x.Category).Include(x => x.ProductList).SingleOrDefaultAsync(c=> c.ID ==dto.ID);
                if (updatedEntity != null)
                {
                    dbContext.Set<ShoppingList>().Entry(updatedEntity).CurrentValues.SetValues(mapper.Map<ShoppingList>(dto));
                    dbContext.Set<Category>().Entry(updatedEntity.Category).CurrentValues.SetValues(mapper.Map<Category>(dto.Category));
                    for(int i = 0; i< dto.ProductList.Count; i++)
                    {
                        dbContext.Set<Product>().Entry(dbContext.Set<Product>().SingleOrDefault(x=> x.ID == dto.ProductList[i].ID)).CurrentValues.SetValues(mapper.Map<Product>(dto.ProductList[i]));
                    }
                    
                    await dbContext.SaveChangesAsync();
                    return new Response<ShoppingListDTO>(ResponseType.Success, dto);
                }
                return new Response<ShoppingListDTO>(ResponseType.NotFound, "Not Found");
            }
            else
            {
                return new Response<ShoppingListDTO>(ResponseType.ValidationError, dto, createValidationResult(result));
            }
        }

        public async Task<IResponse<ShoppingListDTO>> UpdateOnlyShoppingList(ShoppingListDTO dto)
        {
            ValidationResult result = shopppingListDTOValidator.Validate(dto);
            
            if (result.IsValid)
            {
                var updatedEntity = await dbContext.Set<ShoppingList>().Include(x => x.Category).Include(x => x.ProductList).SingleOrDefaultAsync(c => c.ID == dto.ID);
                if (updatedEntity != null)
                {
                    dbContext.Set<ShoppingList>().Entry(updatedEntity).CurrentValues.SetValues(mapper.Map<ShoppingList>(dto));
                    await dbContext.SaveChangesAsync();
                    return new Response<ShoppingListDTO>(ResponseType.Success, dto);
                }
                return new Response<ShoppingListDTO>(ResponseType.NotFound, "Not Found");
            }
            else
            {
                return new Response<ShoppingListDTO>(ResponseType.ValidationError, dto, createValidationResult(result));
            }
        }

        private List<CustomValidationError> createValidationResult(ValidationResult result)
        {
            List<CustomValidationError> errors = new();
            foreach (var error in result.Errors)
            {
                errors.Add(new()
                {
                    ErrorMessage = error.ErrorMessage,
                    PropertyName = error.PropertyName
                });
            }

            return errors;
        }
    }
}
