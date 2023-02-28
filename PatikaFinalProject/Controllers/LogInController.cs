using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IResponse> Register(RegistrationRequestModel loginModel)
        {
            return await loginService.Register(loginModel);
        }

        [AllowAnonymous]
        [HttpPost("LogIn")]
        public ActionResult<LoginResponseModel> LogIn(LoginRequestModel loginModel)
        {
            IResponse<RegistrationRequestModel> userModel = loginService.ValidateCredentials(loginModel);
            if(userModel.ResponseType == ResponseType.NotFound) 
            { 
                return Unauthorized();
            }
            return Ok(loginService.GenerateToken(userModel.Data));
        }
    }
}

