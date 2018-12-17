using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.ApplicationService.Services;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Infrastructure.Data;
using CrownCleanApp.Infrastructure.Data.Managers;
using CrownCleanApp.Infrastructure.Data.SQLRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CrownCleanApp.RestAPI
{
    public class Startup
    {
        private IConfiguration _conf { get; }

        private IHostingEnvironment _env { get; set; }

        // TODO: set proper values for configuration in appsettings.json

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _conf = builder.Build();
            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            if (_env.IsDevelopment()) {
                services.AddDbContext<CrownCleanAppContext>(
                    opt => opt.UseSqlite("Data Source=CrownCleanApp.db"));
                IdentityModelEventSource.ShowPII = true;
            }
            else if (_env.IsProduction()) {
                services.AddDbContext<CrownCleanAppContext>(
                    opt => opt
                        .UseSqlServer(_conf.GetConnectionString("defaultConnection")));
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = _conf["JwtIssuer"],
                    ValidAudience = _conf["JwtIssuer"],
                    ValidateIssuerSigningKey = true,
                    //ValidateIssuer = false,
                    //ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["JwtKey"])),
                    ClockSkew = TimeSpan.FromMinutes(2) 
                };
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper());

            services.AddCors();

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope()) {
                    var ctx = scope.ServiceProvider.GetService<CrownCleanAppContext>();
                    DBInitializer.SeedDB(ctx, new AuthenticationHelper());
                }
            }
            else {
                using (var scope = app.ApplicationServices.CreateScope()) {
                    var ctx = scope.ServiceProvider.GetService<CrownCleanAppContext>();
                    ctx.Database.EnsureCreated();
                }
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
