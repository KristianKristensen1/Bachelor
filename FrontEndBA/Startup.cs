using JwtAuthenticationHelper.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace FrontEndBA
{
    // Setup local environment using this guide https://neelbhatt.com/2018/02/04/enforce-ssl-and-use-hsts-in-net-core2-0-net-core-security-part-i/
    // Followed documentation https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.1&tabs=visual-studio
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // retrieve the configured token params and establish a TokenValidationParameters object,
            // we are going to need this later.


            var validationParams = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,

                ValidateAudience = true,
                ValidAudience = "Test", //Configuration["Token:Audience"],

                ValidateIssuer = true,
                ValidIssuer = "Test", //Configuration["Token:Issuer"],

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Test1111123323uq2hhsjajsjajhgfjhfksaoaodjdndjxsajanja")),   //Configuration["Token:SigningKey"])),
                ValidateIssuerSigningKey = true,

                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            services.AddJwtAuthenticationWithProtectedCookie(validationParams);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiresAdmin", policy => policy.RequireClaim("HasAdminRights", "Y"));
                options.AddPolicy("RequiresResearcher", policy => policy.RequireClaim("HasResearcherRights", "Y"));
                options.AddPolicy("RequiresParticipant", policy => policy.RequireClaim("HasParticipantRights", "Y"));

            });

            services.AddMemoryCache();

            services.AddMvc();

            //Use Https
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
            services.AddSingleton<IConfiguration>(Configuration);
           
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();
            app.UseAuthentication();

            //Use Https 
            var options = new RewriteOptions()
                .AddRedirectToHttps();
                
            app.UseRewriter(options);


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "root",
                    template: "{controller=Welcome}/{action=Participant}");
                
            });
        }
    }
}
