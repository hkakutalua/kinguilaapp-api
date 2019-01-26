using AutoMapper;
using KinguilaAppApi.ApiMapping;
using KinguilaAppApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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

            app.UseMvc();
        }
    }
}
