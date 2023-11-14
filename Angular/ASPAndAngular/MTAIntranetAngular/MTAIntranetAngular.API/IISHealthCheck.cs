using Microsoft.Extensions.Diagnostics.HealthChecks;
using MTAIntranetAngular.Utility;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Policy;
using static HotChocolate.ErrorCodes;

namespace HealthCheck.API
{
    public class IISHealthCheck : IHealthCheck
    {
        private readonly string Host;
        private readonly string Suffix;

        public IISHealthCheck(string baseUrl, string suffix)
        {
            this.Host = baseUrl;
            this.Suffix = suffix;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
        {

            //Process firstProcess = new();
            //ProcessStartInfo firstStartInfo = new()
            //{
            //    WindowStyle = ProcessWindowStyle.Hidden,
            //    FileName = "cmd.exe",
            //    Arguments = $"/k appcmd start site \"Default Web Site\" \\\\192.168.122.60",
            //    UseShellExecute = true,
            //    Verb = "runas",
            //    WorkingDirectory = "%windir%\\syswow64\\inetsrv"
            //};
            //firstProcess.StartInfo = firstStartInfo;
            //firstProcess.Start();

            // this works, because the protocol is included in the string
            Uri serverUri = new Uri(Host);

            // needs UriKind arg, or UriFormatException is thrown
            Uri relativeUri = new Uri(Suffix, UriKind.Relative);

            // Uri(Uri, Uri) is the preferred constructor in this case
            Uri fullUri = new Uri(serverUri, relativeUri);
            try
            {
                using var ping = new HttpClient();
                //ping.BaseAddress = new Uri("mtatrapezeprod");
                var reply = await ping.GetAsync(fullUri);
                switch (reply.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var msg =
                            "Success";
                        return HealthCheckResult.Healthy(msg);
                    default:
                        var err =
                            $"ICMP to {fullUri} failed";
                        EmailConfiguration.SendServerFailure(Host);

                        //Process.Start("cmd.exe", "SHUTDOWN /r /f /m \\" + "192.168.122.60");

                        //Process process = new();
                        //ProcessStartInfo startInfo = new()
                        //{
                        //    WindowStyle = ProcessWindowStyle.Hidden,
                        //    FileName = "cmd.exe",
                        //    Arguments = $"/k SHUTDOWN /r /f /m \\\\192.168.122.60",
                        //    UseShellExecute = true,
                        //    Verb = "runas"
                        //};
                        //process.StartInfo = startInfo;
                        //process.Start();

                        return HealthCheckResult.Unhealthy(err);
                }
            }
            catch (Exception e)
            {
                var err =
                    $"ICMP to {fullUri} failed {e.Message}";
                EmailConfiguration.SendServerFailure(Host);

                //Process.Start("cmd.exe", "SHUTDOWN /r /f /m \\" + "192.168.122.60");

                //Process process = new();
                //ProcessStartInfo startInfo = new()
                //{
                //    WindowStyle = ProcessWindowStyle.Hidden,
                //    FileName = "cmd.exe",
                //    Arguments = $"/k SHUTDOWN /r /f /m \\\\192.168.122.60",
                //    UseShellExecute = true,
                //    Verb = "runas"
                //};
                //process.StartInfo = startInfo;
                //process.Start();

                return HealthCheckResult.Unhealthy(err);
            }
        }
    }
}
