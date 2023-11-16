using Microsoft.Extensions.Diagnostics.HealthChecks;
using MTAIntranetAngular.Utility;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Policy;
using static HotChocolate.ErrorCodes;

namespace HealthCheck.API
{
    public class ProcessHealthCheck : IHealthCheck
    {
        private readonly string Host;
        private readonly string ProcessName;

        public ProcessHealthCheck(string processName, string host)
        {
            this.Host = host;
            this.ProcessName = processName;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
        {
            // this works, because the protocol is included in the string
            try
            {
                //using var ping = new HttpClient();
                //ping.BaseAddress = new Uri("mtatrapezeprod");
                //var reply = await ping.GetAsync(Host);

                Process[] ipByName = Process.GetProcessesByName(ProcessName, Host);
                switch (ipByName.Length > 0)
                {
                    case true:
                        var msg =
                            "Success";
                        return HealthCheckResult.Healthy(msg);
                    default:
                        var err =
                            $"Process {ProcessName} not running on {Host}";

                        // Send Email Error about process
                        EmailConfiguration.SendProcessFailure(ProcessName, Host);

                        return HealthCheckResult.Unhealthy(err);
                }
            }
            catch (Exception e)
            {
                var err =
                    $"Process {ProcessName} not running on {Host}";
                // Send Email Error about process
                EmailConfiguration.SendProcessFailure(ProcessName, Host);

                //ProcessStartInfo info = new ProcessStartInfo("C:\\PsTools");
                //info.FileName = @"C:\PsTools\psexec.exe";
                //info.Arguments = @"\\" + Host + @" -i C:\WINDOWS\notepad.exe";
                //info.RedirectStandardOutput = true;
                //info.UseShellExecute = false;
                //Process p = Process.Start(info);

                return HealthCheckResult.Unhealthy(err);
            }
        }
    }
}
