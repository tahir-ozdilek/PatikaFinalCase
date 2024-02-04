using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatikaFinalProject.Bussiness.Services
{
    public class RegisterLoginService
    {
        private readonly MyDbContext dbContext;
        private readonly IValidator<LoginRequestModel> loginRequestValidator;
        private readonly IValidator<RegistrationRequestModel> registrationRequestValidator;
        
        public RegisterLoginService(MyDbContext dbContext, IMapper mapper, IValidator<LoginRequestModel> loginRequestValidator, IValidator<RegistrationRequestModel> registrationRequestValidator)
        {
            this.dbContext = dbContext;
            this.loginRequestValidator = loginRequestValidator;
            this.registrationRequestValidator = registrationRequestValidator;
        }

        public Response<RegistrationRequestModel> ValidateCredentials(LoginRequestModel userModel)
        {
            ValidationResult validationResult = loginRequestValidator.Validate(userModel);

            if (!validationResult.IsValid)
            {
                return new Response<RegistrationRequestModel>(ResponseType.ValidationError, new RegistrationRequestModel(userModel.UserName, userModel.Password, ""), createValidationResult(validationResult));
            }

            byte[] hashedPass = System.Security.Cryptography.SHA512.HashData(Encoding.UTF8.GetBytes(userModel.Password + userModel.UserName));

            User? user = dbContext.Set<User>().SingleOrDefault(x => x.UserName == userModel.UserName && x.HashedPass == hashedPass);

            if (user == null)
            {
                return new Response<RegistrationRequestModel>(ResponseType.NotFound, "Not Found");
            }
            return new Response<RegistrationRequestModel>(ResponseType.Success, new RegistrationRequestModel(user.UserName, userModel.Password, user.UserType));
        }

        public LoginResponseModel GenerateToken(RegistrationRequestModel userModel)
        {
            if(userModel.UserName != null && userModel.UserType != null)
            { 
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("How much is this static key secure ?"));

                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Role, userModel.UserType) };

                JwtSecurityToken token = new JwtSecurityToken(issuer: "http://localhost", claims: claims, audience: "http://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(1000), signingCredentials: credentials);
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                return new LoginResponseModel(handler.WriteToken(token), userModel.UserName);
            }
            return new LoginResponseModel("", "");
        }

        public async Task<Response> Register(RegistrationRequestModel userModel)
        {
            ValidationResult validationResult = registrationRequestValidator.Validate(userModel);

            Response response;
            User? user = dbContext.Set<User>().SingleOrDefault(x => x.UserName == userModel.UserName);
            if(user == null && validationResult.Errors.Count == 0 && userModel.UserName != null && userModel.UserType != null)
            {
                byte[] hashedPass = System.Security.Cryptography.SHA512.HashData(Encoding.UTF8.GetBytes(userModel.Password+userModel.UserName));

                await dbContext.Set<User>().AddAsync(new User(userModel.UserName, hashedPass, userModel.UserType));
                await dbContext.SaveChangesAsync();

                response = new Response(ResponseType.Success, "Successfully registered.");
            }
            else if(user != null)
            {
                response = new Response(ResponseType.ValidationError, "Username already exist.");
            }
            else
            {
                response = new Response<RegistrationRequestModel>(ResponseType.ValidationError, userModel, createValidationResult(validationResult));
            }
            return response;
        }

        private List<CustomValidationError> createValidationResult(ValidationResult result)
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

            return errors;
        }
    }

    public class LoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginRequestModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }

    public class LoginResponseModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }

        public LoginResponseModel(string token, string userName)
        {
            Token = token;
            UserName = userName;
        }
    }

    public class RegistrationRequestModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? UserType { get; set; } // Member, Admin

        public RegistrationRequestModel() { }
        public RegistrationRequestModel(string userName, string userPassword, string userType)
        {
            UserName = userName;
            UserType = userType;
            Password = userPassword;
        }
    }
}
