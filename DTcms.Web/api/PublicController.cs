using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web.api
{
    public class PublicController : ApiController
    {
        Model.userconfig userConfig = new BLL.userconfig().loadConfig();

        [HttpPost]
        public HttpResponseMessage GetToken()
        {
            string json = "{\"status\": 0, \"msg\": \"错误提示：请输入账号或密码！\"}";            
            string name = DTRequest.GetFormString("name");
            string pwd = DTRequest.GetFormString("pwd");
            if (String.IsNullOrEmpty(name)||String.IsNullOrEmpty(pwd))
            {
                json = "{\"status\": 0, \"msg\": \"错误提示：请输入账号或密码！\"}";
            }
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(name, pwd, userConfig.emaillogin, userConfig.mobilelogin, true);
            if (model == null)
            {
                json="{\"status\":0, \"msg\":\"用户名或密码错误，请重试！\"}";
            }
            //检查用户是否通过验证
            if (model.status == 1) //待验证
            {
                json="{\"status\":0, \"msg\":\"尚未通过验证！\"}";
            }
            else if (model.status == 2) //待审核
            {
                json="{\"status\":0, \"msg\":\"尚未通过审核！\"}";
            }
            //写入登录日志
            new BLL.user_login_log().Add(model.id, model.user_name, "会员登录");
            string token = CreateToken(model.id);
            json = "{\"status\":1, \"msg\":\""+token+"\"}";
            return new HttpResponseMessage { Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };
        }

        public string CreateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            Common.CacheHelper.Insert2(token, userId, 20);
            return token;
        }
    }
}