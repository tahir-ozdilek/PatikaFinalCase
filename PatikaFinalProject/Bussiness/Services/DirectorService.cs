using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;
using System.Linq;

namespace PatikaFinalProject.Bussiness.Services
{
    public class DirectorService
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<DirectorCreateDTO> _createDtoValidator;
        private readonly IValidator<DirectorDTO> _DTOValidator;
        public DirectorService(MyDbContext dbContext, IMapper mapper, IValidator<DirectorCreateDTO> createDtoValidator, IValidator<DirectorDTO> DTOValidator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _DTOValidator = DTOValidator;
        }

        public async Task<IResponse<DirectorCreateDTO>> Create(DirectorCreateDTO dto)
        {
            ValidationResult validationResult = _createDtoValidator.Validate(dto);

            if (validationResult.IsValid)
            {
                await _dbContext.Set<Director>().AddAsync(_mapper.Map<Director>(dto));
                await _dbContext.SaveChangesAsync();
                return new Response<DirectorCreateDTO>(ResponseType.Success, dto);
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

                return new Response<DirectorCreateDTO>(ResponseType.ValidationError, dto, errors);
            }
        }


        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = _dbContext.Set<Director>().SingleOrDefault(x => x.ID == id);
            if (removedEntity != null)
            {
                _dbContext.Remove(removedEntity);
                await _dbContext.SaveChangesAsync();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");

        }

        public async Task<IResponse<List<DirectorDTO>>> GetAll()
        {
            List<DirectorDTO> data = _mapper.Map<List<DirectorDTO>>(await _dbContext.Set<Director>().ToListAsync());
            return new Response<List<DirectorDTO>>(ResponseType.Success, data);
        }


        public async Task<IResponse<DirectorDTO>> Update(DirectorDTO dto)
        {
            var result = _DTOValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _dbContext.Set<Director>().FindAsync(dto.ID);
                if (updatedEntity != null)
                {
                    _dbContext.Set<Director>().Entry(updatedEntity).CurrentValues.SetValues(_mapper.Map<Director>(dto));
                    await _dbContext.SaveChangesAsync();
                    return new Response<DirectorDTO>(ResponseType.Success, dto);
                }
                return new Response<DirectorDTO>(ResponseType.NotFound, $"{dto.ID} ye ait data bulunamadı");
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

                return new Response<DirectorDTO>(ResponseType.ValidationError, dto, errors);
            }
        }
    }
}
