// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 03-05-2019
// ***********************************************************************
// <copyright file="Program.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Cowboy.Stroke.AspNetCore
{
    /// <summary>
    /// Class Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog(Path.Combine(Environment.CurrentDirectory, "nlog.config")).GetCurrentClassLogger();
            WebHost.CreateDefaultBuilder(args)
             .ConfigureLogging((context, logging) =>
             {
                 logging.AddConsole();
                 logging.AddDebug();
                 
             })
             .UseNLog()
             .UseStartup<Startup>()
             .ConfigureLogging((context, logging) =>
             {
                 logging.ClearProviders();
                 logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
             })
             .UseNLog()
             .Build().Run();

        }
    }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();


        //    public static void Main(string[] args)
        //    {
        //        CreateWebHostBuilder(args).Build().Run();
        //    }

        //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //        WebHost.CreateDefaultBuilder(args)
        //            .UseStartup<Startup>();
        //}
    }
