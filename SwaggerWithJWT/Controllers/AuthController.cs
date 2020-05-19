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
            if (CheckUser(user))
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

        bool CheckUser(User user)
        {
            if (user != null)
            {
                if (user.UserName  == "admin" && user.Password == "1234")
                    return true;

            }

            return false;
        }
    }
}
