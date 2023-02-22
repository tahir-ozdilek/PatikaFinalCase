using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;
using System.ComponentModel.Design;
using System.Linq;

namespace PatikaFinalProject.Bussiness.Services
{
    public class ShoppingListService
    {
        private readonly MyDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IValidator<ShoppingListDTO> DTOValidator;
        private readonly IValidator<ShoppingListCreateDTO> createDTOValidator;
        public ShoppingListService(MyDbContext dbContext, IMapper mapper, IValidator<ShoppingListCreateDTO> createDTOValidator, IValidator<ShoppingListDTO> DTOValidator)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.createDTOValidator = createDTOValidator;
            this.DTOValidator = DTOValidator;
        }

        public async Task<IResponse<ShoppingListCreateDTO>> Create(ShoppingListCreateDTO dto)
        {
            ValidationResult validationResult = createDTOValidator.Validate(dto);

            if (validationResult.IsValid)
            {
                await dbContext.Set<ShoppingList>().AddAsync(mapper.Map<ShoppingList>(dto));
                await dbContext.SaveChangesAsync();
                return new Response<ShoppingListCreateDTO>(ResponseType.Success, dto);
            }
            else
            {
                List<CustomValidationError> errors = new();
                foreach (ValidationFailure error in validationResult.Errors)
                {
                    errors.Add(new()
                    {
                        ErrorMessage = error.ErrorMessage,
                        PropertyName = error.PropertyName
                    });
                }
                return new Response<ShoppingListCreateDTO>(ResponseType.ValidationError, dto, errors);
            }
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = dbContext.ShoppingList.SingleOrDefault(x => x.ID == id);
            if (removedEntity != null)
            {
                dbContext.Remove(removedEntity);
                await dbContext.SaveChangesAsync();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
        }

        public async Task<IResponse<List<ShoppingListDTO>>> GetAll()
        {
            List<ShoppingList> shoppingList = await dbContext.Set<ShoppingList>().Include(y => y.Category).Include(x => x.ProductList).ToListAsync();

            List<ShoppingListDTO> data = mapper.Map<List<ShoppingListDTO>>(shoppingList);
            return new Response<List<ShoppingListDTO>>(ResponseType.Success, data);
        }

        public async Task<IResponse<ShoppingListDTO>> Update(ShoppingListDTO dto)
        {
            var result = DTOValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await dbContext.Set<ShoppingList>().FindAsync(dto.ID);
                if (updatedEntity != null)
                {
                    dbContext.Set<ShoppingList>().Entry(updatedEntity).CurrentValues.SetValues(mapper.Map<ShoppingList>(dto));
                    await dbContext.SaveChangesAsync();
                    return new Response<ShoppingListDTO>(ResponseType.Success, dto);
                }
                return new Response<ShoppingListDTO>(ResponseType.NotFound, $"{dto.ID} ye ait data bulunamadı");
            }
            else
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

                return new Response<ShoppingListDTO>(ResponseType.ValidationError, dto, errors);
            }
        }
    }
}
