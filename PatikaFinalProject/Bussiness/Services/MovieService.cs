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
    public class MovieService
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<MovieCreateDTO> _createDtoValidator;
        private readonly IValidator<MovieDTO> _DTOValidator;
        public MovieService(MyDbContext dbContext, IMapper mapper, IValidator<MovieCreateDTO> createDtoValidator, IValidator<MovieDTO> DTOValidator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _DTOValidator = DTOValidator;
        }

        public async Task<IResponse<MovieCreateDTO>> Create(MovieCreateDTO dto)
        {
            ValidationResult validationResult = _createDtoValidator.Validate(dto);

            if (validationResult.IsValid)
            {
                await _dbContext.Set<Movie>().AddAsync(_mapper.Map<Movie>(dto));
                await _dbContext.SaveChangesAsync();
                return new Response<MovieCreateDTO>(ResponseType.Success, dto);
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
                return new Response<MovieCreateDTO>(ResponseType.ValidationError, dto, errors);
            }
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = _dbContext.Set<Movie>().SingleOrDefault(x => x.ID == id);
            if (removedEntity != null)
            {
                _dbContext.Remove(removedEntity);
                await _dbContext.SaveChangesAsync();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
        }

        public async Task<IResponse<List<MovieDTO>>> GetAll()
        {
            List<MovieDTO> data = _mapper.Map<List<MovieDTO>>(await _dbContext.Set<Movie>().Include(co => co.MovieType).ToListAsync());
            return new Response<List<MovieDTO>>(ResponseType.Success, data);
        }

        public async Task<IResponse<MovieDTO>> Update(MovieDTO dto)
        {
            var result = _DTOValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _dbContext.Set<Movie>().FindAsync(dto.ID);
                if (updatedEntity != null)
                {
                    _dbContext.Set<Movie>().Entry(updatedEntity).CurrentValues.SetValues(_mapper.Map<Movie>(dto));
                    await _dbContext.SaveChangesAsync();
                    return new Response<MovieDTO>(ResponseType.Success, dto);
                }
                return new Response<MovieDTO>(ResponseType.NotFound, $"{dto.ID} ye ait data bulunamadı");
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

                return new Response<MovieDTO>(ResponseType.ValidationError, dto, errors);
            }
        }
    }
}
