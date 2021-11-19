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
        /// ���캯������ע��
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;//������ע���Configuration���ԣ�configuration���������ļ�����Ϣ
        }

        public IConfiguration Configuration { get; }//�����ļ�����Ϣ���ȴ浽����

        /// <summary>
        /// �÷���������ʱ���á�ʹ�ô˷�����������ӷ���ע�����
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(u=> { u.Filters.Add<MyExceptionAttribute>(); });//ʹ�÷���

            /*
            �ص㣺 ���Ʒ�ת������ע������
            ֮ǰ�������Լ�new����(new����Ŀ���Ȩ����������)�����������벻�ȶ��������࣬����ά�������󴴽��ò������׳�bug����
             �����ԣ�Ϊ��д�����ȶ�������ά���Ĵ��룬��Ҫͳһ�Ѵ�������Ĺ������������ˡ������˼�����IOC����
            ��Ȼ���ɡ����ˡ��ڵ�����Ĵ����ʱ�������Ҫnew�Ķ��󴫵ݽ����������Ϊ����DI��
            ����ν�ġ����ˡ�������IOC�����������Ͼ���һ����ܣ��ܸ������ǵ����ã���������Ԥ��õ�˼·���������Ǵ�������DotNet Core����Ѿ����ã���Ȼ������Ҳ����ʹ�õ�������IOC����������autofac��
           */

            //����session����
            services.AddSession();//ע��session����

            #region IOC����������ע������

            //��������ע��Ĺ���--����IOC��������������Ĺ�����ʲô��-->������������ڣ��������Ĺ��̣�
            //��������1���������� AddScoped
            //��������2��˲ʱ���� AddTransient��ÿ�õ�һ�Σ����ᴴ��һ���µĶ���
            //��������3������ AddSingleton ��Ŀ������������ֻ��һ������ע�⣬��Ҫ����-��     

            //����IOC����������� Account��ֵ��IAccount��ͬʱ����������Ϊ����������
            services.AddScoped<IAccountService, AccountService>();//����IOC����������� Account��ֵ��IAccount��ͬʱ����������Ϊ����������
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


            

            //Ef Core������ע���ϵ����
            services.AddScoped<DbContext, LDG_HopuGoodsContext>();
            //�����ĵ���ȡ�����ַ���ע��DBContext
            services.AddDbContext<LDG_HopuGoodsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnection"));

            });

            #endregion
        }

        /// <summary>
        /// �÷���������ʱ���á�ʹ�ô˷�������HTTP����ܵ�(���󻷽�)���м��������Ϊ��ʵ��ĳ�����ܶ���װ�ķ��������ܹ���ȡ��һ���м�������������󣬲������󴫸���һ���м䣨��ȡ��һ���м�����ص����󣬲������󷵻ظ���һ���м������
        /// �м������˳��ģ��������£�������ʹ�õ�ʱ����Ҫע���м����˳�򡣡�
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
            
            //��־�м��
            loggerFactory.AddLog4Net();
            //���session�м��
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
