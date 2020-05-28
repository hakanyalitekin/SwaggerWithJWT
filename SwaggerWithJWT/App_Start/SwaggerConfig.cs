using SwaggerWithJWT;
using SwaggerWithJWT.Filters;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
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

                        c.SingleApiVersion("v1", "Swagger With JWT (�stenilen herhangi bir isim verilebilir.)");

                        //Token i�in eklendi.
                        c.ApiKey("Authorization")
                        .Description("Filling bearer token here")
                        .Name("Bearer")
                        .In("header");


                        //Summary i�in eklendi. //Uyar� mesajlar�n� kald�rmak i�in Projenin Proproty'sinde ki Build'in alt�nada ki warning'e 1591 eklenmelidir. 
                        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + @"bin\";
                        var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
                        var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                        c.IncludeXmlComments(commentsFile);

                        //File Upload i�in eklendi.
                        c.OperationFilter<SwaggerParameterOperationFilter>();


                    })
                .EnableSwaggerUi(c =>
                    {
                        //Token i�in eklendi.
                        c.EnableApiKeySupport("Authorization", "header");

                    });
        }


        public class SwaggerParameterOperationFilter : IOperationFilter
        {
            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                var requestAttributes = apiDescription.GetControllerAndActionAttributes<SwaggerParameterAttribute>();
                if (requestAttributes.Any())
                {
                    operation.parameters = operation.parameters ?? new List<Parameter>();

                    foreach (var attr in requestAttributes)
                    {
                        operation.parameters.Add(new Parameter
                        {
                            name = attr.Name,
                            description = attr.Description,
                            @in = attr.Type == "file" ? "formData" : "body",
                            required = attr.Required,
                            type = attr.Type
                        });
                    }

                    if (requestAttributes.Any(x => x.Type == "file"))
                    {
                        operation.consumes.Add("multipart/form-data");
                    }
                }
            }
        }
    }
}
