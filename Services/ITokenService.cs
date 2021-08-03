using Authorization.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public interface ITokenService
    {
        public string GenerateJSONWebToken(IConfiguration _config, LoginModel loginModel);
        public LoginModel CheckCredential(LoginModel model);
    }
}
