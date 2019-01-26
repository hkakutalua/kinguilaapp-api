using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using KinguilaAppApi.ApiMapping;
using KinguilaAppApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace KinguilaAppApi
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; set; }

        public Startup(IHostingEnvironment environment)
        {
            HostingEnvironment = environment;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();
            
            services.AddSwaggerGen(setup =>
            {
               setup.SwaggerDoc("v1", new Info
               {
                   Title = "KinguilaApp API",
                   Version = "v1",
                   Contact = new Contact
                   {
                       Name = "Henrick Kakutalua",
                       Email = "henrykeys96@gmail.com", 
                       Url = "https://medium.com/@henrickpedro"
                   }
               });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            });

            services.AddTransient<IExchangeRateService, KinguilaHojeExchangeRateService>();
            services.AddTransient<IPageTextualInformationParser, KinguilaHojeTextualInformationParser>();
            services.AddSingleton<KinguilaHojeSourceMapper>();
            services.AddTransient<IDateProvider>(x => DateProvider.Default());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "KinguilaApp API");
                setup.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
