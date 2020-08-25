using AppLocator.Application;
using AppLocator.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Linq;

namespace AppLocator.WebApi
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
            services.AddControllers();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
                cfg.AddProfile(new InfrastructureProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<Application.Services.IApplicationService, Application.Services.ApplicationService>();
            services.AddScoped<Application.Repositories.IApplicationRepository, Infrastructure.Repositories.ApplicationRepository>();

            services.AddSingleton(s => new Infrastructure.Context(Configuration["MongoDB:ConnectionString"], Configuration["MongoDB:DatabaseName"]));

            services.AddControllersWithViews().AddNewtonsoftJson();

            services.AddControllersWithViews(options =>
            {
                options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            });

            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = Configuration["AppDetails:Title"],
                    Version = Configuration["AppDetails:Version"],
                    Description = Configuration["AppDetails:Description"],
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Daniel Pessoa",
                        Url = new System.Uri("https://cobalto.dev")
                    }
                });
            });
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Configuration["AppDetails:Title"]} v1");
               });
        }
    }
}
