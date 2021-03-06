using AspectCore.Test.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspectCore.Test.Web
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
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Information);
            });
            
            services.AddControllers();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.DecorateWithAspect<IWeatherForecastService>();
            // services.DecorateWithAspect(typeof(IWeatherForecastService).Assembly);
            //services.DecorateWithAspect<IAspectDecorated>(typeof(IWeatherForecastService).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}