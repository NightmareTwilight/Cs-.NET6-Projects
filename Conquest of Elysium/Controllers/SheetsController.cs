using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Conquest_of_Elysium.Data;
using Conquest_of_Elysium.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Conquest_of_Elysium.Controllers
{
    public class SheetsController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public SheetsController(ApplicationDbContext context, UserManager<User> userManager,
			SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

	    // GET: Sheets
        [Authorize]
	    public async Task<IActionResult> Index()
        {
			var user = await _userManager.GetUserAsync(User);
			var applicationDbContext = _context.Sheets.Where(s => s.User == user);
            return View(await applicationDbContext.ToListAsync());
		}

		// GET: Sheets
		[Authorize]
		public async Task<IActionResult> All()
		{
			var applicationDbContext = _context.Sheets.Include(s => s.User);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Sheets/Details/5
		[Authorize]
		public async Task<IActionResult> Details(Guid? id)
		{

			if (id == null || _context.Sheets == null)
            {
                return NotFound();
            }

            var sheet = await _context.Sheets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sheet == null)
            {
                return NotFound();
            }

            return View(sheet);
        }

		// GET: Sheets/Create
		[Authorize]
		public IActionResult Create()
		{

			return View();
        }

        // POST: Sheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create([Bind("Id,Name,Health,MaxHealth,Race,Class,UserID")] Sheet sheet)
        {
			var user = await _userManager.GetUserAsync(User);

			if (ModelState.IsValid)
            {
				sheet.Id = Guid.NewGuid();
                sheet.User = user;
                sheet.Level = 1;
                sheet.MaxHealth = sheet.Health;
                sheet.Insanity = 0;
                _context.Add(sheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sheet);
        }

		// GET: Sheets/Edit/5
		[Authorize]
		public async Task<IActionResult> Edit(Guid? id)
        {
			if (id == null || _context.Sheets == null)
            {
                return NotFound();
            }

            var sheet = await _context.Sheets.FindAsync(id);
            if (sheet == null)
            {
                return NotFound();
            }
            return View(sheet);
        }

        // POST: Sheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,MaxHealth,Health,UserID,Head,Chest,LArm,RArm,LLeg,RLeg,Divine,Level,Class,WeS,RaS,MaI,DiI,INT,PyR,MaR,MeR,STR,TNK,SpC,DEX,AWE,Items,Effects,Bless,Active,Passive,Race")] Sheet sheet)
        {
			if (id != sheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SheetExists(sheet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(sheet);
        }

		// GET: Sheets/Delete/5
		[Authorize]
		public async Task<IActionResult> Delete(Guid? id)
        {
			if (id == null || _context.Sheets == null)
            {
                return NotFound();
            }

            var sheet = await _context.Sheets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sheet == null)
            {
                return NotFound();
            }

            return View(sheet);
        }

        // POST: Sheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
			if (_context.Sheets == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Sheets'  is null.");
            }
            var sheet = await _context.Sheets.FindAsync(id);
            if (sheet != null)
            {
                _context.Sheets.Remove(sheet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SheetExists(Guid id)
        {
          return _context.Sheets.Any(e => e.Id == id);
        }
    }
}
