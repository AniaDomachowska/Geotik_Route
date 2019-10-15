using System.IO;
using Geotik.Route.Repository;
using Geotik.Route.Service;
using Geotik.Route.Service.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Geotik.Route.Api
{
    public class Startup
    {
        private readonly IHostingEnvironment environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var routeMatchingConfig = new RouteMatchingConfig();
            Configuration.Bind("RouteMatching", routeMatchingConfig);
            services.AddSingleton(routeMatchingConfig);

            var mapper = AutoMapperConfig.Install();

            services.AddSingleton(mapper);

            services.AddTransient<IRouteMatchingAdapter, RouteMatchingAdapter>();
            services.AddTransient<IRoutePointService, RoutePointService>();
            services.AddTransient<ITracePointMapper, TracePointMapper>();
            services.AddTransient<ITracePointRequestFormatter, CsvTracePointRequestFormatter>();

            var filePath = Path.Combine(environment.ContentRootPath, Configuration.GetValue<string>("PointDataPath"));

            services.AddTransient<IRoutePointRepository, RoutePointRepository>(provider =>
                new RoutePointRepository(filePath));
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

            app.UseSerilogRequestLogging();


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}