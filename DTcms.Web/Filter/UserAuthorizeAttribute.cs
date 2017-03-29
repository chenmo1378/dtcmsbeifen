using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTcms.Web.Filter
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            if (!IsUserLogin())
            {
                //跳转URL
               // filterContext.HttpContext.Response.Redirect("/home/index", true);
                //return;                
                filterContext.Result = new RedirectResult("/home/index");

                //验证不通过
                //ContentResult Content = new ContentResult();
                //Content.Content = "<script type='text/javascript'>alert('权限验证不通过！');history.go(-1);</script>";
                //filterContext.Result = Content;
            }
        }

        public bool IsUserLogin()
        {
            //如果Session为Null
            if (HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string username = Utils.GetCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms");
                string password = Utils.GetCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms");
                if (username != "" && password != "")
                {
                    BLL.users bll = new BLL.users();
                    Model.users model = bll.GetModel(username, password, 0, 0, false);
                    if (model != null)
                    {
                        HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}