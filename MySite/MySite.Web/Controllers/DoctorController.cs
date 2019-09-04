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

namespace MySite.Web.Controllers
{
    public class DoctorController : BaseController
    {
        private readonly DataContext _context;
        public DoctorController(DataContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Doctors.ToListAsync());
        //}
        //[Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //string ss = HttpContext.User.Identity.IsAuthenticated.ToString();
	    var logger = NLog.LogManager.GetCurrentClassLogger();
	    logger.Info("Test in Index of Doctor...");
	    var doctors = from s in _context.Doctors
                        select s;
	    var Count = doctors.Count();
	    logger.Info(Count);
	    logger.Info("Doctors has got.");
	    try
	    {
            //PreAction();
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            logger.Info("ViewData finished.");
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            logger.Info("Search string finished.");
          
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.Name.Contains(searchString) || s.Department.Contains(searchString));
            }
	    logger.Info("Start Switch.");
            switch (sortOrder)
            {
                case "name_desc":
                    doctors = doctors.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    doctors = doctors.OrderBy(s => s.Department);
                    break;
                case "date_desc":
                    doctors = doctors.OrderByDescending(s => s.Department);
                    break;
                default:
                    doctors = doctors.OrderBy(s => s.Name);
                    break;
            }
	    logger.Info("Switch finished.");
	    }
	    catch(System.Exception ex)
	    {
                 logger.Info(ex.ToString());
	    }
	    logger.Info("Start to page..");
	    int pageSize = 3;
	    PagedList<Doctor> _pageListDoc = null;
	    try
	    {
	       _pageListDoc = await PagedList<Doctor>.CreateAsync(doctors.AsNoTracking(), page ?? 1, pageSize);
	    }
	    catch(System.Exception ex1)
            {
                 logger.Info(ex1.ToString());
            }
            logger.Info("Page finished..");
	    logger.Info(_pageListDoc.TotalPages); 
            return View(_pageListDoc);
        }

        
        public void GetDetails(string ID)
        {

        }
        
    }
}
