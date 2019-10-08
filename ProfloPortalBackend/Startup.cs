using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProfloPortalBackend.BusinessLayer;
using ProfloPortalBackend.DataAccessLayer;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ProfloPortalBackend.Hubs;
using ProfloPortalBackend.RealTime;
using Newtonsoft.Json;

namespace ProfloPortalBackend
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
            services.AddScoped<DBContext>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITeamRepository, TeamImplements>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IListService, ListService>();
            services.AddScoped<IListRepository, ListRepository>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IProfloRTService, ProfloRTService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IInviteService, InviteService>();
            services.AddScoped<IInviteRepository, InvitesRepository>();
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options => {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddCors(x => x.AddPolicy("ProfloCorsPolicy", builder =>
            {
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                // .WithOrigins("http://core.proflo.cgi-wave7.stackroute.io", 
                // "http://core-api.proflo.cgi-wave7.stackroute.io", "http://localhost:4200")
                .AllowAnyOrigin()
                .AllowCredentials();
            }));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.IncludeErrorDetails = true;
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = false,
                    ValidateActor = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"))
                };
            });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSwaggerUI(n =>
            {
                n.SwaggerEndpoint("/swagger.json", "Core_Backend_Proflo");
            });
            app.UseCors("ProfloCorsPolicy");
            app.UseSignalR(routes => {
                routes.MapHub<ProfloHub>("/proflo");
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
