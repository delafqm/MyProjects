using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MediatRHandlerClass;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MediatRForWebApp2._2
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddMediatR(typeof(Startup).Assembly);

            //services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());

            services.AddMediatR(typeof(IMyMediatR).Assembly);

            //services.AddScoped<ServiceFactory>(p => p.GetService);

            //services.AddScoped<INotificationHandler<PingNotification>, PingNotificationHandler1>();
            //services.AddScoped<INotificationHandler<PingNotification>, PingNotificationHandler2>();
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
