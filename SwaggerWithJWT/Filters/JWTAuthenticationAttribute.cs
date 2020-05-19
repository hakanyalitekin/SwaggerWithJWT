using SwaggerWithJWT.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SwaggerWithJWT.Filters
{
    public class JWTAuthenticationAttribute : AuthorizationFilterAttribute
    {
        //override yazıp ovirrede yapılabilecek metorlar listesinden OnAuthorization'u seçiyoruz.
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //Gelen request'i kontrol editoruz. Authorization boş ise direk 401 Unauthorized veriyoruz.
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            //Eğer doluysa geçerliliğini kontrol ediyoruz.
            else
            {
                var tokenKey = actionContext.Request.Headers.Authorization.Parameter;
                var decodeToken = JwtManager.GetPrincipal(tokenKey);
                if (decodeToken == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);

                }
            }
        }
    }
}