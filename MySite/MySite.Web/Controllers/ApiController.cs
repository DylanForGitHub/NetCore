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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MySite.Web.Controllers
{
    [Route("web/[controller]/[action]")]
    public class APIController : Controller
    {
        private readonly DataContext _context;
        public APIController(DataContext context)
        {
            _context = context;
        }
        
        [Route("{ID?}")]
        [HttpPost("ID")]
        public Doctor GetDetails(string ID)
        {
            var doctors = from s in _context.Doctors
                        where s.ID == ID
                        select s;
            
            if(doctors != null)
            {
                return doctors.First();
            }
            else
            {
                return null;
            }
        }
        
        [Route("{ID?}")]
        [HttpGet("ID")]
        public string GetDetails1(string ID)
        {
            return "abc";
        }

        [Route("{UserName?}")]
        [HttpGet]
        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<JsonResult> CheckUser(string UserName)
        {
            AjaxModel returnResult = new AjaxModel();
            if(String.IsNullOrEmpty(UserName))
            {
                returnResult.Status = false;
                returnResult.Message = "The user name is null or empty.";
            }
            else
            {
                //var UserCount = from s in _context.Registions
                //                where s.UserName == userName
                //                select new { Count = s.Count() };
                
                var UserCount = await _context.Registions.CountAsync(s => s.UserName.Equals(UserName));

                if(UserCount == 0)
                {
                    returnResult.Status = true;
                    returnResult.Message = "The user name doesn't exist.";
                }
                else
                {
                    returnResult.Status = true;
                    returnResult.Message = "The user name existed.";
                }
            }
            
            return Json(returnResult);
        }
    }
}