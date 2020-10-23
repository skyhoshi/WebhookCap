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

namespace Catcher_WebAPI.Areas.APITester
{
    [Authorize]
    public class EmployeeManagementController : Controller
    {
        private readonly GoCanvasDbContext _context;

        public EmployeeManagementController(GoCanvasDbContext context)
        {
            _context = context;
        }

        // GET: APITester/Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: APITester/Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblEmployee = await _context.Employees
                .FirstOrDefaultAsync(m => m.LineID == id);
            if (tblEmployee == null)
            {
                return NotFound();
            }

            return View(tblEmployee);
        }

        // GET: APITester/Employees/Create
        public IActionResult Create()
        {
            var locations = _context.Locations.Select(s => s);
            List<string> locationsAsString = locations.Select(l => l.Location).ToList();
            CatchWatchEmployee.AvailableLocationsAsStrings = locationsAsString;

            return View();
        }

        // POST: APITester/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineID,BadgeID,FirstName,LastName,Username,Location,Active,Suspended")] CatchWatchEmployee CatchWatchEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(CatchWatchEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CatchWatchEmployee);
        }

        // GET: APITester/Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var locations = _context.Locations.Where(s=>!string.IsNullOrWhiteSpace(s.Location)).Select(s => s);
            if (locations.Any())
            {
                List<SelectListItem> locationsAsSelectListItems = new List<SelectListItem>();

                foreach (CatchWatchLocations location in locations)
                {
                    locationsAsSelectListItems.Add(new SelectListItem(location.Location, location.Location));
                }

                ViewBag.LocationsAsSelectListItems = locationsAsSelectListItems;
                ViewBag.Locations = locations.ToList();
            }

            var tblEmployee = await _context.Employees.FindAsync(id);
            if (tblEmployee == null)
            {
                return NotFound();
            }
            return View(tblEmployee);
        }

        // POST: APITester/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LineID,BadgeID,FirstName,LastName,Username,Location,Active,Suspended")] CatchWatchEmployee CatchWatchEmployee)
        {
            if (id != CatchWatchEmployee.LineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CatchWatchEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblEmployeeExists(CatchWatchEmployee.LineID))
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
            return View(CatchWatchEmployee);
        }

        // GET: APITester/Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return NotFound();
        }

        // POST: APITester/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }

        private bool tblEmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.LineID == id);
        }
    }
}
