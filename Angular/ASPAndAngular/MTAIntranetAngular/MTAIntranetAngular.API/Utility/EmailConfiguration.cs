using System.Net.Mail;
using System.Net;
using System.Text;
using System.DirectoryServices;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using MTAIntranetAngular.API.Data.Models;

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
                    @"https://mtadev.mta-flint.net:8443/mtaIntranet#/ticket/" + ticket.TicketId + "<br />" +
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
            MyMsg.Subject = "Ticket " + ticket.ApprovalState.Name + ": " + 
                ticket.SubType.Name + " " + ticket.Category.Name + 
                " ticket from Intranet: " + 
                ticket.DateEntered.ToString("MM/dd/yyyy hh:mm tt");
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body =
                //"https://mtadev.mta-flint.net/Tickets/" + ticket.TicketId + "<br />" +
                @"https://mtadev.mta-flint.net:8443/mtaIntranet#/ticket/" + ticket.TicketId + "<br />" +
                "Category: " + ticket.Category.Name + " <br />" +
                "Impact: " + ticket.Impact.Description + " <br />" +
                "SubType: " + ticket.SubType.Name + " <br />" +
                "Description: " + ticket.SubType.Description + " <br />" +
                (ticket.ApprovalState.Name == "Approved" ? 
                "Approved "
                : "Reason for rejection: " + ticket.ReasonForRejection);
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        // servers, services, processes, and website health checks
        public static void SendServerInitSuccess(string serverName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = serverName + " connected and monitoring...";
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Connection to " + serverName + " successful.";
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendServerFailure(string serverName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = serverName + " is not responding";
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Please resolve the problem with " + serverName;
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendServerRestored(string serverName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = serverName + " has been restored.";
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Please confirm that " + serverName + " has been restored.";
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendServiceInitSuccess(string serverName, string serviceName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = "Service " + serviceName + " on server " + serverName +
                " connected and monitoring...";
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Sevice " + serviceName + " on "
                + serverName + " connection successful.";
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }
        public static void SendServiceFailure(string serverName, string serviceName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = "Service " + serviceName + " on server " + serverName + 
                " is not responding.";
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Please restore sevice " + serviceName + " on " 
                + serverName;
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendServiceRestored(string serverName, string serviceName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = "Service " + serviceName + " on server " + serverName +
                " has been restored.";
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Please confirm sevice " + serviceName + " is running on "
                + serverName;
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendProcessInitSuccess(string serverName, string processName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = processName + " connected and monitoring on server " + serverName + "...";
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Process " + processName +
                " on server " + serverName + " connection successful.";
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendProcessFailure(string serverName, string processName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = processName + " is not running on server " + serverName;
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Please start process " + processName + 
                " on server " + serverName;
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendProcessRestored(string serverName, string processName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = processName + " has been restored on server " + serverName;
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Please confirm process " + processName +
                " is running on server " + serverName;
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendWebsiteInitSuccess(string serverName, string websiteName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = websiteName + " connected and monitoring on " + serverName + "...";
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Website " + websiteName +
                " on server " + serverName + " connection successful.";
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendWebsiteFailure(string serverName, string websiteName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = websiteName + " is not responding on server " + serverName;
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Please restore website " + websiteName + 
                " on server " + serverName;
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = SetCredentials();
            MyMail.Credentials = MyCredentials;
            MyMail.Send(MyMsg);
        }

        public static void SendWebsiteRestored(string serverName, string websiteName)
        {
            SmtpClient MyMail = new SmtpClient();
            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.office365.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;
            MyMsg.Priority = MailPriority.Normal;
            MyMsg.To.Add("jhicks@mtaflint.org");
            MyMsg.Subject = websiteName + " has been restored on server " + serverName;
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = SetMailAddress();
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = "Please confirm website " + websiteName +
                " has been restored on server " + serverName;
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
