using Authorization.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Authorization.Repository;

namespace Authorization.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _repository;
        public TokenService(ITokenRepository repository)
        {
            _repository = repository;
        }
        public LoginModel CheckCredential(LoginModel model)
        {
            LoginModel userDetails = _repository.CheckCredential(model);
            return userDetails;
        }

        public string GenerateJSONWebToken(IConfiguration _config, LoginModel loginModel)
        {
            var tokenString =_repository.GenerateJSONWebToken(_config, loginModel);
            return tokenString;
        }
    }
}
