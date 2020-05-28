using SwaggerWithJWT.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SwaggerWithJWT.Controllers
{
    //[JWTAuthentication]
    public class HomeController : ApiController
    {
        // GET: api/Home
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }



        #region FileUpload
        [Route("api/Home/UploadBoQFile")]
        [HttpPost]
        //[SwaggerParameter(Name ="Dosya", Description ="Excel dosyası", Required =true, Type ="file")]
        [SwaggerParameter]
        public HttpResponseMessage UploadBoQFile()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            var httpRequest = HttpContext.Current.Request;

            //var file = httpRequest.Files[fileName];
            var filePath = HttpContext.Current.Server.MapPath("~/" + file.FileName);
            file.SaveAs(filePath);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
        #endregion

    }
}
