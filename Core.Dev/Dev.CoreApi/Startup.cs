using Dev.CoreApi.Filters;
using Dev.CoreApi.MiddleWare;
using Dev.CoreApi.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Dev.CoreApi
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
            services.AddControllers(opt =>
            //全局过滤器
            opt.Filters.Add(typeof(CustomExceptionFilter)))
                .AddNewtonsoftJson(options =>//microsoft.aspnetcore.mvc.newtonsoftjson
                {
                    //设置JSON返回日期格式
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                }).AddXmlDataContractSerializerFormatters();
            //swagger  nuget install Swashbuckle.AspNetCore
            //项目->属性->取消警告1701;1702;1591     输出路径，xml文档文件
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Dev.CoreApi",
                    Description = "A simple example ASP.NET Core Web API",
                });
                c.IncludeXmlComments($"{System.AppDomain.CurrentDomain.BaseDirectory}/Dev.CoreApi.XML");
            });
            //services.AddScoped<ITestService, TestService>();

            //处理跨域
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("X-Pagination"));

                //options.AddPolicy(name: "_myAllowSpecificOrigins",
                //      policy =>
                //      {
                //          policy.WithOrigins("http://example.com",
                //                              "http://www.contoso.com");
                //      });
            });
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //添加自定义认证处理器
                x.AddScheme<CustomerAuthenticationHandler>(CustomerAuthenticationHandler.CustomerSchemeName, CustomerAuthenticationHandler.CustomerSchemeName);
            }).AddJwtBearer(options => {// 添加jwt验证：
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = "yourdomain.com",//Audience
                        ValidIssuer = "yourdomain.com",//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))//拿到SecurityKey
                    };
                });


            services.AddAutoMapper(typeof(StuProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dev.Core Api"); });
            }
            //else
            //{
            //    //存在未处理异常
            //    app.UseExceptionHandler(appBuilder =>
            //    {
            //        appBuilder.Run(async context =>
            //        {
            //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //            await context.Response.WriteAsJsonAsync(new
            //            {
            //                code = (int)HttpStatusCode.InternalServerError
            //            });
            //        });
            //    });
            //}

            app.UseRouting();

            //使用跨域策略
            app.UseCors("CorsPolicy");

            //授权验证
            app.UseRefreshTokenExpire();
            app.UseAuthentication();
            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //也可以在这里启用跨域//.RequireCors("CorsPolicy");
            });
        }

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterModule(new AutofacModuleRegister());
        //}
    }
}
