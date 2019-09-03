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
        private readonly UserManager<ApplicationUser> _userManager;

        public class DisplayAttend
        {
            public string JMBAG { get; set; }
            public DateTime LoggedTime { get; set; }

            public DisplayAttend(string jMBAG, DateTime loggedTime)
            {
                JMBAG = jMBAG;
                LoggedTime = loggedTime;
            }
        }

        public ClassAttendsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
                classAttends= await _context.ClassAttends.Where(ca => ca.IdentityUserId == currentUser.Id).ToListAsync();
            }
            else {
                classAttends = await _context.ClassAttends.ToListAsync();
            }

            List<DisplayAttend> data = new List<DisplayAttend>();

            foreach (ClassAttend ca in classAttends)
            {
                data.Add(new DisplayAttend(UserList.FirstOrDefault(u => u.Id == ca.IdentityUserId).JMBAG, ca.LoggedTime));
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

            ClassAttend classAttend = new ClassAttend(currentUser.Id, DateTime.Now);

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
