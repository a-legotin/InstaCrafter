using System;
using System.Net;
using System.Reflection;
using System.Text;
using InstaCrafter.Identity.Auth;
using InstaCrafter.Identity.Data;
using InstaCrafter.Identity.Extensions;
using InstaCrafter.Identity.Helpers;
using InstaCrafter.Identity.Models;
using InstaCrafter.Identity.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace InstaCrafter.Identity
{
    public class Startup
    {
        private const string SecretKey = "aMivDmHLpUA777sqsfhqGbKRdRj1PVkH";
        private readonly SymmetricSecurityKey _signingKey = new(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("main-db"));

            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("dhgdfhdygh5346t3tfwfsdfsdsf"));  
            var tokenValidationParameters = new TokenValidationParameters  
            {  
                ValidateIssuerSigningKey = true,  
                IssuerSigningKey = signingKey,  
                ValidateIssuer = true,  
                ValidIssuer = jwtAppSettingOptions["Issuer"],  
                ValidateAudience = true,  
                ValidAudience = jwtAppSettingOptions["Audience"],  
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

            services.AddAuthorization();

            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 4;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "InstaCrafter.Identity", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InstaCrafter.Identity v1"));
            }

            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
                });

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}