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
    public class DirectorController : ControllerBase
    {
        DirectorService _DirectorService;

        private readonly ILogger<DirectorService> _logger;

        public DirectorController(ILogger<DirectorService> logger, DirectorService service)
        {
            _DirectorService = service;
            _logger = logger;
        }

        [HttpPost(Name = "AddDirector")]
        public async Task<IResponse<DirectorCreateDTO>> AddDirector(DirectorCreateDTO newDirector)
        {
            return await _DirectorService.Create(newDirector);
        }
        
        [HttpDelete(Name = "DeleteDirector")]
        public async Task<IResponse> DeleteDirector(int id)
        {
            return await _DirectorService.Remove(id);
        }

        [HttpPut(Name = "UpdateDirector")]
        public async Task<IResponse<DirectorDTO>> UpdateDirector(DirectorDTO newDirector)
        {
            return await _DirectorService.Update(newDirector);
        }

        [HttpGet("GetAllDirectors")]
        public async Task<IResponse> GetDirectors( )
        {
            return await _DirectorService.GetAll();
        }
    }
}