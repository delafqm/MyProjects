using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapperForWebApp2._2.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutoMapperForWebApp2._2
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


            //参考资料
            //https://blog.csdn.net/raphaelhe/article/details/103076371
            //
            //项目用下面的资料，能用就未改动
            //https://www.cnblogs.com/NCoreCoder/p/11359294.html
            //https://www.cnblogs.com/NCoreCoder/p/11359294.html


            //方式一：单个注册
            //services.AddAutoMapper(typeof(UserProfile));
            //services.AddAutoMapper(typeof(UserProfile), typeof(UserProfile));

            //方式二：批量注册，继承接口IProfile
            services.AddAutoMapper(typeof(IProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //一次性加载
            app.UseStateAutoMapper();

            app.UseMvc();
        }
    }
}
