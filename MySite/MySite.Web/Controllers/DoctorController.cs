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
    public class DoctorController : Controller
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

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
                
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var doctors = from s in _context.Doctors
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.Name.Contains(searchString) || s.Department.Contains(searchString));
            }
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

            int pageSize = 10;
            return View(await PagedList<Doctor>.CreateAsync(doctors.AsNoTracking(), page ?? 1, pageSize));
        }

        
        public void GetDetails(string ID)
        {

        }
        
    }
}