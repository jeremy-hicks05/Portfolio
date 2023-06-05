using MTAIntranet.Shared;
using System.Net;
using System.Net.Mail;

namespace MTAIntranet.MVC.Utility
{
    public static class EmailConfiguration
    {
        public static string? From { get; set; }
        public static string? SmtpServer { get; set; }
        public static int MyPort { get; set; }
        public static string? UserName { get; set; }
        public static string? Password { get; set; }

        public static void SendEmail()
        {
            Console.WriteLine("Sending test email...");
            var smtpClient = new SmtpClient("outlook.office.com")
            {
                Port = MyPort,
                Credentials = new NetworkCredential(UserName, Password),
                EnableSsl = true
            };

            if (From is not null)
            {
                smtpClient.Send(From, "jhicks@mtaflint.org", "testSubject", "testBody");
            }
            else
            {
                // From is empty - error message
            }
        }
    }
}
