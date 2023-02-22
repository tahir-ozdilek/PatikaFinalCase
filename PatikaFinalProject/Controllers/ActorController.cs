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
    public class ActorController : ControllerBase
    {
        ActorService _actorService;

        private readonly ILogger<ActorService> _logger;

        public ActorController(ILogger<ActorService> logger, ActorService service)
        {
            _actorService = service;
            _logger = logger;
        }

        [HttpPost(Name = "AddActor")]
        public async Task<IResponse<ActorCreateDTO>> AddActor(ActorCreateDTO newActor)
        {
            return await _actorService.Create(newActor);
        }
        
        [HttpDelete(Name = "DeleteActor")]
        public async Task<IResponse> DeleteActor(int id)
        {
            return await _actorService.Remove(id);
        }

        [HttpPut(Name = "UpdateActor")]
        public async Task<IResponse<ActorDTO>> UpdateActor(ActorDTO newActor)
        {
            return await _actorService.Update(newActor);
        }

        [HttpGet("GetAllActors")]
        public async Task<IResponse> GetActors( )
        {
            return await _actorService.GetAll();
        }
    }
}