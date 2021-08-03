using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Models
{
    public class LoginModel
    {
        [Key]
        public int Userid { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
