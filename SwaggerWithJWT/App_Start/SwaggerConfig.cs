using SwaggerWithJWT;
using Swashbuckle.Application;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace SwaggerWithJWT
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {

                        c.SingleApiVersion("v1", "Swagger With JWT (Ýstenilen herhangi bir isim verilebilir.)");

                        //Token için eklendi.
                        c.ApiKey("Authorization")
                        .Description("Filling bearer token here")
                        .Name("Bearer")
                        .In("header");


                        //Summary için eklendi. //Uyarý mesajlarýný kaldýrmak için Projenin Proproty'sinde ki Build'in altýnada ki warning'e 1591 eklenmelidir. 
                        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + @"bin\";
                        var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
                        var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                        c.IncludeXmlComments(commentsFile);

                    })
                .EnableSwaggerUi(c =>
                    {
                        //Token için eklendi.
                        c.EnableApiKeySupport("Authorization", "header");

                    });
        }
    }
}
