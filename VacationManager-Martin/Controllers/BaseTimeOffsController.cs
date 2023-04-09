using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VacationManager_Martin.Data;
using VacationManager_Martin.Data.Entities.TimeOffs;

namespace VacationManager_Martin.Controllers
{
    [Authorize]
    public class BaseTimeOffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaseTimeOffsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BaseTimeOffs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BaseTimeOff.Include(b => b.Requestor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BaseTimeOffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BaseTimeOff == null)
            {
                return NotFound();
            }

            var baseTimeOff = await _context.BaseTimeOff
                .Include(b => b.Requestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseTimeOff == null)
            {
                return NotFound();
            }

            return View(baseTimeOff);
        }

        // GET: BaseTimeOffs/Create
        public IActionResult Create()
        {
            ViewData["RequestorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BaseTimeOffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,From,To,CreatedOn,IsHalfDay,IsApproved,RequestorId")] BaseTimeOff baseTimeOff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baseTimeOff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestorId"] = new SelectList(_context.Users, "Id", "Id", baseTimeOff.RequestorId);
            return View(baseTimeOff);
        }

        // GET: BaseTimeOffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BaseTimeOff == null)
            {
                return NotFound();
            }

            var baseTimeOff = await _context.BaseTimeOff.FindAsync(id);
            if (baseTimeOff == null)
            {
                return NotFound();
            }
            ViewData["RequestorId"] = new SelectList(_context.Users, "Id", "Id", baseTimeOff.RequestorId);
            return View(baseTimeOff);
        }

        // POST: BaseTimeOffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,From,To,CreatedOn,IsHalfDay,IsApproved,RequestorId")] BaseTimeOff baseTimeOff)
        {
            if (id != baseTimeOff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baseTimeOff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaseTimeOffExists(baseTimeOff.Id))
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
            ViewData["RequestorId"] = new SelectList(_context.Users, "Id", "Id", baseTimeOff.RequestorId);
            return View(baseTimeOff);
        }

        // GET: BaseTimeOffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BaseTimeOff == null)
            {
                return NotFound();
            }

            var baseTimeOff = await _context.BaseTimeOff
                .Include(b => b.Requestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseTimeOff == null)
            {
                return NotFound();
            }

            return View(baseTimeOff);
        }

        // POST: BaseTimeOffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BaseTimeOff == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BaseTimeOff'  is null.");
            }
            var baseTimeOff = await _context.BaseTimeOff.FindAsync(id);
            if (baseTimeOff != null)
            {
                _context.BaseTimeOff.Remove(baseTimeOff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaseTimeOffExists(int id)
        {
          return (_context.BaseTimeOff?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
