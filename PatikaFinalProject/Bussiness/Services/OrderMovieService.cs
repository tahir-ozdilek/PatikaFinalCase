using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;
using System.Linq;
using System.Xml;

namespace PatikaFinalProject.Bussiness.Services
{
    public class OrderMovieService
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderMovieService(MyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IResponse<OrderCreateDTO>> Create(int MovieID, int CustomerID)
        {
            OrderCreateDTO dto;
            Movie? retrievedMovie = _dbContext.Set<Movie>().SingleOrDefault(x => x.ID == MovieID);
            if (retrievedMovie != null && retrievedMovie.IsSold == false)
            {
                dto = new OrderCreateDTO();
                dto.MovieID = MovieID;
                dto.CustomerID = CustomerID;
                dto.OrderDate = DateTime.Now;
                dto.Price = retrievedMovie.Price;

                Order or = _mapper.Map<Order>(dto);
                await _dbContext.Set<Order>().AddAsync(or);
                retrievedMovie.IsSold= true;
                _dbContext.Set<Movie>().Entry(retrievedMovie).CurrentValues.SetValues(retrievedMovie);

                await _dbContext.SaveChangesAsync();
                return new Response<OrderCreateDTO>(ResponseType.Success, dto);
            }
            else
            {
                List<CustomValidationError> errors = new();
                errors.Add(new()
                {
                    ErrorMessage = "Movie not found"
                }); ;

                return new Response<OrderCreateDTO>(ResponseType.ValidationError, null, errors);
            }
        }
    }
}
