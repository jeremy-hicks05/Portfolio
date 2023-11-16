using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using HealthCheck.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MTAIntranetAngular.API.Data;
using MTAIntranetAngular.API.Data.Models;
using MTAIntranetAngular.Utility;

namespace MTAIntranetAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly MtaresourceMonitoringContext _context;

        public ServersController(MtaresourceMonitoringContext context)
        {
            _context = context;
        }

        // GET: api/Servers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Server>>> GetServers()
        {
            // check status of each and every server

            // if previous status was 'Healthy'
            // and current status is 'Unhealthy'
            // send email notification
            foreach (Server s in _context.Servers)
            {
                try
                {
                    using var ping = new Ping();
                    var reply = await ping.SendPingAsync(s.ServerName);
                    switch (reply.Status)
                    {
                        case IPStatus.Success:
                            var msg =
                                $"ICMP to {s.ServerName} took {reply.RoundtripTime} ms.";
                            s.PreviousState = s.CurrentState;
                            s.CurrentState = "Healthy";
                            _context.SaveChanges();
                            break;
                        default:
                            var err =
                                $"ICMP to {s.ServerName} failed: {reply.Status}";
                            s.PreviousState = s.CurrentState;
                            s.CurrentState = "Unhealthy";
                            _context.SaveChanges();
                            break;
                    }
                }
                catch (Exception e)
                {
                    var err =
                        $"ICMP to {s.ServerName} failed {e.Message}";
                    s.PreviousState = s.CurrentState;
                    s.CurrentState = "Unhealthy";
                    _context.SaveChanges();
                }
                if (s.PreviousState == "Unknown" && 
                    s.CurrentState == "Unhealthy")
                {
                    EmailConfiguration.SendServerFailure(s.ServerName ?? "Unknown");
                }
                else if (s.PreviousState == "Unknown" &&
                    s.CurrentState == "Healthy")
                {
                    EmailConfiguration.SendServerFailure(s.ServerName ?? "Unknown");
                }
                else if (s.PreviousState == "Healthy" &&
                    s.CurrentState == "Unhealthy")
                {
                    // server failure
                    EmailConfiguration.SendServerFailure(s.ServerName ?? "Unknown");
                }
                else if (s.PreviousState == "Unhealthy" &&
                    s.CurrentState == "Healthy")
                {
                    // successful restoration
                    EmailConfiguration.SendServerFailure(s.ServerName ?? "Unknown");
                }
            }

            return await _context.Servers.ToListAsync();
        }

        // GET: api/Servers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Server>> GetServer(int id)
        {
            var server = await _context.Servers.FindAsync(id);

            if (server == null)
            {
                return NotFound();
            }

            return server;
        }

        // PUT: api/Servers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServer(int id, Server server)
        {
            if (id != server.Id)
            {
                return BadRequest();
            }

            _context.Entry(server).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerExists(id))
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

        // POST: api/Servers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Server>> PostServer(Server server)
        {
            _context.Servers.Add(server);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServerExists(server.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetServer", new { id = server.Id }, server);
        }

        // DELETE: api/Servers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServer(int id)
        {
            var server = await _context.Servers.FindAsync(id);
            if (server == null)
            {
                return NotFound();
            }

            _context.Servers.Remove(server);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServerExists(int id)
        {
            return _context.Servers.Any(e => e.Id == id);
        }
    }
}
