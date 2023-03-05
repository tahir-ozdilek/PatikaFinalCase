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
        RegisterLoginService loginService;

        public LogInController(RegisterLoginService service)
        {
            loginService = service;
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
            if(userModel.ResponseType != ResponseType.Success) 
            { 
                return Unauthorized(userModel);
            }
            if(userModel.Data != null)
            {
                return Ok(loginService.GenerateToken(userModel.Data));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

