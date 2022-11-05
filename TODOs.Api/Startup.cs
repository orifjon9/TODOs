using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TODOs.Api.Exceptions;
using TODOs.Api.Repositories;
using TODOs.Api.Repositories.Contracts;
using TODOs.Data;

namespace TODOs.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (httpContext, ex) =>
                {
                    var env = httpContext.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment();
                };

                options.Map<NotFoundException>(ex => new ProblemDetails
                {
                    Title = ex.Message,
                    Status = StatusCodes.Status404NotFound
                });
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
                options.OnBeforeWriteDetails = (httpContext, problemDetails) =>
                {
                    var logger = httpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                    var logLevel = (problemDetails.Status ?? 500) >= StatusCodes.Status500InternalServerError ? LogLevel.Error : LogLevel.Warning;
                    logger.Log(logLevel, problemDetails.Title);
                    logger.Log(logLevel, problemDetails.Detail);
                };
                
            });
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            }).AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TODOs.Api", Version = "v1" });
            });

            services.AddDbContext<TodoDbContext>(op => op.UseSqlServer(Configuration.GetConnectionString("MSSQL")));
            services.AddScoped<ITodoRepository, TodoRepository>()
               .AddScoped<IListRepository, ListRepository>();
            services.AddSingleton(provider => Mappings.AutoMapperConfig.Create(provider));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            app.UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TODOs.Api v1"));

            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}

