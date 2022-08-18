using System.Web.Http;
using WebActivatorEx;
using Dev.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Dev.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Dev.Api");
                        c.IncludeXmlComments(string.Format("{0}/bin/Dev.Api.XML", System.AppDomain.CurrentDomain.BaseDirectory));//ÉèÖÃxmlµØÖ·
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("Dev.Api");
                        c.InjectJavaScript(thisAssembly, "Dev.Api.Scripts.Swagger.swagger_lang.js");//ºº»¯js
                    });
        }
    }
}
