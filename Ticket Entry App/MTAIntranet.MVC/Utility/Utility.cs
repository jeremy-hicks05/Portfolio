

namespace MTAIntranet.MVC.Utility
{
    using System.DirectoryServices;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using MTAIntranet.SQL.Entities;
    using MTAIntranet.SQL.API;
    using MTAIntranet.MVC.Models.ViewModels;

    public static class Utility
    {
        public static string GetManagerEmailFromDisplayName(HttpContext context)
        {
            string managerEmail = "";

            string domainName = "MTA-FLINT.NET";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string httpContextUserName = context.User!.Identity!.Name!;

                string userName = httpContextUserName.Substring(10);

                using (DirectoryEntry de = new DirectoryEntry("LDAP://" + domainName))
                {
                    using (DirectorySearcher adSearch = new DirectorySearcher(de))
                    {
                        // Get user from active directory.
                        adSearch.Filter = "(sAMAccountName=" + userName.Trim().ToLower(CultureInfo.CurrentCulture) + ")";
                        adSearch.PropertiesToLoad.Add("manager");
                        SearchResult? adSearchResult = adSearch.FindOne();
                        if (adSearchResult == null)
                        {
                            return userName + " not found - found ";
                        }
                        else
                        {
                            managerEmail = GetUsersManagersEmail(adSearchResult); ;
                        }
                    }
                }
            }
            return managerEmail;
        }

        public static string GetUsersManagersEmail(SearchResult searchResult)
        {
            string response = "";
            string domainName = "MTA-FLINT.NET";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {

                string userDisplayName = searchResult.Properties["manager"][0]!.ToString()!.Split(',')[0].Remove(0, 3);

                using (DirectoryEntry de = new DirectoryEntry("LDAP://" + domainName))
                {
                    using (DirectorySearcher adSearch = new DirectorySearcher(de))
                    {
                        // Get user from active directory.
                        adSearch.Filter = "(displayName=" + userDisplayName.Trim().ToLower(CultureInfo.CurrentCulture) + ")";
                        SearchResult? adSearchResult = adSearch.FindOne();
                        if (adSearchResult == null)
                        {
                            return userDisplayName + " not found";
                        }
                        else
                        {
                            DirectoryEntry entry = adSearchResult.GetDirectoryEntry();

                            response += entry.Properties["mail"][0];
                        }
                    }
                }
            }
            return response;
        }

        public static Ticket ConvertFromTicketViewModelToTicket(MtaticketsContext context, TicketViewModel model)
        {
            Ticket ticket = new()
            {
                ApprovalState = context.ApprovalStates.First(a => a.Name == model.ApprovalState),
                ApprovedBy = model.ApprovedBy,
                DateLastUpdated = model.DateLastUpdated,
                DateEntered = model.DateEntered,
                EnteredByUser = model.EnteredByUser,
                Category = context.Categories.First(c => c.Name == model.Category),
                Impact = context.Impacts.First(i => i.Description == model.Impact),
                SubType = context.TicketSubTypes.First(s => s.Name == model.SubType),
                ReasonForRejection = model.ReasonForRejection,
                Summary = model.Summary
            };

            return ticket;
        }

        public static TicketViewModel ConvertFromTicketToViewModel(Ticket ticket)
        {
            TicketViewModel model = new()
            {
                ApprovalState = ticket.ApprovalState.Name,
                ApprovedBy = ticket.ApprovedBy ?? "NA",
                Summary = ticket.Summary,
                Category = ticket.Category.Name,
                DateEntered = ticket.DateEntered,
                DateLastUpdated = ticket.DateLastUpdated,
                EnteredByUser = ticket.EnteredByUser,
                Impact = ticket.Impact.Description,
                SubType = ticket.SubType.Name,
                ReasonForRejection = ticket.ReasonForRejection
            };

            return model;
        }

        public static TicketSubType ConvertFromSubTypeViewModelToSubType(MtaticketsContext context, TicketSubTypeViewModel model)
        {
            TicketSubType subType = new()
            {
                NeedsApproval = model.NeedsApproval,
                Category = context.Categories.First(c => c.Name == model.Category),
                Description = model.Description,
                Cclist = model.Cclist,
                Name = model.Name
            };

            return subType;
        }

        public static TicketSubTypeViewModel ConvertFromSubTypeToViewModel(TicketSubType subType)
        {
            TicketSubTypeViewModel model = new()
            {
                Category = subType.Category.Name,
                Description = subType.Description,
                Cclist = subType.Cclist,
                Name = subType.Name,
                NeedsApproval = subType.NeedsApproval
            };

            return model;
        }
    }
}
