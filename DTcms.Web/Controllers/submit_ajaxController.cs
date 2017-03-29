using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTcms.Web.Controllers
{
    public class submit_ajaxController : Controller
    {
        /// <summary>
        /// denglu
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string name, string pwd)
        {            
            if (name == "123" && pwd == "456")
            {
                BLL.users bll = new BLL.users();
                Model.users model = bll.GetModel(1);
                Session[DTKeys.SESSION_USER_INFO] = model;
                Session.Timeout = 45;
                Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name, 43200);
                Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password, 43200);
                return RedirectToAction("center", "User");
            }
            return Content("{\"status\": 0, \"msg\": \"温馨提示：请输入用户名或密码！\"}");
        }

    }
}
