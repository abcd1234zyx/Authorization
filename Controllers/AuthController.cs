using System;
using Authorization.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Authorization.Services;
using Microsoft.AspNetCore.Authorization;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;       
        private readonly ITokenService _tokenService;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));

        public AuthController(IConfiguration config, ITokenService tokenService)
        {
            _config = config;            
            _tokenService = tokenService;
        }

        /// <summary>
        /// 1.Checking if username and password is valid
        /// 2.then generate jwt token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //https://localhost:44392/api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]LoginModel userModel)
        {         
            try
            {
                _log4net.Info(nameof(Login) + " method invoked, Username : "+userModel.Username);
                LoginModel memberDetail=_tokenService.CheckCredential(userModel);
                if (memberDetail != null)
                {
                    var tokenString = _tokenService.GenerateJSONWebToken(_config, memberDetail);
                    return Ok(tokenString);
                }

                return Unauthorized("Invalid User Credentials");
            }
            catch(Exception e)
            {
                _log4net.Error("Error Occured from " + nameof(Login) + "Error Message : " + e.Message);
                return BadRequest(e.Message);
            }
        }
    }

}

