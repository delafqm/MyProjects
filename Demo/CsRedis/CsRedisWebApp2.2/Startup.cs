using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsRedisWebApp2._2.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CsRedisWebApp2._2
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
            //使用Redis代替系统缓存
            services.AddDistributedRedisCache(option => {
                option.Configuration = string.Format("{0}:{1},allowAdmin=true,password={2},defaultdatabase={3}", "127.0.0.1", "6379", "Pqvk57eze8Pr@ject94", 15);//最后一个表
                option.InstanceName = "RedisCache:GatewayOcelotAPI:Name";
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //使用第三方Redis工具
            string config0 = string.Format("{0}:{1},allowAdmin=true,password={2},defaultdatabase={3}", "127.0.0.1", "6379", "Pqvk57eze8Pr@ject94", 0);
            //实例化一个单例
            RedisConfig.Database0 = new CSRedis.CSRedisClient(config0);
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
