using System.IO;
using Giveaway.API.DB;
using Giveaway.API.Shared.Authorization.Handlers;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Filters;
using Giveaway.API.Shared.Helpers;
using Giveaway.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using SharpRaven.Core.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Giveaway.Data.EF;
using Giveaway.Util.Utils;

namespace Giveaway.API
{
    public class Startup
    {
        private IConfiguration ConfigRoot { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            ConfigRoot = builder.Build();
            AutoMapperConfig.RegisterModel();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton((IConfigurationRoot)ConfigRoot);
            services.AddCors();
            services.AddSingleton(ConfigRoot);
            services.AddSingleton<IAuthorizationHandler, ClientHandler>();
            services.AddWebDataLayer();

            services.AddMvc(ConfigureMvc)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;

                });
            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "GiveAway.API", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "GiveAway.API.xml");
                options.IncludeXmlComments(xmlPath);
                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddAuthentication(JwtHelper.ConfigureAuthenticationOptions)
                .AddJwtBearer(Const.Jwt.DefaultScheme, JwtHelper.ConfigureJwtBearerOptions);

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ISettingService, SettingService>();
            services.AddSingleton<ISuperAdminService, SuperAdminService>();
            services.AddSingleton<IAdminService, AdminService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IUserRoleService, UserRoleService>();

            //Use raven to send logs to sentry.io
            services.Configure<RavenOptions>(ConfigRoot.GetSection("RavenOptions"));
            //Add HTTPContextAccessor as Singleton
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Configure RavenClient
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ServiceProviderHelper.Init(app.ApplicationServices);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(ConfigureCors);
            app.UseAuthentication();
            app.UseMvc();
            app.UseContent();

            DbInitializer.Seed(app.ApplicationServices);

#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Giveaway.API v1");
            });
#endif
        }

        private void ConfigureCors(CorsPolicyBuilder builder)
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }

        public static void ConfigureMvc(MvcOptions options)
        {
            options.Filters.Add<JsonExceptionFilter>();
        }
    }
}
