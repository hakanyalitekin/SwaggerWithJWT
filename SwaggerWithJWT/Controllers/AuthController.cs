using SwaggerWithJWT.Helpers;
using SwaggerWithJWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwaggerWithJWT.Controllers
{
    public class AuthController : ApiController
    {
        [Route("api/Auth/Login")]
        [HttpPost]
        public IHttpActionResult Login(User user)
        {
            string result;
            string token;
              
            if (user.UserName == "admin" && user.Password == "1234")
            {
                token = JwtManager.GenerateToken(user.UserName);
                result = token;
            }
            else
            {
                result = "Kullanıcı adı şifre geçersiz.";
            }

            return Json(result);
        }
    }
}
