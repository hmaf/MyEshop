using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;

namespace MyEshop.Models
{
    public class RegisterViewModel
        {
            
            [MaxLength(300)]
            [EmailAddress]
            [Display(Name = "ایمیل")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Remote("VerifyEmail", "Account")]
            public string Email { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [MaxLength(50)]
            [Display(Name = "کلمه عبور")]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
            public string Password { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [MaxLength(50)]
            [Display(Name = "کلمه عبور")]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string RePassword { get; set; }
        }


    public class LoginViewModel
    {
        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپارید")]
        public bool ReMemberMe { get; set; }
    }
}
