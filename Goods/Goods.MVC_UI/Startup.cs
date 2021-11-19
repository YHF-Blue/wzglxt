using DataEntity.Entities;
using Goods.MVC_UI.Filters;
using IService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.MVC_UI
{
    public class Startup
    {
        /// <summary>
        /// 构造函数依赖注入
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;//把依赖注入给Configuration属性，configuration就是配置文件的信息
        }

        public IConfiguration Configuration { get; }//配置文件的信息，先存到这里

        /// <summary>
        /// 该方法由运行时调用。使用此方法向容器添加服务。注册服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(u=> { u.Filters.Add<MyExceptionAttribute>(); });//使用服务

            /*
            重点： 控制反转、依赖注入的理解
            之前是我们自己new对象，(new对象的控制权在我们手上)，坏处：代码不稳定，依赖多，不好维护，对象创建得不好容易出bug，，
             ，所以，为了写出更稳定，更易维护的代码，需要统一把创建对象的工作交给“别人”（这个思想就是IOC），
            ，然后由“别人”在调用你的代码的时候把你需要new的对象传递进来（这个行为就是DI）
            ，所谓的“别人”，就是IOC容器（本质上就是一个框架，能根据我们的配置，依照我们预设好的思路来帮助我们创建对象，DotNet Core框架已经内置，当然，我们也可以使用第三方的IOC容器，比如autofac）
           */

            //配置session服务
            services.AddSession();//注册session服务

            #region IOC容器的依赖注入配置

            //配置依赖注入的规则--告诉IOC容器：创建对象的规则是什么？-->对象的生命周期（生到死的过程）
            //生命周期1：作用域单例 AddScoped
            //生命周期2：瞬时生命 AddTransient，每用到一次，都会创建一个新的对象
            //生命周期3：单例 AddSingleton 项目启动到结束，只用一个对象（注意，不要乱用-）     

            //告诉IOC容器，待会把 Account赋值给IAccount，同时，生命周期为，作用域单例
            services.AddScoped<IAccountService, AccountService>();//告诉IOC容器，待会把 Account赋值给IAccount，同时，生命周期为，作用域单例
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IGoods_EquipmentService, Goods_EquipmentService>();
            services.AddScoped<IWorkFlowService, WorkFlowService>();
            services.AddScoped<IGoods_ConsumableService,Goods_ConsumableService>();
            services.AddScoped<IPowerService,PowerService>();
            services.AddScoped<IWorkFlowModelService, WorkFlowModelService>();
            services.AddScoped<IGoods_CategoryService, Goods_CategoryService>();
            services.AddScoped<IPowerInfoService, PowerInfoService>();


            

            //Ef Core的依赖注入关系配置
            services.AddScoped<DbContext, LDG_HopuGoodsContext>();
            //配置文档获取连接字符串注入DBContext
            services.AddDbContext<LDG_HopuGoodsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnection"));

            });

            #endregion
        }

        /// <summary>
        /// 该方法由运行时调用。使用此方法配置HTTP请求管道(请求环节)、中间件：就是为了实现某个功能而封装的方法，它能够获取上一个中间件传递来的请求，并把请求传给下一个中间（获取后一个中间件返回的请求，并把请求返回给上一个中间件）。
        /// 中间件是有顺序的，从上往下，所以在使用的时候，需要注意中间件的顺序。。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            //日志中间件
            loggerFactory.AddLog4Net();
            //添加session中间件
            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=LoginView}/{id?}");
            });
        }
    }
}
