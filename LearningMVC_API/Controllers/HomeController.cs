using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningMVC_API.Models;
using LearningMVC_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LearningMVC_API.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ClassModel ChosenClass;
        ReservationModels reserved_Class = new ReservationModels();

        public HomeController(ApplicationDbContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Contact()
        {
           return View( await _context.Users.ToListAsync());

           
        }


        public async Task<IActionResult> ClassCatalogue()
        {
            return View(await _context.classModel.ToListAsync());
        }


        [Authorize]
        public async Task<IActionResult> ReserveClass(int? Id)
        {


            if (Id == null)
            {
                return NotFound();
            }

            var ClassModels = await _context.classModel.SingleOrDefaultAsync(m => m.Id == Id);
            ChosenClass = _context.classModel.SingleOrDefault(m => m.Id == Id);

            reserved_Class.Class = ChosenClass;


            var currentUser = await _userManager.GetUserAsync(User);
            var currentClass = _context.reservedModel.Where(m => m.User == currentUser).Select(c => c.Class == ChosenClass);

            reserved_Class.User = currentUser;

            ChosenClass.Capacity = ChosenClass.Capacity - 1;
            if (ChosenClass.Capacity <= 0)
            {
                ChosenClass.Capacity = 0;
                ViewData["Message"] = "Class Is Already at full capacity";
                return View(ClassModels);
            }
            else if (ChosenClass.EndTime >= DateTime.Today)
            {
                ViewData["Message"] = "Class is expired";
                return View(ClassModels);
            }
            else if (currentClass.Count() >= 1)
            {

                ViewData["Message"] = "You Already reserved this class";
                return View(ClassModels);
            }
            else
            {
                _context.reservedModel.Add(reserved_Class);
                await _context.SaveChangesAsync();

               ViewData["Message"]= "Class reserved successfully";
                return View(ClassModels);

            }

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
