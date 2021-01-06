using FinanceTracker.RequestHandlers;
using FinanceTracker.Services;
using FinanceTracker.Services.Implementations;
using FinanceTracker.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinanceTracker
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            // {
            //     builder.WithOrigins("https://localhost:5001").AllowAnyMethod().AllowAnyHeader();
            // }));
            //
            // services.AddControllers();
            services.AddScoped<IExpenseRequestHandler, ExpenseRequestHandler>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IExpenseDataService, ExpenseDataService>();
            services.AddScoped<IImportService, ImportService>();

            services.AddGrpc();
            // services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            // {
            //     builder.AllowAnyOrigin()
            //         .AllowAnyMethod()
            //         .AllowAnyHeader()
            //         .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            // }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseCors(builder => builder
            //     .AllowAnyOrigin()
            //     .AllowAnyMethod()
            //     .AllowAnyHeader());
            //
            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<FinanceTrackerImplementation>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
