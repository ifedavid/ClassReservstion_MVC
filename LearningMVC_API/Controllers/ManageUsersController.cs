using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningMVC_API.Data;
using LearningMVC_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningMVC_API.Controllers
{
    [Authorize(Roles ="Executive")]
    public class ManageUsersController : Controller
    {

        private readonly ApplicationDbContext _context;
        public UserManager<ApplicationUser> _userManager;

        public ManageUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());


        }

        public async Task<IActionResult> GiveAdminRights(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var CurrentUser = await _userManager.FindByIdAsync(id);
           if (CurrentUser == null)
            {
                return NotFound();
            }

            await _userManager.AddToRoleAsync(CurrentUser, "Admin");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveAdminRights(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var CurrentUser = await _userManager.FindByIdAsync(id);
            if (CurrentUser == null)
            {
                return NotFound();
            }

            await _userManager.RemoveFromRoleAsync(CurrentUser, "Admin");
            return RedirectToAction("Index");
        }
    }
}