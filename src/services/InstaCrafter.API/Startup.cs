using System.Reflection;
using AutoMapper;
using InstaCrafter.API.Extensions;
using InstaCrafter.Infrastructure.Extensions;
using InstaCrafter.Infrastructure.Identity.Models.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace InstaCrafter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Token>(Configuration.GetSection("token"));
            services.ConfigureAuthentication(Configuration);
            services.ConfigureAuthorization();
            services.ConfigureIdentityDatabase(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication.API", Version = "v1" });
            });
            services.AddHttpContextAccessor();
            services.ConfigureMediatr();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.EnsureIdentityDbIsCreated();
                app.SeedIdentityDataAsync().Wait();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication.API v1"));
            }

            app.UseCors(options
                => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}