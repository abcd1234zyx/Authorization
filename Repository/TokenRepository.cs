using Authorization.Models;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Authorization.Mappers;

namespace Authorization.Repository
{
    public class TokenRepository : ITokenRepository
    {

        /// <summary>
        /// This method will generate token
        /// </summary>
        /// <param name="_cofig,memberDetail"></param>
        /// <returns>token</returns>
        public string GenerateJSONWebToken(IConfiguration _config,LoginModel loginModel)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                List<Claim> claims = new List<Claim>() {                    
                    new Claim(JwtRegisteredClaimNames.Sub, loginModel.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, loginModel.Userid.ToString())
                };
                var token = new JwtSecurityToken(
                  _config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(15),
                  signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// method will fetch credential from member Data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Member</returns>
        public LoginModel CheckCredential(LoginModel model)
        {
            try
            {                
                LoginModel memberDetail;
                {
                    var login = GetUserDataFromCsv();
                    memberDetail = login.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                }
                return memberDetail;
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public List<LoginModel> GetUserDataFromCsv()
        {
            string path = @"./UserData.csv";
            try
            {
                using (var reader = new StreamReader(path, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<UserDataMap>();
                    var records = csv.GetRecords<LoginModel>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
    }

    
