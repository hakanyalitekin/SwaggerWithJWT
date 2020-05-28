using System;

namespace SwaggerWithJWT.Filters
{
    //[AttributeUsage(AttributeTargets.Method)]
    public class SwaggerParameterAttribute:Attribute
    {
        public string Name { get; set; } = "File";

        public string Description { get; set; } = "Select a file to upload";

        public string Type { get; set; } = "file";

        public bool Required { get; set; } = false;
    }
}