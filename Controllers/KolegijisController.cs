using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Studenti.Data;
using Studenti.Models;

namespace Studenti.Controllers
{
    public class KolegijisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KolegijisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kolegijis


        public async Task<IActionResult> Index(string sortOrder, string searchString)

        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;


            var li = from s in _context.Korisnik
                     select s;

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    li = li.OrderBy(s => s.Name);
                    break;
                case "date_desc":
                    li = li.OrderByDescending(s => s.Surname);
                    break;
                default:
                    li = li.OrderBy(s => s.Name);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.Name.Contains(searchString)
                                       || s.Surname.Contains(searchString));
            }



            return View(await li.AsNoTracking().ToListAsync());
        }
    }
}

           
        

    
      
