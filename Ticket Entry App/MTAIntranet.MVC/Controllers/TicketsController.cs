using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTAIntranet.SQL.API;
using MTAIntranet.MVC.Models;
using MTAIntranet.MVC.Utility;
using System.Diagnostics;
using MTAIntranet.SQL.Entities;
using Microsoft.EntityFrameworkCore;
using MTAIntranet.MVC.Models.ViewModels;

namespace MTAIntranetMVC.Controllers
{
    //[Authorize(Roles = AccessRoles.ITS)]
    public class TicketsController : Controller
    {
        private readonly MtaticketsContext db;

        public TicketsController(MtaticketsContext injectedContext)
        {
            db = injectedContext;
        }

        // --- APPROVAL STATES --- //
        #region ApprovalStates

        // CREATE APPROVAL STATES GET
        [HttpGet]
        [Route("Tickets/AddApprovalState")]
        public IActionResult AddApprovalState()
        {
            return View();
        }

        // CREATE APPROVAL STATES POST
        [HttpPost]
        [Route("Tickets/AddApprovalState")]
        public IActionResult AddApprovalState(ApprovalStateViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApprovalState approvalState = new();
                approvalState.Name = model.Name;
                db.Add(approvalState);
                db.SaveChanges();
            }
            return RedirectToAction("ApprovalStates", "Tickets");
        }

        // READ SINGLE OR ALL
        [HttpGet]
        [Route("Tickets/ApprovalStates/{id?}")]
        public IActionResult ApprovalStates(int? id)
        {
            IEnumerable<ApprovalState> model =
                db.ApprovalStates
                .Include(state => state.Tickets);
            if (id is not null)
            {
                return View(model.First(a => a.ApprovalStateId == id));
            }
            return View(model);
        }

        // UPDATE APPROVAL STATES GET
        [HttpGet]
        [Route("Tickets/UpdateApprovalState/{id}")]
        public IActionResult UpdateApprovalState(int id)
        {
            ApprovalState? model = db.ApprovalStates.FirstOrDefault(a => a.ApprovalStateId == id);
            return View(model);
        }

        // UPDATE APPROVAL STATES POST
        [HttpPost]
        [Route("Tickets/UpdateApprovalState/{id}")]
        public IActionResult UpdateApprovalState(ApprovalState approvalState)
        {
            if (ModelState.IsValid)
            {
                db.Update(approvalState);
                db.SaveChanges();
                return RedirectToAction("ApprovalStates", "Tickets");
            }
            return View();
        }

        // DELETE APPROVAL STATES GET
        [HttpGet]
        [Route("Tickets/DelApprovalState/{id}")]
        public IActionResult DelApprovalState(int id)
        {
            ApprovalState? approvalState =
                db.ApprovalStates
                .Where(a => a.ApprovalStateId == id)
                .Include(state => state.Tickets)
                .FirstOrDefault();
            return View(approvalState);
        }
        // DELETE APPROVAL STATES POST
        [HttpPost]
        [Route("Tickets/DelApprovalState/{id}")]
        public IActionResult DelApprovalState(ApprovalState model)
        {
            if (ModelState.IsValid)
            {
                db.Remove(model);
                db.SaveChanges();
                return RedirectToAction("ApprovalStates", "Tickets");
            }
            return View();
        }
        #endregion
        // -- END APPROVAL STATES -- //

        // --- IMPACTS --- //
        #region Impacts

        // CREATE IMPACT GET //
        [HttpGet]
        [Route("Tickets/AddImpact")]
        public IActionResult AddImpact()
        {
            return View();
        }

        // CREATE IMPACT POST //
        [HttpPost]
        [Route("Tickets/AddImpact")]
        public IActionResult AddImpact(ImpactViewModel model)
        {
            if (ModelState.IsValid)
            {
                Impact impact = new();
                impact.Description = model.Description;

                db.Add(impact);
                db.SaveChanges();

                return RedirectToAction("Impacts", "Tickets");
            }
            return View();
        }

        // READ IMPACT GET
        [HttpGet]
        [Route("Tickets/Impacts")]
        public IActionResult Impacts()
        {
            IEnumerable<Impact> model = db.Impacts;
            return View(model);
        }

        // UPDATE IMPACT GET
        [HttpGet]
        [Route("Tickets/UpdateImpact/{id}")]
        public IActionResult UpdateImpact(int id)
        {
            Impact impact = db.Impacts.First(i => i.ImpactId == id);

            return View(impact);
        }

        // UPDATE IMPACT POST
        [HttpPost]
        [Route("Tickets/UpdateImpact/{id}")]
        public IActionResult UpdateImpact(Impact impact)
        {
            if (ModelState.IsValid)
            {
                db.Update(impact);
                db.SaveChanges();
                return RedirectToAction("Impacts", "Tickets");
            }
            return View();
        }

        // DELETE IMPACT GET
        [HttpGet]
        [Route("Tickets/DelImpact/{id}")]
        public IActionResult DelImpact(int id)
        {
            Impact impact = db.Impacts.First(i => i.ImpactId == id);

            return View(impact);
        }

        // DELETE IMPACT POST
        [HttpPost]
        [Route("Tickets/DelImpact/{id}")]
        public IActionResult DelImpact(Impact impact)
        {
            db.Remove(impact);
            db.SaveChanges();
            return RedirectToAction("Impacts", "Tickets");
        }
        #endregion
        // -- END IMPACTS -- //

        // --- TICKETS --- //
        #region Tickets

        // CREATE TICKET GET
        [HttpGet]
        [Route("Tickets/AddTicket")]
        public IActionResult AddTicket()
        {
            TicketViewModel model = new();
            model.FillLists(db);
            return View(model);
        }

        // CREATE TICKET POST
        [HttpPost]
        [Route("Tickets/Addticket")]
        public IActionResult AddTicket(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ticket newTicket = Utility.ConvertFromTicketViewModelToTicket(db, model);

                db.Add(newTicket);
                db.SaveChanges();

                EmailConfiguration.SendTicketInfoTo(newTicket);

                if (newTicket.ApprovalState.Name == "Needs Approval")
                {
                    EmailConfiguration.SendApprovalRequestToManager(newTicket);
                }
                else if (newTicket.ApprovalState.Name == "Does not need approval")
                {
                    EmailConfiguration.SendEmailToKACE(newTicket);
                }
                return RedirectToAction("Tickets");
            }

            model.FillLists(db);

            return View(model);
        }

        // READ TICKET(s)
        [HttpGet]
        [Route("Tickets/{id?}")]
        public IActionResult Tickets(int? id)
        {
            IEnumerable<Ticket> model;

            if (id is not null)
            {
                model = db.Tickets
                    .Include(t => t.Category)
                    .Include(t => t.ApprovalState)
                    .Include(t => t.Impact)
                    .Include(t => t.SubType)
                    .Where(t => t.TicketId == id);

                return View(model);
            }
            else
            {
                model = db.Tickets
                    .Include(t => t.Category)
                    .Include(t => t.ApprovalState)
                    .Include(t => t.Impact)
                    .Include(t => t.SubType);

                return View(model);
            }
        }

        //READ TICKETS BY LOGGED IN USER
        [HttpGet]
        [Route("Tickets/MyTickets")]
        public IActionResult MyTickets()
        {
            if (db.Tickets is not null)
            {
                return View(db.Tickets
                    .Include(t => t.Category)
                    .Include(t => t.ApprovalState)
                    .Include(t => t.Impact)
                    .Include(t => t.SubType)
                    .Where(t => t.EnteredByUser ==
                        (User.Identity == null ? "Anonymous" : User.Identity.Name)));
            }
            return NotFound();
        }

        //READ APPROVED TICKETS
        [HttpGet]
        [Route("Tickets/Approved")]
        public IActionResult ApprovedTickets()
        {
            if (db.Tickets is not null)
            {
                return View(db.Tickets
                    .Include(t => t.Category)
                    .Include(t => t.ApprovalState)
                    .Include(t => t.Impact)
                    .Include(t => t.SubType)
                    .Where(t => t.ApprovalState.Name == "Approved"));
            }
            return NotFound();
        }

        //READ UNAPPROVED TICKETS
        [HttpGet]
        [Route("Tickets/Unapproved")]
        public IActionResult UnapprovedTickets()
        {
            if (db.Tickets is not null)
            {
                return View(db.Tickets
                    .Include(t => t.Category)
                    .Include(t => t.ApprovalState)
                    .Include(t => t.Impact)
                    .Include(t => t.SubType)
                    .Where(t => t.ApprovalState.Name != "Approved"));
            }
            return NotFound();
        }

        // UPDATE TICKET GET
        [HttpGet]
        [Route("Tickets/UpdateTicket/{id}")]
        public IActionResult UpdateTicket(int id)
        {
            TicketViewModel model = new();
            if (ModelState.IsValid)
            {
                Ticket? ticket = db.Tickets
                    .Include(t => t.ApprovalState)
                    .Include(t => t.Impact)
                    .Include(t => t.Category)
                    .Include(t => t.SubType)
                    //.Include(t => t.ApprovedBy)
                    .First(t => t.TicketId == id);

                ticket.DateLastUpdated = DateTime.Now;
                model.DateLastUpdated = DateTime.Now;

                model = Utility.ConvertFromTicketToViewModel(ticket);

                model.FillLists(db);

                // TODO: Only send if change occurred, or grey out "Save" in view if no change in data
                EmailConfiguration.SendTicketInfoTo(ticket);

                return View(model);
            }
            return View();
        }

        // UPDATE TICKET POST
        [HttpPost]
        [Route("Tickets/UpdateTicket/{id}")]
        public IActionResult UpdateTicket(int id, TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = Utility.ConvertFromTicketViewModelToTicket(db, model);
                ticket.TicketId = id;

                ticket.DateLastUpdated = DateTime.Now;

                db.Update(ticket);
                db.SaveChanges();
                return RedirectToAction("Tickets");
            }

            model.FillLists(db);

            return View(model);
        }

        // DELETE TICKET GET
        [HttpGet]
        [Route("Tickets/DelTicket/{id}")]
        public IActionResult DelTicket(int id)
        {
            Ticket? ticket = db.Tickets
                .Include(t => t.Category)
                .Include(t => t.ApprovalState)
                .Include(t => t.Impact)
                .FirstOrDefault(t => t.TicketId == id);

            return View(ticket);
        }

        // DELETE TICKET POST
        [HttpPost]
        [Route("Tickets/DelTicket/{id}")]
        public IActionResult DelTicket(Ticket model)
        {
            db.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Tickets");
        }

        // APPROVE TICKET GET
        [HttpGet]
        [Route("Tickets/ApproveTicket/{id}")]
        public IActionResult ApproveTicket(int id)
        {
            TicketViewModel model = new();

            Ticket? ticket = db.Tickets
                .Include(t => t.Category)
                .Include(t => t.SubType)
                .Include(t => t.ApprovalState)
                .Include(t => t.Impact)
                .FirstOrDefault(t => t.TicketId == id);

            model.FillLists(db);

            if (ticket != null)
            {
                model = Utility.ConvertFromTicketToViewModel(ticket);

                if (ticket.ApprovalStateId == db.ApprovalStates.First(a => a.Name == "Approved").ApprovalStateId)
                {
                    // TODO: Make "Ticket already approved" page
                    // ticket already approved
                    return View(model);
                }
                else
                {
                    return View(model);
                }
            }
            return View();
        }

        // APPROVE TICKET POST
        [HttpPost]
        [Route("Tickets/ApproveTicket/{id}")]
        public IActionResult ApproveTicket(int id, TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                // convert to Ticket
                Ticket ticket = Utility.ConvertFromTicketViewModelToTicket(db, model);
                ticket.TicketId = id;

                db.Update(ticket);
                db.SaveChanges();

                if (model.ApprovalState == "Approved")
                {
                    EmailConfiguration.SendEmailToKACE(ticket);
                }
                if (model.ApprovalState == "Rejected")
                {
                    EmailConfiguration.SendRejectionNotice(ticket);
                }

                return RedirectToAction("Tickets");
            }

            model.FillLists(db);

            return View(model);
        }

        #endregion
        // -- END TICKETS -- //

        // --- TICKET SUB TYPES --- //
        #region TicketSubTypes

        // CREATE TICKET SUBTYPE GET
        [HttpGet]
        [Route("Tickets/AddTicketSubType")]
        public IActionResult AddTicketSubType()
        {
            TicketSubTypeViewModel model = new();
            model.CategoriesList = db.Categories;
            return View(model);
        }

        // CREATE TICKET SUBTYPE POST
        [HttpPost]
        [Route("Tickets/AddTicketSubType")]
        public IActionResult AddTicketSubType(TicketSubTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                TicketSubType ticketSubType = Utility.ConvertFromSubTypeViewModelToSubType(db, model);

                db.Add(ticketSubType);
                db.SaveChanges();

                return RedirectToAction("TicketSubTypes", "Tickets");
            }
            return View();
        }

        // AJAX READ TICKET SUBTYPE(s)
        [HttpGet]
        [Route("Tickets/GetTicketSubTypes/{id}")]
        public List<string> GetTicketSubTypes(string? id)
        {
            List<string> myStrings = new();

            int categoryId = db.Categories.First(c => c.Name == id).CategoryId;

            foreach (TicketSubType subType in
                db.TicketSubTypes
                .Where(ts => ts.CategoryId == categoryId))
            {
                myStrings.Add(subType.Name);
            }

            return myStrings;
        }

        // AJAX READ TICKET SUBTYPE APPROVAL NEED
        [HttpGet]
        [Route("Tickets/GetTicketSubTypeApproval/{category}/{subcategory}")]
        public string GetTicketSubTypeApproval(string? category, string? subcategory)
        {
            return db.TicketSubTypes.First(st => st.Category.Name == category && st.Name == subcategory).NeedsApproval;
        }

        // READ TICKET SUBTYPE(s)
        [HttpGet]
        [Route("Tickets/TicketSubTypes/{id?}")]
        public IActionResult TicketSubTypes(int? id)
        {
            IEnumerable<TicketSubType> model;

            if (id is not null)
            {
                model = db.TicketSubTypes
                    .Where(s => s.TicketSubTypeId == id)
                    .Include(s => s.Category);
                return View(model);
            }
            model = db.TicketSubTypes
                .Include(s => s.Category);
            return View(model);
        }

        // UPDATE TICKET SUBTYPE GET
        [HttpGet]
        [Route("Tickets/UpdateTicketSubType/{id}")]
        public IActionResult UpdateTicketSubType(int id)
        {
            TicketSubType subType = db.TicketSubTypes
                .Include(s => s.Category)
                .First(s => s.TicketSubTypeId == id);

            TicketSubTypeViewModel model = Utility.ConvertFromSubTypeToViewModel(subType);

            model.FillLists(db);

            return View(model);
        }

        // UPDATE TICKET SUBTYPE POST
        [HttpPost]
        [Route("Tickets/UpdateTicketSubType/{id}")]
        public IActionResult UpdateTicketSubType(int id, TicketSubTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                TicketSubType subType = Utility.ConvertFromSubTypeViewModelToSubType(db, model);
                //subType.Name = model.Name;
                //subType.TicketSubTypeId = id;
                //subType.NeedsApproval = model.NeedsApproval;
                //subType.Category = db.Categories.First(t => t.Name == model.Category);
                //subType.Cclist = model.Cclist;
                //subType.Description = model.Description;

                db.Update(subType);
                db.SaveChanges();
                return RedirectToAction("TicketSubTypes", "Tickets");
            }
            return View();
        }

        // DELETE TICKET SUBTYPE GET
        [HttpGet]
        [Route("Tickets/DelTicketSubType/{id}")]
        public IActionResult DelTicketSubType(int id)
        {
            TicketSubType subType = db.TicketSubTypes
                .First(s => s.TicketSubTypeId == id);

            return View(subType);
        }

        // DELETE TICKET SUBTYPE POST
        [HttpPost]
        [Route("Tickets/DelTicketSubType/{id}")]
        public IActionResult DelTicketSubType(TicketSubType subType)
        {
            db.Remove(subType);
            db.SaveChanges();
            return RedirectToAction("TicketSubTypes", "Tickets");
        }
        #endregion
        // -- END TICKET SUB TYPES -- //

        // --- TICKET CATEGORIES --- //
        #region Categories

        // CREATE TICKET CATEGORY GET
        [HttpGet]
        [Route("Tickets/AddCategory")]
        public IActionResult AddCategory()
        {
            return View();
        }

        // CREATE TICKET CATEGORY POST
        [HttpPost]
        [Route("Tickets/AddCategory")]
        public IActionResult AddCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category Category = new();
                Category.Name = model.Name;

                db.Add(Category);
                db.SaveChanges();

                return RedirectToAction("Categories", "Tickets");
            }
            return View();
        }

        // READ TICKET CATEGORY(s)
        [HttpGet]
        [Route("Tickets/Categories/{id?}")]
        public IActionResult Categories(int? id)
        {
            return View(db.Categories.Include(t => t.TicketSubTypes));
        }

        // UPDATE TICKET CATEGORY GET
        [HttpGet]
        [Route("Tickets/UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id)
        {
            Category Category = db.Categories
                .Include(t => t.TicketSubTypes)
                .First(t => t.CategoryId == id);

            CategoryViewModel model = new CategoryViewModel();
            model.Name = Category.Name;

            return View(model);
        }

        // UPDATE TICKET CATEGORY POST
        [HttpPost]
        [Route("Tickets/UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id, CategoryViewModel model)
        {
            Category Category = new();
            Category.CategoryId = id;
            Category.Name = model.Name;
            //Category.Processes = 
            if (ModelState.IsValid)
            {
                db.Update(Category);
                db.SaveChanges();
                return RedirectToAction("Categories", "Tickets");
            }
            return View();
        }

        // DELETE TICKET CATEGORY GET
        [HttpGet]
        [Route("Tickets/DelCategory/{id}")]
        public IActionResult DelCategory(int id)
        {
            Category Category =
                db.Categories
                .Include(t => t.TicketSubTypes)
                .First(t => t.CategoryId == id);

            return View(Category);
        }

        // DELETE TICKET CATEGORY POST
        [HttpPost]
        [Route("Tickets/DelCategory/{id}")]
        public IActionResult DelTicketType(Category Category)
        {
            try
            {
                db.Remove(Category);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("", "Tickets");
            }
            return RedirectToAction("Categories", "Tickets");
        }
        #endregion
        // -- END TICKET CATEGORIES -- //

        // -- EMAIL REMINDERS -- //
        #region EmailReminders
        [HttpGet]
        [AllowAnonymous]
        [Route("Tickets/SendEmailReminders")]
        public void SendEmailReminders()
        {
            foreach (Ticket ticket in
                db.Tickets
                .Include(t => t.SubType)
                .Include(t => t.Impact)
                .Include(t => t.ApprovalState)
                .Include(t => t.Category)
                .Where(t => t.ApprovalState.Name == "Needs Approval"))
            {
                EmailConfiguration.SendApprovalRequestToManager(ticket);
            }
        }
        #endregion
        // -- END EMAIL REMINDERS -- //

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}