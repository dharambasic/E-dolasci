using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Studenti.Data;
using static Studenti.Models.ClassAttend;
using Studenti.Controllers;


namespace Studenti.Controllers
{
    public class KorisniksController : Controller
    {
        private readonly ApplicationDbContext _context;







        public KorisniksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Korisniks
        public async Task<IActionResult> Index(string searchString)

        {



            ViewData["CurrentFilter"] = searchString;


            var li = from s in _context.Korisnik
                     select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.Name.Contains(searchString)
                                       || s.Surname.Contains(searchString));
            }



            return View(await li.ToListAsync());
        }
    }

}

       