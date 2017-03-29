using DTcms.Common;
using DTcms.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTcms.Web.Controllers
{
    public class UserController : BaseController
    {
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!IsUserLogin())
            {
                filterContext.Result = new RedirectResult("/home/index");
                return;
            }
            string s = "sss";
            string a = "aaa";
        }

        public bool IsUserLogin()
        {
            //如果Session为Null
            if (HttpContext.Session[DTKeys.SESSION_USER_INFO] != null)
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
                        HttpContext.Session[DTKeys.SESSION_USER_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        //[UserAuthorizeAttribute]
        public ActionResult center()
        {
            string a = "aaa";
            string b = "bbb";
            string c = "ccc";
            string d = "ddd";
            return View();
        }
    }
}
