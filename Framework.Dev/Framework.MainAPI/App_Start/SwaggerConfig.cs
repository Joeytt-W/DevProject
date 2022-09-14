using System.Web.Http;
using WebActivatorEx;
using Framework.MainAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Framework.MainAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Framework.MainAPI");
                        c.IncludeXmlComments(string.Format("{0}/bin/Framework.MainAPI.XML", System.AppDomain.CurrentDomain.BaseDirectory));//ÉèÖÃxmlµØÖ·
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("Framework.MainAPI");
                        c.InjectJavaScript(thisAssembly, "Framework.MainAPI.Scripts.Swagger.swagger_lang.js");//ºº»¯js
                    });
        }
    }
}
