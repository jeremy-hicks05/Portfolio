
using HealthCheck.API;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MTAIntranetAngular.API.Data.GraphQL;
using System.Net;
using System.Text.Json.Serialization;

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
                .AddCheck("EAM Test", new IISHealthCheck("http://mtatrapezetest", "eam"))
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

            //builder.Services.AddCors(options =>
            //options.AddPolicy(name: "AngularPolicy",
            //cfg =>
            //{
            //    cfg.AllowAnyHeader();
            //    cfg.AllowAnyMethod();
            //    cfg.AllowAnyOrigin();
            //    //cfg.WithOrigins(builder.Configuration["AllowedCORS"]);
            //}));

            // SignalR
            builder.Services.AddSignalR();

            builder.Services.AddDbContext<MtaticketsContext>(options =>
                options.UseSqlServer(MTADevConnection));
            //,ServiceLifetime.Transient);

            builder.Services.AddGraphQLServer()
                //.AddAuthorization()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddFiltering()
                .AddSorting();

            var app = builder.Build();

            // ADDED
            //FileExtensionContentTypeProvider provider =
            //    new FileExtensionContentTypeProvider();
            //provider.Mappings[".webmanifest"] = "application/majifest+json";

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    ContentTypeProvider = provider
            //});
            // ADDED

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.MapGet("/Error", () => Results.Problem());
            }

            app.UseCors(options =>
                options
                .WithOrigins("https://localhost:4200",
                "https://mtadev.mta-flint.net:8443/",
                "https://mtadev.mta-flint.net/"
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

            app.MapHub<HealthCheckHub>("/api/health-hub");

            app.MapGet("/api/broadcast/update2", async (IHubContext<HealthCheckHub> hub) =>
            {
                await hub.Clients.All.SendAsync("Update", "test");
                return Results.Text("Update message sent.");
            });

            app.UseAuthorization();

            // ADDED 
            //app.UseCors("AngularPolicy");

            app.MapControllers();

            app.MapGraphQL("/api/graphql");

            app.MapMethods("/api/heartbeat", new[] { "HEAD" },
                () => Results.Ok());

            app.Run();
        }
    }
}