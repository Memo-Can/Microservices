﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Course.IdentityServer.Data;
using Course.IdentityServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;

namespace Course.IdentityServer
{
	public class Program
	{
		public static int Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
					.MinimumLevel.Debug()
					.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
					.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
					.MinimumLevel.Override("System", LogEventLevel.Warning)
					.MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
					.Enrich.FromLogContext()
					// uncomment to write to Azure diagnostics stream
					//.WriteTo.File(
					//    @"D:\home\LogFiles\Application\identityserver.txt",
					//    fileSizeLimitBytes: 1_000_000,
					//    rollOnFileSizeLimit: true,
					//    shared: true,
					//    flushToDiskInterval: TimeSpan.FromSeconds(1))
					.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
					.CreateLogger();

			try
			{
				var host = CreateHostBuilder(args).Build();

				//using (var scope = host.Services.CreateScope())
				//{
				//    var serviceProvider = scope.ServiceProvider;
				//    var applicationContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
				//    applicationContext.Database.Migrate();

				//    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

				//    if (!userManager.Users.Any())
				//    {
				//        userManager.CreateAsync(new ApplicationUser() { UserName = "memo", Email = "mehmetcan-b@hotmail.com" }, "!'Admin.1234").Wait();
				//    }


				//}
				using (var scope = host.Services.CreateScope())
				{
					var serviceProvider = scope.ServiceProvider;

					var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

					applicationDbContext.Database.Migrate();

					var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

					if (!userManager.Users.Any())
					{
						userManager.CreateAsync(new ApplicationUser { UserName = "memo", Email = "mehmetcan-b@hotmail.com", City = "İstanbul" }, "!'Admin.1234").Wait();
					}
				}


				Log.Information("Starting host...");
				host.Run();
				return 0;
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Host terminated unexpectedly.");
				return 1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
						.UseSerilog()
						.ConfigureWebHostDefaults(webBuilder =>
						{
							webBuilder.UseStartup<Startup>();
						});
	}
}