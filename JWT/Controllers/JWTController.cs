using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using JWT.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Route("api/JWT/[action]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        [HttpGet("{basicKey}")]
        [HttpGet]
        //basicKey + [appsettings.json => Secret] to generate Unique-Token
        public ActionResult<IEnumerable<string>> GetJWT(string basicKey)
        {
            var jwtStr = string.Empty;
            var status = string.Empty;
            if (string.IsNullOrEmpty(basicKey))
            {
                return new JsonResult(new
                {
                    Status = "Fail",
                    message = 
                        "Please input basicKey in the end of 'api/JWT/GetJWT' to generete token!"
                });
            }
            JWTModel jwtModel = new JWTModel()
            {
                UId = 1,
                Role = "Admin",
            };
            jwtStr = JWTCore.GenerateJWT(jwtModel);
            status = "Success";
            return Ok(new
            {
                status = status,
                token = jwtStr
            });
        }

        [HttpGet]
        [Authorize]
        //Before use JWT.We should addtch one key-value in URL Headers
        //Authorization : Bearer + [Token]
        public ActionResult<string> UseJWT()
        {
            //cause obtained jwt secret as a service inject to 'Startup=>ConfigureServices'
            //And we could transform this encrept secret in HttpContext too.
            return "Authorize Confirm,Allow Access";
        }
    }
}
