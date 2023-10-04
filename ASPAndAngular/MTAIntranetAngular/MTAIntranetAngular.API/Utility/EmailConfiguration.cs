using MTAIntranetAngular.API;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.DirectoryServices;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace MTAIntranetAngular.Utility
{
    public static class EmailConfiguration
    {
        public static string? From { get; set; }
        public static string? SmtpServer { get; set; }
        public static int Port { get; set; }
        public static string? UserName { get; set; }
        public static string? Password { get; set; }

        public static void SendApprovalRequestToManager(Ticket ticket)
        {
            string recipients = "";
            recipients += "jhicks@mtaflint.org"; // REMOVE IF NOT TESTING
            //recipients += GetManagerEmailFromDisplayName(ticket.EnteredByUser[10..]);

            List<string> recipientsArray = recipients.Split(',').ToList();
            recipientsArray = recipientsArray.Distinct().ToList();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            for (int i = 0; i < recipientsArray.Count; i++)
            {
                SmtpClient MyMail = new SmtpClient();
                MailMessage MyMsg = new MailMessage();
                MyMail.Host = "smtp.office365.com";
                MyMail.Port = 587;
                MyMail.EnableSsl = true;
                MyMsg.Priority = MailPriority.Normal;
                MyMsg.To.Add(new MailAddress(recipientsArray[i].Trim('\'')));
                MyMsg.Subject = "Ticket Approval Request: " + ticket.SubType.Name + " " + ticket.Category.Name + " ticket from Intranet: " + ticket.DateEntered.ToString("MM/dd/yyyy hh:mm tt");
                MyMsg.SubjectEncoding = Encoding.UTF8;
                MyMsg.IsBodyHtml = true;
                MyMsg.From = SetMailAddress();
                MyMsg.BodyEncoding = Encoding.UTF8;
                MyMsg.Body = ticket.Summary + "<br />" +
                    //"https://mtadev.mta-flint.net/Tickets/" + ticket.TicketId + "<br />" +
                    @"https://https://mtadev.mta-flint.net:8443/mtaIntranet#/ticket/" + ticket.TicketId + "<br />" +
                    "Impact: " + ticket.Impact.Description + "<br />" +
                    "Approval State: " + ticket.ApprovalState.Name + "<br />" +
                    "Entered by User: " + ticket.EnteredByUser + "<br />" +
                    "Ticket Type: " + ticket.Category.Name + "<br />" +
                    "Ticket SubType: " + ticket.SubType.Name + "<br />";

                //MyMsg.Body += "<a href=https://mtadev.mta-flint.net/Tickets/ApproveTicket/" + ticket.TicketId + ">Approve / Reject Ticket</a>";
                MyMsg.Body += @"<a href=https://mtadev.mta-flint.net:8443/mtaIntranet#/ticket/" + ticket.TicketId + ">View Ticket</a>";

                MyMail.UseDefaultCredentials = false;
                NetworkCredential MyCredentials = SetCredentials();
                MyMail.Credentials = MyCredentials;
                MyMail.Send(MyMsg);
            }
        }

        public static void SendTicketInfoTo(Ticket ticket)
        {
            string recipients = "";
            recipients += "jhicks@mtaflint.org"; // REMOVE IF NOT TESTING
            //recipients += ticket.SubType.Cclist;

            if (ticket.ImpactId == 2 || ticket.ImpactId == 3)
            {
                //recipients += "," + GetManagerEmailFromDisplayName(ticket.EnteredByUser[10..]);
            }

            List<string> recipientsArray = recipients.Split(',').ToList();
            recipientsArray = recipientsArray.Distinct().ToList();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            for (int i = 0; i < recipientsArray.Count; i++)
            {
                SmtpClient MyMail = new SmtpClient();
                MailMessage MyMsg = new MailMessage();
                MyMail.Host = "smtp.office365.com";
                MyMail.Port = 587;
                MyMail.EnableSsl = true;
                MyMsg.Priority = MailPriority.Normal;
                MyMsg.To.Add(new MailAddress(recipientsArray[i].Trim('\'')));
                MyMsg.Subject = "Notice: " + ticket.SubType.Name + " " + ticket.Category.Name + " ticket from Intranet: " + ticket.DateEntered.ToString("MM/dd/yyyy hh:mm tt");
                MyMsg.SubjectEncoding = Encoding.UTF8;
                MyMsg.IsBodyHtml = true;
                MyMsg.From = SetMailAddress();
                MyMsg.BodyEncoding = Encoding.UTF8;
                MyMsg.Body = ticket.Summary + "<br />" +
                    //"https://mtadev.mta-flint.net/Tickets/" + ticket.TicketId + "<br />" +
                    @"https://mtadev.mta-flint.net:8443/mtaIntranet#/ticket/" + ticket.TicketId + "<br />" +
                    "Impact: " + ticket.Impact.Description + "<br />" +
                    "Approval State: " + ticket.ApprovalState.Name + "<br />" +
                    "Entered by User: " + ticket.EnteredByUser + "<br />" +
                    "Ticket Type: " + ticket.Category.Name + "<br />" +
                    "Ticket SubType: " + ticket.SubType.Name + "<br />";

                MyMail.UseDefaultCredentials = false;
                NetworkCredential MyCredentials = SetCredentials();
                MyMail.Credentials = MyCredentials;
                MyMail.Send(MyMsg);
            }
        }
        private static string GetManagerEmailFromDisplayName(string userName)
        {
            string managerEmail = "";

            string domainName = "MTA-FLINT.NET";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using (var de = new DirectoryEntry("LDAP://" + domainName))
                {
                    using (var adSearch = new DirectorySearcher(de))
                    {
                        // Get user from active directory.
                        adSearch.Filter = "(sAMAccountName=" + userName.Trim().ToLower(CultureInfo.CurrentCulture) + ")";
                        adSearch.PropertiesToLoad.Add("manager");
                        var adSearchResult = adSearch.FindOne();
                        if (adSearchResult == null)
                        {
                            return userName + " not found - found ";
                        }
                        else
                        {
                            managerEmail = GetUsersManagersEmail(adSearchResult.Properties["manager"][0]!.ToString()!.Split(',')[0].Remove(0, 3)); ;
                        }
                    }
                }
            }
            return managerEmail;
        }

        public static string GetUsersManagersEmail(string userDisplayName)
        {
            string response = "";
            string domainName = "MTA-FLINT.NET";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using (var de = new DirectoryEntry("LDAP://" + domainName))
                {
                    using (var adSearch = new DirectorySearcher(de))
                    {
                        // Get user from active directory.
                        adSearch.Filter = "(displayName=" + userDisplayName.Trim().ToLower(CultureInfo.CurrentCulture) + ")";
                        var adSearchResult = adSearch.FindOne();
                        if (adSearchResult == null)
                        {
                            return userDisplayName + " not found - found ";
                        }
                        else
                        {
                            var entry = adSearchResult.GetDirectoryEntry();

                            response += entry.Properties["mail"][0];
                        }
                    }
                }
            }
            return response;
        }

        public static void SendEmailToKACE(Ticket ticket)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("helpdesk@mtaflint.org");
            MyMsg.Subject = ticket.SubType.Name + " " + ticket.Category.Name + " ticket from Intranet: " + ticket.DateEntered.ToString("MM/dd/yyyy hh:mm tt");
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body =
                "@category=" + ticket.Category.Name + " <br />" +
                "@cc_list=" + ticket.SubType.Cclist + " <br />" +
                "@impact=" + ticket.Impact.Description + " <br />" +
                "@submitter=" + ((ticket.ApprovedBy == "NA" || ticket.ApprovedBy == null) ? ticket.EnteredByUser[10..] : ticket.ApprovedBy[10..]) + " <br />" +
                ticket.Summary.Trim() + " <br />";
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendRejectionNotice(Ticket ticket)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");  // TESTING
            //MyMsg.To.Add(ticket.EnteredByUser[10..] + "@mtaflint.org");
            //MyMsg.To.Add(
            //    (ticket.ApprovedBy == "NA" ||
            //    ticket.ApprovedBy == null)
            //    ? ticket.EnteredByUser[10..]
            //    : ticket.ApprovedBy[10..] + "@mtaflint.org");
            //foreach (string email in ticket.SubType.Cclist.Split(","))
            //{
            //    MyMsg.To.Add(email);
            //}
            MyMsg.Subject = "Ticket " + ticket.ApprovalState.Name + ": " + ticket.SubType.Name + " " + ticket.Category.Name + " ticket from Intranet: " + ticket.DateEntered.ToString("MM/dd/yyyy hh:mm tt");
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body =
                //"https://mtadev.mta-flint.net/Tickets/" + ticket.TicketId + "<br />" +
                @"https://mtadev.mta-flint.net:8443/mtaIntranet#/ticket/" + ticket.TicketId + "<br />" +
                "Category: " + ticket.Category.Name + " <br />" +
                "Impact: " + ticket.Impact.Description + " <br />" +
                ticket.ApprovalState.Name == "Approved" ? 
                "Approved "
                : "Reason for rejection: " + ticket.ReasonForRejection;
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        private static NetworkCredential SetCredentials()
        {
            return new NetworkCredential("intranet@mtaflint.org", "$mtainet23!");
        }

        private static MailAddress SetMailAddress()
        {
            return new MailAddress("intranet@mtaflint.org", "intranet@mtaflint.org");
        }
    }
}
