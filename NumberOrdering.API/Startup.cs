using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NumberOrdering.API.Models;
using NumberOrdering.Domain;
using NumberOrdering.Infrastructure;
using Swashbuckle.AspNetCore.Filters;

namespace NumberOrdering.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<INumbersFileRepository, NumbersFileRepository>();
            services.AddSingleton<INumbersService, NumbersService>();

            AddSwagger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Number Ordering API V1");
                c.RoutePrefix = "api/documentation";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Number Ordering API",
                        Version = "v1",
                        Description = "HTTP service for ordering numbers"
                    });

                var dir = new DirectoryInfo(AppContext.BaseDirectory);
                foreach (var fi in dir.EnumerateFiles("*.xml"))
                {
                    options.IncludeXmlComments(fi.FullName);
                }

                options.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblyOf<PostNumbersRequestExample>();
        }
    }
}
