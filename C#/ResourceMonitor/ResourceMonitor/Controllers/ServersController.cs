﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourceMonitor.Data;

namespace ResourceMonitor
{
    public class ServersController : Controller
    {
        private readonly MtaresourceMonitoringContext _context;

        public ServersController(MtaresourceMonitoringContext context)
        {
            _context = context;
        }

        // GET: Servers
        public async Task<IActionResult> Index()
        {
              return _context.Servers != null ? 
                          View(await _context.Servers.ToListAsync()) :
                          Problem("Entity set 'MtaresourceMonitoringContext.Servers'  is null.");
        }

        // GET: Servers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servers == null)
            {
                return NotFound();
            }

            var server = await _context.Servers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (server == null)
            {
                return NotFound();
            }

            return View(server);
        }

        // GET: Servers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServerName,Recipients,PreviousState,CurrentState,LastCheck,LastEmailsent,TimeInterval")] Server server)
        {
            if (ModelState.IsValid)
            {
                _context.Add(server);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(server);
        }

        // GET: Servers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Servers == null)
            {
                return NotFound();
            }

            var server = await _context.Servers.FindAsync(id);
            if (server == null)
            {
                return NotFound();
            }
            return View(server);
        }

        // POST: Servers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServerName,Recipients,PreviousState,CurrentState,LastCheck,LastEmailsent,TimeInterval")] Server server)
        {
            if (id != server.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(server);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServerExists(server.Id))
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
            return View(server);
        }

        // GET: Servers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Servers == null)
            {
                return NotFound();
            }

            var server = await _context.Servers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (server == null)
            {
                return NotFound();
            }

            return View(server);
        }

        // POST: Servers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servers == null)
            {
                return Problem("Entity set 'MtaresourceMonitoringContext.Servers'  is null.");
            }
            var server = await _context.Servers.FindAsync(id);
            if (server != null)
            {
                _context.Servers.Remove(server);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServerExists(int id)
        {
          return (_context.Servers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
