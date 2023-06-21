using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using identity_project2.Data;
using identity_project2.Models;
using Microsoft.AspNetCore.Authorization;

namespace identity_project2.Controllers
{
    
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Leads
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.salesLeadEntities != null ? 
                          View(await _context.salesLeadEntities.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.salesLeadEntities'  is null.");
        }

        // GET: Leads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.salesLeadEntities == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.salesLeadEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }

        // GET: Leads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Email,Phone,Source")] SalesLeadEntity salesLeadEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesLeadEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesLeadEntity);
        }

        // GET: Leads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.salesLeadEntities == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.salesLeadEntities.FindAsync(id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }
            return View(salesLeadEntity);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Email,Phone,Source")] SalesLeadEntity salesLeadEntity)
        {
            if (id != salesLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesLeadEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesLeadEntityExists(salesLeadEntity.Id))
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
            return View(salesLeadEntity);
        }

        // GET: Leads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.salesLeadEntities == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.salesLeadEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.salesLeadEntities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.salesLeadEntities'  is null.");
            }
            var salesLeadEntity = await _context.salesLeadEntities.FindAsync(id);
            if (salesLeadEntity != null)
            {
                _context.salesLeadEntities.Remove(salesLeadEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesLeadEntityExists(int id)
        {
          return (_context.salesLeadEntities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
