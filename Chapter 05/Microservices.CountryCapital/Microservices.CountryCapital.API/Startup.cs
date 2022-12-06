using Microservices.CountryCapital.Domain;
using Microservices.CountryCapital.Infrastructure;
using Microservices.CountryCapital.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.CountryCapital.API
{
    public class Startup
    {
        private bool IsLocalEnvironment = false;

        public Startup(IHostingEnvironment env,IConfiguration configuration)
        {
            Configuration = configuration;
            IsLocalEnvironment = env.IsEnvironment("Local");
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IRepository,FileRepository>();
            //services.AddScoped<IRepository, S3Repository>();
            services.AddScoped<ICountryCapitalService, CountryCapitalService>();
            services.AddScoped<IValidateCountryDomain, ValidateCountryDomain>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (IsLocalEnvironment)
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CountryCapitalService V1");
                else
                    c.SwaggerEndpoint("/countrycapital/swagger/v1/swagger.json", "CountryCapitalService V1");

                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
        }
    }
}
