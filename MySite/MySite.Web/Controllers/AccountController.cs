using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySite.Web.Models;
using MySite.Model;
using Microsoft.EntityFrameworkCore;
using MySite.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace MySite.Web.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly DataContext _context;
        
        public AccountController(DataContext context)
        {
            _context = context;
        }    

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexCookie(string ReturnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //检查用户信息
                var user = CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    //记录Session
                    //HttpContext.Session.Set("CurrentUser", ByteConvertHelper.Object2Bytes(user));
                    HttpContext.Session.SetString("CurrentUser", user.UserName);
                    //跳转到系统首页
                    return RedirectToAction("Index", "Doctor");
                }
                //ModelState.AddModelError("", "Username or password is wrong.");
                
                //return RedirectToAction("Index", "Account");
            }
            PreAction();
            //return View(model);
            return View("Index", model);
        }

        private Login CheckUser(string userName, string password)
        {
            string MD5String = MD5Comm.Get32MD5One(password).ToLower();
            var SelectedUser = from s in _context.Users
                                where s.UserName == userName && s.PasswordMD532 == MD5String
                                select s;
            if(SelectedUser.Count() > 0)
            {
                return SelectedUser.First();
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCookie(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //检查用户信息
                var user = CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>{
                        new Claim(ClaimTypes.Name, model.UserName)
                    };
                    
                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal  principal = new ClaimsPrincipal (userIdentity);
                    AuthenticationProperties authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(2)
                    };
                    if(model.RememberMe)
                    {
                        authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10);
                    }
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    
                    return RedirectToAction("Index", "Doctor");
                }
            }
            return View("Index", model);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> LogoutCookie()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            LogoutModel LogoutMes = new LogoutModel(){ Status = true, Error = "", Message = "Logout successfully." };
            //return RedirectToAction("Index", "Home");
            return Json(LogoutMes);
        }

        [AllowAnonymous]
        public IActionResult Register(RegistionModel model)
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> RegisterSave(RegistionModel model)
        {
            if (ModelState.IsValid)
            {
                var UserCount = await _context.Registions.CountAsync(s => s.UserName.Equals(model.UserName));

                if(UserCount == 0)
                {
                    Login _newLogin = new Login();
                    var Registion = CheckConvertRegistion(model, _newLogin);
                    _context.Registions.Add(Registion);
                    _context.Users.Add(_newLogin);
                    await _context.SaveChangesAsync();
                }
            }
            return View("IndexCookie");
        }

        private Registion CheckConvertRegistion(RegistionModel model, Login user)
        {
            Registion _registion = new Registion();
            _registion.UserName = model.UserName;
            _registion.Password = model.Password;
            _registion.PasswordMD532 = MD5Comm.Get32MD5One(model.Password).ToLower();
            _registion.Phone = model.PhoneNumber;
            _registion.Email = model.Email;
            _registion.Status = false;
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.PasswordMD532 = _registion.PasswordMD532;
            return _registion;
        }
    }
}