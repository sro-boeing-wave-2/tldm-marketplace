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
using Microsoft.EntityFrameworkCore;
using MarketPlaceBackend.Models;
using MarketPlaceBackend.Services;
using MarketPlaceBackend.Contracts;

namespace MarketPlaceBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AppPolicy", builder =>
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowAnyOrigin()
                )
            );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            if (Environment.IsEnvironment("Testing"))
            {
                services.AddDbContext<MarketPlaceBackendContext>(options => options.UseInMemoryDatabase("TesingDb"));
                services.AddDbContext<MarketPlaceBackendContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DockerContext")));
            }
            else
            {
                services.AddDbContext<MarketPlaceBackendContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DockerContext"),
                        sqlServerOptionsAction: sqlOptions =>
                        {
                                sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null
                        );
                        }));
            }
            services.AddTransient<IApplicationService, ApplicationService>();
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
                app.UseHsts();
            }

            if(!env.IsEnvironment("Testing"))
            {
                var context = app.ApplicationServices.GetService<MarketPlaceBackendContext>();
                context.Database.Migrate();
            }
            app.UseCors("AppPolicy");
            app.UseMvc();
        }
    }
}
