using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTAIntranetAngular.API.Data;
using MTAIntranetAngular.API.Data.Models;
using MTAIntranetAngular.Utility;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Security.Policy;
using static HotChocolate.ErrorCodes;
using Microsoft.AspNetCore.Authorization;

namespace MTAIntranetAngular.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class WebsitesController : ControllerBase
    {
        private readonly MtaresourceMonitoringContext _context;

        public WebsitesController(MtaresourceMonitoringContext context)
        {
            _context = context;
        }

        [Route("Monitor")]
        public async Task<ActionResult<IEnumerable<Website>>> Monitor()
        {
            foreach (Website w in _context.Websites)
            {
                Uri serverUri = new Uri("http://" + w.ServerName);

                // needs UriKind arg, or UriFormatException is thrown
                Uri relativeUri = new Uri(w.WebsiteName ?? "null", UriKind.Relative);

                // Uri(Uri, Uri) is the preferred constructor in this case
                Uri fullUri = new Uri(serverUri, relativeUri);
                try
                {
                    using var ping = new HttpClient();
                    var reply = await ping.GetAsync(fullUri);
                    switch (reply.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            var msg =
                                $"PING/ICMP to {w.WebsiteName} succees.";
                            w.PreviousState = w.CurrentState;
                            w.CurrentState = "Healthy";
                            w.LastCheck = DateTime.Now;
                            //_context.SaveChanges();
                            break;
                        default:
                            var err =
                                $"PING/ICMP to {w.WebsiteName} failed";
                            w.PreviousState = w.CurrentState;
                            w.CurrentState = "Unhealthy";
                            w.LastCheck = DateTime.Now;
                            //_context.SaveChanges();
                            break;
                    }
                }
                catch (Exception e)
                {
                    var err =
                        $"PING/ICMP to {w.WebsiteName} failed {e.Message}";
                    w.PreviousState = w.CurrentState;
                    w.CurrentState = "Unhealthy";
                    w.LastCheck = DateTime.Now;
                    //_context.SaveChanges();
                }
                if (w.PreviousState == "Unknown" &&
                    w.CurrentState == "Unhealthy")
                {
                    // failed to initially connect
                    EmailConfiguration.SendWebsiteFailure(w);
                    w.LastEmailsent = DateTime.Now;
                    //_context.SaveChanges();
                }
                else if (w.PreviousState == "Unknown" &&
                    w.CurrentState == "Healthy")
                {
                    // successful initial connection
                    EmailConfiguration.SendWebsiteInitSuccess(w);
                    w.LastEmailsent = DateTime.Now;
                    //_context.SaveChanges();
                }
                else if (w.PreviousState == "Healthy" &&
                    w.CurrentState == "Unhealthy")
                {
                    // website failure
                    EmailConfiguration.SendWebsiteFailure(w);
                    w.LastEmailsent = DateTime.Now;
                    //_context.SaveChanges();
                }
                else if (w.PreviousState == "Unhealthy" &&
                    w.CurrentState == "Healthy")
                {
                    // send successful restoration message
                    EmailConfiguration.SendWebsiteRestored(w);
                    w.LastEmailsent = DateTime.Now;
                    //_context.SaveChanges();
                }
                else if (w.TimeInterval != 0 &&
                    w.PreviousState == "Unhealthy" &&
                    w.CurrentState == "Unhealthy" &&
                    (w.LastEmailsent.AddMinutes(Convert.ToDouble(w.TimeInterval))
                        <= DateTime.Now))
                {
                    // process failure reminder
                    EmailConfiguration.SendWebsiteFailure(w);
                    w.LastEmailsent = DateTime.Now;
                    //_context.SaveChanges();
                }
            }
            _context.SaveChanges();
            return await _context.Websites.ToListAsync();
        }

        // GET: api/Websites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Website>>> GetWebsites()
        {
            return await _context.Websites.ToListAsync();
        }

        // GET: api/Websites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Website>> GetWebsite(int id)
        {
            var website = await _context.Websites.FindAsync(id);

            if (website == null)
            {
                return NotFound();
            }

            return website;
        }

        // PUT: api/Websites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebsite(int id, Website website)
        {
            if (id != website.Id)
            {
                return BadRequest();
            }

            _context.Entry(website).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebsiteExists(id))
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

        // POST: api/Websites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Website>> PostWebsite(Website website)
        {
            _context.Websites.Add(website);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WebsiteExists(website.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWebsite", new { id = website.Id }, website);
        }

        // DELETE: api/Websites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebsite(int id)
        {
            var website = await _context.Websites.FindAsync(id);
            if (website == null)
            {
                return NotFound();
            }

            _context.Websites.Remove(website);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebsiteExists(int id)
        {
            return _context.Websites.Any(e => e.Id == id);
        }
    }
}
