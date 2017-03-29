using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web.api
{
    public class AuthController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Token(string appid = "", string appsecret = "")
        {
            ApiResponseEntity rep;
            var isv = AppManage.Instance.GetAppISV(appid, appsecret);
            if (isv != null)
            {
                string token = CreateToken(appid);

                rep = new ApiResponseEntity
                {
                    Status = InterfaceStatus.Success,
                    BizData = new
                    {
                        AccessToken = token
                    }
                };
            }
            else
            {
                rep = new ApiResponseEntity()
                {
                    Status = InterfaceStatus.Parm_Missing,
                    Message = "param error"
                };
            }
            return rep.ToHttpResponseMessage();
        }

        public string CreateToken(string appid)
        {
            string token = Guid.NewGuid().ToString().ToMd5();
            Set(token, appid);
            return token;
        }
        public void Set(string token, string appid)
        {
            var config = ServerConfigManage.Instance.GetServerConfig();
            string key = string.Format(RedisCacheKey.App_Token, token);
            RedisNetHelper.Set<string>(key, appid, DateTime.Now.AddSeconds(config.TokenSurvivalTime));
        }
    }
}