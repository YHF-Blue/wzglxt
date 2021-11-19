using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
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

            //ע�����
            services.AddSwaggerGen(u =>
            {
                u.SwaggerDoc("V1", new OpenApiInfo()
                {
                    Title="test",
                    Version="version-01",
                    Description="����"
                });
            });

            //ע��������
            services.AddCors(u => u.AddPolicy("AllowCors", s => s.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.FromSeconds(1728000))));
            // services.AddCors(u => u.AddPolicy("AllowCors1", s => s.AllowAnyMethod()));



            #region JWT��Ȩ

            //1.Nuget����������Microsoft.AspNetCore.Authentication.JwtBearer 
            var validAudience = this.Configuration["audience"];
            var validIssuer = this.Configuration["issuer"];
            var securityKey = this.Configuration["HS256SecurityKey"]; //HS256�Գƽ�����Կ
                                                                      //var securityKey = this.Configuration["RS256SecurityKey"];//RS256��Կ


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //Ĭ����Ȩ�������ƣ�                                      
                     .AddJwtBearer(options =>
                     {
                         //������֤����
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,//�Ƿ���֤Issuer
                             ValidateAudience = true,//�Ƿ���֤Audience
                             ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��


                             ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                             ValidAudience = validAudience,//Audience
                             ValidIssuer = validIssuer,//Issuer���������ǰ��ǩ��jwt������һ��  ��ʾ˭ǩ����Token

                             #region HS256--�Գ���Կ
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
                             #endregion




                         };
                     });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //ʹ��swagger�м��
            app.UseSwagger();
            app.UseSwaggerUI(u =>
            {
                u.SwaggerEndpoint("/swagger/V1/swagger.json", "test1");
            });

            app.UseRouting();

            //ʹ�ÿ����м����������UseRouting��UseAuthorization֮��
            app.UseCors("AllowCors");

            #region ͨ���м����֧����Ȩ�������֤��
            app.UseAuthentication();
            #endregion

            app.UseAuthorization();



           

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
