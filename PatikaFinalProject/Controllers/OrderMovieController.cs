using Microsoft.AspNetCore.Authorization;
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
    public class OrderMovieController : ControllerBase
    {
        OrderMovieService _OrderMovieService;

        private readonly ILogger<OrderMovieService> _logger;

        public OrderMovieController(ILogger<OrderMovieService> logger, OrderMovieService service)
        {
            _OrderMovieService = service;
            _logger = logger;
        }
        [AllowAnonymous]
        //[Authorize(Roles = "Member")]
        [HttpPost("OrderMovie")]
        public async Task<IResponse<OrderCreateDTO>> OrderMovie(int movieID, int customerID)
        {
            return await _OrderMovieService.Create(movieID, customerID);
        }

        [AllowAnonymous]
        [HttpPost("LogIn")]
        public IActionResult Login(LoginRequestModel userModel)
        {
            JWTGenerator authenticator = new JWTGenerator();

            if (authenticator.ValidateCredentials(userModel))
            {
                return Created("", authenticator.GenerateToken(userModel));
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}