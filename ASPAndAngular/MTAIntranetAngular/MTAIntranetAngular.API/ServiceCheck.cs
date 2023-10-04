using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;
using System.ServiceProcess;
//using System.ServiceProcess;

namespace MTAIntranetAngular.API
{
    public class ServiceCheck : IHealthCheck
    {
        private readonly string ServiceName;
        private readonly string MachineName;
        public ServiceCheck(string serviceName, string machineName) 
        {
            this.ServiceName = serviceName;
            this.MachineName = machineName;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            ServiceController sc = new ServiceController(ServiceName, MachineName);

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    return HealthCheckResult.Healthy();
                case ServiceControllerStatus.Stopped:
                    sc.Start();
                    return HealthCheckResult.Unhealthy();
                case ServiceControllerStatus.Paused:
                    return HealthCheckResult.Unhealthy();
                case ServiceControllerStatus.StopPending:
                    return HealthCheckResult.Unhealthy();
                case ServiceControllerStatus.StartPending:
                    return HealthCheckResult.Unhealthy();
                default:
                    return HealthCheckResult.Degraded();
            }
        }
    }
}
