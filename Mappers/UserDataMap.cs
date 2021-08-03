using Authorization.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Mappers
{    
    [ExcludeFromCodeCoverage]
    public sealed class UserDataMap : ClassMap<LoginModel>
    {
        public UserDataMap()
        {
            Map(x => x.Userid).Name("Userid");
            Map(x => x.Username).Name("Username");
            Map(x => x.Password).Name("Password");          
        }
    }
}
