using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatikaFinalProject.Bussiness.Services;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;

namespace PatikaFinalCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        JWTGenerator loginService;

        private readonly ILogger<JWTGenerator> logger;

        public LogInController(ILogger<JWTGenerator> logger, JWTGenerator service)
        {
            loginService = service;
            this.logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IResponse<LoginResponseModel>> Register(LoginRequestModel loginModel)
        {
            return await loginService.Register(loginModel);
        }

        [HttpPost("LogIn")]
        public IActionResult<LoginResponseModel> LogIn(LoginRequestModel loginModel)
        {
            bool isUser = loginService.ValidateCredentials(loginModel);
            if(!isUser) 
            { 
                return Unauthorized();
            }
             return Ok(loginService.GenerateToken(loginModel));
        }
    }
}

