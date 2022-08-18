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
            //ȫ�ֹ�����
            opt.Filters.Add(typeof(CustomExceptionFilter)))
                .AddNewtonsoftJson(options =>//microsoft.aspnetcore.mvc.newtonsoftjson
                {
                    //����JSON�������ڸ�ʽ
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                }).AddXmlDataContractSerializerFormatters();
            //swagger  nuget install Swashbuckle.AspNetCore
            //��Ŀ->����->ȡ������1701;1702;1591     ���·����xml�ĵ��ļ�
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

            //�������
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
                //����Զ�����֤������
                x.AddScheme<CustomerAuthenticationHandler>(CustomerAuthenticationHandler.CustomerSchemeName, CustomerAuthenticationHandler.CustomerSchemeName);
            }).AddJwtBearer(options => {// ���jwt��֤��
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//�Ƿ���֤Issuer
                        ValidateAudience = true,//�Ƿ���֤Audience
                        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                        ValidAudience = "yourdomain.com",//Audience
                        ValidIssuer = "yourdomain.com",//Issuer���������ǰ��ǩ��jwt������һ��
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))//�õ�SecurityKey
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
            //    //����δ�����쳣
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

            //ʹ�ÿ������
            app.UseCors("CorsPolicy");

            //��Ȩ��֤
            app.UseRefreshTokenExpire();
            app.UseAuthentication();
            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //Ҳ�������������ÿ���//.RequireCors("CorsPolicy");
            });
        }

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterModule(new AutofacModuleRegister());
        //}
    }
}
