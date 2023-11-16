using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MTAIntranetAngular.API;
using MTAIntranetAngular.API.Data.Models;
using MTAIntranetAngular.Utility;

namespace MTAIntranetAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly MtaresourceMonitoringContext _context;

        public ServicesController(MtaresourceMonitoringContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            foreach (Service s in _context.Services)
            {
                ServiceController sc = new ServiceController(s.ServiceName, s.ServerName);

                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        s.PreviousState = s.CurrentState;
                        s.CurrentState = "Healthy";
                        _context.SaveChanges();
                        break;
                    case ServiceControllerStatus.Stopped:
                        s.PreviousState = s.CurrentState;
                        s.CurrentState = "Unhealthy";
                        _context.SaveChanges();
                        break;
                        //sc.Start();
                    case ServiceControllerStatus.Paused:
                        s.PreviousState = s.CurrentState;
                        s.CurrentState = "Unhealthy";
                        _context.SaveChanges();
                        break;
                    case ServiceControllerStatus.StopPending:
                        s.PreviousState = s.CurrentState;
                        s.CurrentState = "Unhealthy";
                        _context.SaveChanges();
                        break;
                    case ServiceControllerStatus.StartPending:
                        s.PreviousState = s.CurrentState;
                        s.CurrentState = "Unhealthy";
                        _context.SaveChanges();
                        break;
                    default:
                        s.PreviousState = s.CurrentState;
                        s.CurrentState = "Unhealthy";
                        _context.SaveChanges();
                        break;
                }
                if (s.PreviousState == "Unknown" &&
                    s.CurrentState == "Unhealthy")
                {
                    // failed initial connection
                    EmailConfiguration.SendServerFailure(s.ServiceName ?? "Unknown");
                }
                else if (s.PreviousState == "Unknown" &&
                    s.CurrentState == "Healthy")
                {
                    // successful initial connection
                    EmailConfiguration.SendServerFailure(s.ServiceName ?? "Unknown");
                }
                else if (s.PreviousState == "Healthy" &&
                    s.CurrentState == "Unhealthy")
                {
                    // service failure
                    EmailConfiguration.SendServerFailure(s.ServiceName ?? "Unknown");
                }
                else if (s.PreviousState == "Unhealthy" &&
                    s.CurrentState == "Healthy")
                {
                    // send successful restoration message
                    EmailConfiguration.SendServerFailure(s.ServiceName ?? "Unknown");
                }
            }
            return await _context.Services.ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            _context.Services.Add(service);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServiceExists(service.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
