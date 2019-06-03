// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 01-21-2019
// ***********************************************************************
// <copyright file="Startup.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cowboy.Stoke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stoke.AspNetCore.IService;
using Cowboy.Stoke.AspNetCore.IService.Implement;
using Cowboy.Stoke.AspNetCore.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Cowboy.Stroke.AspNetCore.Model;
using Cowboy.TravelShop.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.IService.Implement;
using Cowboy.Stroke.AspNetCore.IService;
using Cowboy.Stroke.AspNetCore.Options;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.Entities;
using Microsoft.Extensions.Options;
using Senparc.CO2NET;
using Senparc.Weixin;

namespace Cowboy.Stroke.AspNetCore
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connectionString = Configuration.GetConnectionString("Stroke");
            services.AddDbContext<StrokeContext>(options =>
            {
                options.UseMySql(connectionString);
            });
            // services.AddHangfire(x => x.UseStorage(new MySqlStorage(connectionString, new MySqlStorageOptions { DashboardJobListLimit = 5 })));
            services.AddSenparcGlobalServices(Configuration)//Senparc.CO2NET 全局注册
                     .AddSenparcWeixinServices(Configuration);//Senparc.Weixin 注册

            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerOperationFilter>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                options.AddSecurityRequirement(security);

                options.SwaggerDoc("v1", new Info { Title = "TravelShop", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "请输入带有Bearer的Token, 格式： Bearer {Token}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var xmlPath = Path.Combine(Environment.CurrentDirectory, "Cowboy.Stroke.AspNetCore.xml");
                options.IncludeXmlComments(xmlPath);
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStrokeService, StrokeService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrderingService, OrderingService>();
            services.AddScoped<ISMSService, SMSService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddMemoryCache();
            services.Configure<WeChatOptions> (Configuration.GetSection("Business:WeChat"));
            services.Configure<SMSOptions>(Configuration.GetSection("Business:SMS"));
            services.Configure<JwtOptions>(Configuration.GetSection("AspNetCore:Jwt")); 
            services.Configure<ConfigOptions>(Configuration.GetSection("AspNetCore:Config"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = "Cowboy.Stroke.com",
                        ValidIssuer = "cowboy.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DJ4DT+I;5fX-~;ptoG_2q8:(uCs:Ubu*?*gd"))
                    };
                });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //        {
            //            //ValidateIssuer = true,
            //            //ValidateAudience = true,
            //            //ValidateLifetime = true,
            //            //ValidateIssuerSigningKey = true,
            //            ValidAudience = "Cowboy.Stroke.com",
            //            ValidIssuer = "cowboy.com",
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DJ4DT+I;5fX-~;ptoG_2q8:(uCs:Ubu*?*gd"))
            //        };
            //    });

            services.AddCors(options =>
            {
                options.AddPolicy(Defines.Default_Cors_Policy, c =>
                {
                    c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseHsts();
            //}
            // app.UseMvc();
            app.UseMvc();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api-docs";
                c.ShowExtensions();
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "My Api V1");
            });
            app.UseStaticFiles();
         
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //var jobOptions = new BackgroundJobServerOptions
            //{
            //    WorkerCount = Environment.ProcessorCount * 5,
            //    ServerName = "bcl.baocailang.com",
            //};
            //app.UseHangfireServer(jobOptions);
            //app.UseHangfireDashboard();
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            IRegisterService register = RegisterService.Start(env, senparcSetting.Value).UseSenparcGlobal();// 启动 CO2NET 全局注册，必须！

            register.UseSenparcWeixin(senparcWeixinSetting.Value, senparcSetting.Value);//微信全局注册，必须！
            app.UseCors(Defines.Default_Cors_Policy);
        }
    }
}
