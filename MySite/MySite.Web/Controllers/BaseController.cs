using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MySite.Web.Controllers
{
    
    public class BaseController : Controller
    {
        public void PreAction()
        {
            bool IsCookieAuth = HttpContext.User.Identity.IsAuthenticated;
            ViewData["IsAuth"] = IsCookieAuth;
        }
    }
}