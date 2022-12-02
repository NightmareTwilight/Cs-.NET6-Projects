using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conquest_of_Elysium.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Conquest_of_Elysium.Models;
using Microsoft.AspNetCore.Authorization;

namespace Final_Project.Controllers
{
    public class SheetObjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SheetObjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

		// GET: SheetObjects
		[Authorize]
		public async Task<IActionResult> Index()
        {
              return View(await _context.SheetObject.ToListAsync());
        }

		// GET: SheetObjects/Details/5
		[Authorize]
		public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SheetObject == null)
            {
                return NotFound();
            }

            var sheetObject = await _context.SheetObject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sheetObject == null)
            {
                return NotFound();
            }

            return View(sheetObject);
        }

		// GET: SheetObjects/Create
		[Authorize]
		public IActionResult Create()
        {
			string[] types = { "Class", "Race", "Item", "Passive Ability", "Active Ability", "Divine Attention", "Blessing", "Insanity Effect" };
			ViewData["Type"] = new SelectList(types, "Type");
			return View();
        }

        // POST: SheetObjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create([Bind("Id,Type,Name,Desc,LongDesc")] SheetObject sheetObject)
        {
            if (ModelState.IsValid)
            {
                sheetObject.Id = Guid.NewGuid();
                _context.Add(sheetObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sheetObject);
        }

		// GET: SheetObjects/Edit/5
		[Authorize]
		public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.SheetObject == null)
            {
                return NotFound();
            }

            var sheetObject = await _context.SheetObject.FindAsync(id);
            if (sheetObject == null)
            {
                return NotFound();
            }
            return View(sheetObject);
        }

        // POST: SheetObjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type,Name,Desc,LongDesc")] SheetObject sheetObject)
        {
            if (id != sheetObject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sheetObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SheetObjectExists(sheetObject.Id))
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
            return View(sheetObject);
        }

		// GET: SheetObjects/Delete/5
		[Authorize]
		public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.SheetObject == null)
            {
                return NotFound();
            }

            var sheetObject = await _context.SheetObject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sheetObject == null)
            {
                return NotFound();
            }

            return View(sheetObject);
        }

        // POST: SheetObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.SheetObject == null)
            {
                return Problem("Entity set 'ApplicationDBContext.SheetObject'  is null.");
            }
            var sheetObject = await _context.SheetObject.FindAsync(id);
            if (sheetObject != null)
            {
                _context.SheetObject.Remove(sheetObject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SheetObjectExists(Guid id)
        {
          return _context.SheetObject.Any(e => e.Id == id);
        }
    }
}
