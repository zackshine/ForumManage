// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration,IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you wan to add an MVC-based UI
            //services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            string migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;


       //     services.AddDbContext<ApplicationDbContext>(options =>
       //                      options.UseSqlServer(connectionString));

       //     services.AddIdentity<ApplicationUser, ApplicationRole>()
       //         .AddEntityFrameworkStores<ApplicationDbContext>()
       //         .AddDefaultTokenProviders();

       //     services.AddIdentityServer()
       //// this adds the config data from DB (clients, resources, CORS)
       //        .AddConfigurationStore(options =>
       //        {
       //            options.ConfigureDbContext = builder =>
       //                builder.UseSqlServer(connectionString,
       //                    sql => sql.MigrationsAssembly(migrationsAssembly));
       //        });
            var builder = services.AddIdentityServer()
                
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients());

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to support static files
            //app.UseStaticFiles();

            app.UseIdentityServer();

            // uncomment, if you wan to add an MVC-based UI
            //app.UseMvcWithDefaultRoute();
        }
    }
}