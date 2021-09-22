using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyEshop.Data;
using MyEshop.Data.Repository;
using MyEshop.Models;

namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Register

        

        
        [HttpGet]
        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            User user=new User()
            {
                Email = register.Email.ToLower(),
                Password = register.Password,
                RegisterDate = DateTime.Now,
                IsAdmin = false
            };
            _userRepository.addUser(user);

            return View("SuccessRegister",register);
        }
        public IActionResult VerifyEmail(string email)
        {
            if (_userRepository.IExistUserByEmail(email.ToLower()))
            {
                return Json($"ایمیل {email} تکراری است");
            }

            return Json(true);
        }
        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userRepository.GetUserForLogin(login.Email.ToLower(), login.Password);
            if (user==null)
            {
                ModelState.AddModelError("Email","اطلاعات وارد شده صحیح نیست");
                return View(login);
            }
            var claim=new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Email),
                new Claim("IsAdmin",user.IsAdmin.ToString())
            };
            var identity=new ClaimsIdentity(claim,CookieAuthenticationDefaults.AuthenticationScheme);
            var principal=new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = login.ReMemberMe
            };
            HttpContext.SignInAsync(principal, properties);
            return Redirect("/");
        }
        #endregion

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
    }
}