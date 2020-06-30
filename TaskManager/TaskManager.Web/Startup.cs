using System;
using System.IO;
using System.Reflection;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using TaskManager.Bll;
using TaskManager.Configuration;
using TaskManager.Dal;
using TaskManager.Email.Template.Engine;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Web.Infrastructure.Extension;
using TaskManager.Web.Infrastructure.Filter;
using TaskManager.Web.Infrastructure.Handler;
using TaskManager.Web.Infrastructure.Middleware;
using TaskManager.Web.Infrastructure.Scheduler;
using TaskManager.Web.Infrastructure.Scheduler.Job.Email;
using TaskManager.Web.Infrastructure.SwaggerConfig;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
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
                options.Filters.Add((new ModelStateValidationFilter()));
            }).AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });

            BllDependencyInstaller.Install(services);
            DalDependencyInstaller.Install(services, Configuration);
            ConfigurationDependencyInstaller.Install(services, Configuration);
            EmailEngineDependencyInstaller.Install(services);
            services.AddSingleton<UnauthorizedApiHandler>();

            services.AddIdentity<User, Role>(
                    options => { options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<TaskManagerDbContext>()
                .AddDefaultTokenProviders();
            
            services.Configure<SecurityStampValidatorOptions>(
                options => options.ValidationInterval = TimeSpan.Zero
            );
            //Configure cookies
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnSigningIn = context =>
                {
                    context.Properties.IsPersistent = true;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = ctx =>
                {
                    var handler = ctx.HttpContext.RequestServices.GetService<UnauthorizedApiHandler>();
                    return handler.Handle(ctx);
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
            services
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Register the Permission policy handlers
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            services.AddHangfire(config =>
            {
                var options = new PostgreSqlStorageOptions()
                {
                    QueuePollInterval = TimeSpan.FromMinutes(5)
                };
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("Hangfire"), options);
            });

            services.AddScoped<INotificationJob, NotificationJob>();
            //Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "InclusiveHub API", Version = "v1"});
                c.DocumentFilter<HideDocsFilter>();
                c.OperationFilter<OptionalParameterOperationFilter>();
                c.GeneratePolymorphicSchemas();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] {new HangfireDashboardAuthorizationFilter()}
            });
            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                WorkerCount = 2
            });
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() {Attempts = 0});
            HangfireJobScheduler.ScheduleRecurringJobs();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}