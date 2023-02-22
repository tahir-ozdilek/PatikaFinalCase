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
    public class MovieController : ControllerBase
    {
        MovieService _MovieService;

        private readonly ILogger<MovieService> _logger;

        public MovieController(ILogger<MovieService> logger, MovieService service)
        {
            _MovieService = service;
            _logger = logger;
        }

        [HttpPost(Name = "AddMovie")]
        public async Task<IResponse<MovieCreateDTO>> AddMovie(MovieCreateDTO newMovie)
        {
            return await _MovieService.Create(newMovie);
        }
        
        [HttpDelete(Name = "DeleteMovie")]
        public async Task<IResponse> DeleteMovie(int id)
        {
            return await _MovieService.Remove(id);
        }

        [HttpPut(Name = "UpdateMovie")]
        public async Task<IResponse<MovieDTO>> UpdateMovie(MovieDTO newMovie)
        {
            return await _MovieService.Update(newMovie);
        }

        [HttpGet("GetAllMovies")]
        public async Task<IResponse> GetMovies( )
        {
            return await _MovieService.GetAll();
        }
    }
}