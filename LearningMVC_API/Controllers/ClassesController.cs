using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearningMVC_API.Data;

namespace LearningMVC_API
{
    public class ClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: ClassModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.classModel.ToListAsync());
        }

        // GET: ClassModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classModel = await _context.classModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (classModel == null)
            {
                return NotFound();
            }

            return View(classModel);
        }

        // GET: ClassModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,StartTime,EndTime,Capacity")] ClassModel classModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classModel);
        }

        // GET: ClassModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classModel = await _context.classModel.SingleOrDefaultAsync(m => m.Id == id);
            if (classModel == null)
            {
                return NotFound();
            }
            return View(classModel);
        }

        // POST: ClassModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,StartTime,EndTime,Capacity")] ClassModel classModel)
        {
            if (id != classModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassModelExists(classModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(classModel);
        }

        // GET: ClassModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classModel = await _context.classModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (classModel == null)
            {
                return NotFound();
            }

            return View(classModel);
        }

        // POST: ClassModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classModel = await _context.classModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.classModel.Remove(classModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassModelExists(int id)
        {
            return _context.classModel.Any(e => e.Id == id);
        }
    }
}
