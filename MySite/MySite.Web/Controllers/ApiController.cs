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
        
        [Route("{name?}")]
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
        
        [Route("{name?}")]
        [HttpGet("ID")]
        public string GetDetails1(string ID)
        {
            return "abc";
        }
    }
}