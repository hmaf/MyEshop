using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
