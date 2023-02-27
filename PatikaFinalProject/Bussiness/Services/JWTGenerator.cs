using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PatikaFinalProject.Common;
using PatikaFinalProject.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatikaFinalProject.Bussiness.Services
{
    public class JWTGenerator
    {
        private readonly MyDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IValidator<LoginRequestModel> loginRequestValidation;
        public JWTGenerator(MyDbContext dbContext, IMapper mapper, IValidator<LoginRequestModel> loginRequestValidation)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.loginRequestValidation = loginRequestValidation;
        }

        public IResponse<LoginResponseModel> ValidateCredentials(LoginRequestModel userModel)
        {
            byte[] hashedPass = System.Security.Cryptography.SHA512.HashData(Encoding.UTF8.GetBytes(userModel.Password + userModel.UserName));

            User? user = dbContext.User.SingleOrDefault(x => x.UserName == userModel.UserName && x.HashedPass == hashedPass);

            if (user == null)
            {
                return new Response<LoginResponseModel>(;
            }
            return true;
        }

        public async Task<RegistrationResponseModel> Register(RegistrationRequestModel userModel)
        {
            RegistrationResponseModel response = new RegistrationResponseModel();
            User? user = dbContext.User.SingleOrDefault(x => x.UserName == userModel.UserName);
            if(user == null)
            {
                byte[] hashedPass = System.Security.Cryptography.SHA512.HashData(Encoding.UTF8.GetBytes(userModel.Password+userModel.UserName));
                
                dbContext.User.Add(new User(userModel.UserName, hashedPass, userModel.UserType));
                response.Message = "Successfully registered.";
            }
            else
            {
                response.Message = "Username already exist.";
            }
            return response;
        }

        public LoginResponseModel GenerateToken(LoginResponseModel userModel)
        { 
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("How much is this static key secure ?"));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Role, userModel.UserType) };
    
            JwtSecurityToken token = new JwtSecurityToken(issuer: "http://localhost", claims: claims, audience: "http://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(100), signingCredentials: credentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new LoginResponseModel(handler.WriteToken(token), userModel);
        }
    }

    public class LoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        public LoginResponseModel(string token, LoginResponseModel userModel)
        {
            Token = token;
            UserName = userModel.UserName;
            UserType = userModel.UserType;
        }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; } // Member, Admin
    }

    public class RegistrationRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; } // Member, Admin
    }

    public class RegistrationResponseModel
    {
        public string Message { get; set; }
    }

}
