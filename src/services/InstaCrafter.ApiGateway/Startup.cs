using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace InstaCrafter.ApiGateway
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
            var audienceConfig = Configuration.GetSection("JwtIssuerOptions");  
  
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("dhgdfhdygh5346t3tfwfsdfsdsf"));  
            var tokenValidationParameters = new TokenValidationParameters  
            {  
                ValidateIssuerSigningKey = true,  
                IssuerSigningKey = signingKey,  
                ValidateIssuer = true,  
                ValidIssuer = audienceConfig["Issuer"],  
                ValidateAudience = true,  
                ValidAudience = audienceConfig["Audience"],  
                ValidateLifetime = true,  
                ClockSkew = TimeSpan.Zero,  
                RequireExpirationTime = true,  
            };  
  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })                .AddJwtBearer("TestKey", x =>  
                {  
                    x.RequireHttpsMetadata = false;  
                    x.TokenValidationParameters = tokenValidationParameters;  
                }); 
            services.AddOcelot();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOcelot().Wait();
        }
    }
}