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

            //注册服务
            services.AddSwaggerGen(u =>
            {
                u.SwaggerDoc("V1", new OpenApiInfo()
                {
                    Title="test",
                    Version="version-01",
                    Description="加油"
                });
            });

            //注册跨域服务
            services.AddCors(u => u.AddPolicy("AllowCors", s => s.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.FromSeconds(1728000))));
            // services.AddCors(u => u.AddPolicy("AllowCors1", s => s.AllowAnyMethod()));



            #region JWT鉴权

            //1.Nuget引入程序包：Microsoft.AspNetCore.Authentication.JwtBearer 
            var validAudience = this.Configuration["audience"];
            var validIssuer = this.Configuration["issuer"];
            var securityKey = this.Configuration["HS256SecurityKey"]; //HS256对称解密秘钥
                                                                      //var securityKey = this.Configuration["RS256SecurityKey"];//RS256公钥


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //默认授权机制名称；                                      
                     .AddJwtBearer(options =>
                     {
                         //配置验证规则
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,//是否验证Issuer
                             ValidateAudience = true,//是否验证Audience
                             ValidateLifetime = true,//是否验证失效时间


                             ValidateIssuerSigningKey = true,//是否验证SecurityKey
                             ValidAudience = validAudience,//Audience
                             ValidIssuer = validIssuer,//Issuer，这两项和前面签发jwt的设置一致  表示谁签发的Token

                             #region HS256--对称秘钥
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
            //使用swagger中间件
            app.UseSwagger();
            app.UseSwaggerUI(u =>
            {
                u.SwaggerEndpoint("/swagger/V1/swagger.json", "test1");
            });

            app.UseRouting();

            //使用跨域中间件，必须在UseRouting和UseAuthorization之间
            app.UseCors("AllowCors");

            #region 通过中间件来支持授权（身份验证）
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
