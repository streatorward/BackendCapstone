using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackEndCapstone.Data;
using BackEndCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BackEndCapstone.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets
                .Include(t => t.ApplicationUser)
                .Include(t => t.Department)
                .Include(t => t.Employee);
               
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult CompletedTickets()
        {
            var applicationDbContext = _context.Tickets
                .Include(t => t.ApplicationUser)
                .Include(t => t.Department)
                .Include(t => t.EmployeeTickets)
                .Include(t => t.Employee)
                .Where(t => t.IsComplete == true).ToList();
            return View(applicationDbContext);
        }

        public IActionResult NotCompletedTickets()
        {
            var applicationDbContext = _context.Tickets
                .Include(t => t.ApplicationUser)
                .Include(t => t.Department)
                .Include(t => t.EmployeeTickets)
                .Include(t => t.Employee)
                .Where(t => t.IsComplete == false).ToList();


            return View(applicationDbContext);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.ApplicationUser)
                .Include(t => t.Department)
                .Include(t => t.Employee)
                .Include(t => t.EmployeeTickets)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            var user = await GetCurrentUserAsync();
            
            ModelState.Remove("User");
            ModelState.Remove("ApplicationUserId");

            if (ModelState.IsValid)
            {
                ticket.ApplicationUser = user;
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(NotCompletedTickets));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", ticket.ApplicationUserId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", ticket.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", ticket.EmployeeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", ticket.ApplicationUserId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", ticket.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", ticket.EmployeeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(NotCompletedTickets));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", ticket.ApplicationUserId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", ticket.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", ticket.EmployeeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.ApplicationUser)
                .Include(t => t.Department)
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketId == id);
        }

        public async Task<IActionResult> MarkComplete(int id)
        {
            if (id != 0)
            {
                var ticket = await _context.Tickets.FindAsync(id);
                ticket.IsComplete = !ticket.IsComplete;
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CompletedTickets));
            }
            return RedirectToAction(nameof(CompletedTickets));
        }

        public async Task<IActionResult> OtherMarkComplete(int id)
        {
            if (id != 0)
            {
                var ticket = await _context.Tickets.FindAsync(id);
                ticket.IsComplete = !ticket.IsComplete;
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(NotCompletedTickets));
            }
            return RedirectToAction(nameof(NotCompletedTickets));
        }
    }
}
