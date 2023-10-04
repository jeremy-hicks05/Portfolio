
using Microsoft.EntityFrameworkCore;
using HealthCheck.API;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;

namespace MTAIntranetAngular.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var MTADevConnection = builder.Configuration
                .GetConnectionString("MTADevConnection");

            // Add services to the container.

            builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });

            builder.Services.AddHealthChecks()
                .AddCheck("MTADev", new ICMPHealthCheck("192.168.122.69", 100))
                .AddCheck("FLTAS003", new ICMPHealthCheck("FLTAS003", 100))
                .AddCheck("FLTASTS", new ICMPHealthCheck("FLTASTS", 100))
                .AddCheck("Sched Srv", new ICMPHealthCheck("FLTAS015", 100))
                .AddCheck("SendEmailRemindersAngular", new ServiceCheck("SendEmailRemindersAngular", "mtadev"))
                //.AddCheck("EMailReminderService", new ServiceCheck("MyFirstService.Demo", "mtadev"))
                //.AddCheck("EAM Max Queue Production", new ServiceCheck("EAM_MAXQ_52120", "192.168.122.70"))
                .AddCheck("EAM Max Queue Test", new ServiceCheck("EAM_MAXQ_52120", "mtatrapezetest"));

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MtaticketsContext>(options =>
                options.UseSqlServer(MTADevConnection),
                ServiceLifetime.Transient);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
                options
                .WithOrigins("https://localhost:4200"
                //"https://mtadev.mta-flint.net/",
                //"https://mtadev.mta-flint.net:50443/"
                )
                //.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(host => true)
                );

            app.UseHttpsRedirection();

            app.UseHealthChecks(new PathString("/api/health"),
                new CustomHealthCheckOptions());

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}