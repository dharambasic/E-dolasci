using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Studenti.Data;
using Studenti.Models;
using Studenti.Utility;

namespace Studenti.Controllers
{
    public class ClassAttendsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public class DisplayAttend
        {
            public string JMBAG { get; set; }
            public DateTime LoggedTime { get; set; }

            public string Name { get; set; }
            public string Surname { get; set; }


         
            public DisplayAttend()
            {

            }

            public DisplayAttend(string name, string surname, string jMBAG, DateTime loggedTime )
            {
                JMBAG = jMBAG;
                Name = name;
                Surname = surname;
                LoggedTime = loggedTime;
             
         
            }

           
          

            
        }

        public ClassAttendsController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
          
          
        }


      


        // GET: ClassAttends
        public async Task<IActionResult> Index()
        {
           var UserList = await _userManager.Users.ToListAsync();
            var currentUser = await _userManager.GetUserAsync(User);
            bool isStudent = await _userManager.IsInRoleAsync(currentUser, StaticDetails.StudentRole);
            List<ClassAttend> classAttends = null;
            
            if (isStudent)
            {   
                classAttends = await _context.ClassAttends.Where(ca => ca.IdentityUserId == currentUser.Id).ToListAsync();
               
               
            }
            else {

                classAttends = await _context.ClassAttends.ToListAsync();
            }

           List<DisplayAttend> data = new List<DisplayAttend>();

            foreach (ClassAttend ca in classAttends)
            {
                data.Add(new DisplayAttend(UserList.FirstOrDefault(u => u.Id == ca.IdentityUserId).Name, UserList.FirstOrDefault (u=> u.Id == ca.IdentityUserId).Surname, UserList.FirstOrDefault(u => u.Id == ca.IdentityUserId).JMBAG, ca.LoggedTime));


            }
            
            return View(data);
        }

        // POST: ClassAttends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string t)
        {

            var currentUser = await _userManager.GetUserAsync(User);

            ClassAttend classAttend = new ClassAttend(currentUser.Id, currentUser.Name, currentUser.Surname, DateTime.Now);

            if (ModelState.IsValid)
            {
                _context.Add(classAttend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classAttend);
        }


       
    }
    }