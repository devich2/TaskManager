using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using TaskManager.Bll;
using TaskManager.Common.Utils;
using TaskManager.Configuration;
using TaskManager.Dal;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Web.Infrastructure.Extension;
using TaskManager.Web.Infrastructure.Middleware;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            var appAssembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configRoot = configBuilder.Build();
            Configuration = configRoot;
            Env = hostingEnvironment;
        }
        public IWebHostEnvironment Env { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(new StatusCodeConvention());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            BllDependencyInstaller.Install(services);
            DalDependencyInstaller.Install(services, Configuration);
            ConfigurationDependencyInstaller.Install(services, Configuration);

            //Configure auth
            services.AddIdentity<User, Role>(
                    options => { options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<TaskManagerDbContext>();

            services.AddSingleton<UnauthorizedApiHandler>();

            //Configure cookies
            services.ConfigureApplicationCookie(options =>
            {

                options.Events.OnSigningIn = context =>
                {
                    context.Properties.IsPersistent = true;
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToLogin = ctx =>
                {
                    var handler = ctx.HttpContext.RequestServices.GetService<UnauthorizedApiHandler>();
                    return handler.Handle(ctx);
                };

                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });

            //Configure Swagger
            /*services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InclusiveHub API", Version = "v1" });
                c.DocumentFilter<HideDocsFilter>();
                c.OperationFilter<OptionalParameterOperationFilter>();
                c.GeneratePolymorphicSchemas();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TaskManagerDbContext>();
                var rep = scope.ServiceProvider.GetService<IUnitRepository>();
                //context.Database.EnsureDeleted();
                var list = await 
                    context.Units.Where(x => x.UnitId == 1).Include(x => x.TermInfo).ToListAsync();
                await rep.SaveChangesAsync();

            }
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            /*app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
            */

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
