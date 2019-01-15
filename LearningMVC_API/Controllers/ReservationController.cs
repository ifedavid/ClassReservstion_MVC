using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearningMVC_API.Data;
using Microsoft.AspNetCore.Identity;
using LearningMVC_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace LearningMVC_API.Controllers
{
    public class ReservationController : Controller
    {
        public string Message { get; set; }
        private readonly ApplicationDbContext _context;
        public ClassModel ChosenClass { get; set; }
        public UserManager<ApplicationUser> _userManager;
        public SignInManager<ApplicationUser> signInManager;
        public ReservationModels reserved_Class = new ReservationModels();
        public ClassModel classModel;

        public ReservationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {



            var Current_user = await _userManager.GetUserAsync(User);

            if (Current_user == null)
            {
                RedirectToPage("/Account/Login");
            }
            else
            {


                return View( _context.reservedModel.Where(m => m.User == Current_user).Select(c => c.Class).Distinct().ToList());

            }

            return View();

        }

        public async Task<IActionResult> DeleteReservation(int? Id)
        {

            var Current_user = await _userManager.GetUserAsync(User);


            if (Id == null)
            {
                return NotFound();
            }


            classModel = _context.classModel.SingleOrDefault(m => m.Id == Id);

            var somethingelse = _context.reservedModel.Where(m => m.User == Current_user).FirstOrDefault(c => c.Class == classModel);
            if (somethingelse != null)
            {

                _context.reservedModel.Remove(somethingelse);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index");
        }
       
       
    }
}