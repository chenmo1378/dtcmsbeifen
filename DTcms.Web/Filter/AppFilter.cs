using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DTcms.Web.Filter
{
    public class AppFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            if (!IsUserLogin())
            {                
                string json="{\"status\": 0, \"msg\": \"错误提示：token不存在或已过期！\"}";
                actionContext.Response = new HttpResponseMessage{ Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };
                return;
            }
        }
        

        public bool IsUserLogin()
        {
            string token = DTRequest.GetFormString("token");
           
            if (!String.IsNullOrEmpty(token))
            {
                object obj = Common.CacheHelper.Get2(token);
                if (obj != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}