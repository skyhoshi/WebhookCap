using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Catcher_WebAPI.Data.CanvasData;
using Catcher_WebAPI.Models.Tables;
using Microsoft.AspNetCore.Authorization;

namespace Catcher_WebAPI.Controllers
{
    [Authorize]
    public class MySubmissionsController : Controller
    {
        private readonly GoCanvasDbContext _context;

        public MySubmissionsController(GoCanvasDbContext context)
        {
            _context = context;
        }

        // GET: MySubmissions
        public async Task<IActionResult> Index()
        {
            return View(await _context.MySubmissions.ToListAsync());
        }

        // GET: MySubmissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mySubmissions = await _context.MySubmissions
                .FirstOrDefaultAsync(m => m.LineID == id);
            if (mySubmissions == null)
            {
                return NotFound();
            }

            return View(mySubmissions);
        }

        // GET: MySubmissions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MySubmissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineID,GoCanSubmissionGUID,FormName")] MySubmissions mySubmissions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mySubmissions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mySubmissions);
        }

        // GET: MySubmissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mySubmissions = await _context.MySubmissions.FindAsync(id);
            if (mySubmissions == null)
            {
                return NotFound();
            }
            return View(mySubmissions);
        }

        // POST: MySubmissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LineID,GoCanSubmissionGUID,FormName")] MySubmissions mySubmissions)
        {
            if (id != mySubmissions.LineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mySubmissions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MySubmissionsExists(mySubmissions.LineID))
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
            return View(mySubmissions);
        }

        // GET: MySubmissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mySubmissions = await _context.MySubmissions
                .FirstOrDefaultAsync(m => m.LineID == id);
            if (mySubmissions == null)
            {
                return NotFound();
            }

            return View(mySubmissions);
        }

        // POST: MySubmissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mySubmissions = await _context.MySubmissions.FindAsync(id);
            _context.MySubmissions.Remove(mySubmissions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MySubmissionsExists(int id)
        {
            return _context.MySubmissions.Any(e => e.LineID == id);
        }
    }
}
