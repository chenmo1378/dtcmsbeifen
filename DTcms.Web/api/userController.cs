using DTcms.Common;
using DTcms.Web.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DTcms.Web.api
{
    
    public class userController : ApiController
    {
        protected string token ;
        protected int userId ;
        protected Model.users userModel;

        //protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        //{
        //    base.Initialize(controllerContext);

        //    if (!IsUserLogin())
        //    {
        //        string json = "{\"status\": 0, \"msg\": \"错误提示：token不存在或已过期！\"}";
        //        //controllerContext = new HttpResponseMessage { Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };
        //        //controllerContext.Controller.
        //        HttpContext.Current.Response.Write(json);
        //        return;
        //    }
        //    token = DTRequest.GetFormString("token");
        //    userId = Convert.ToInt32(Common.CacheHelper.Get2(token));
        //    userModel = new BLL.users().GetModel(userId);
        //}
        
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
        
        public UserModel Index()
        {
            token = DTRequest.GetFormString("token");
            userId = Convert.ToInt32(Common.CacheHelper.Get2(token));
            userModel = new BLL.users().GetModel(userId);

            return new UserModel() { UserID = "000", UserName = "Admin" };
        }
        public string Get(int id)
        {
            token = DTRequest.GetFormString("token");
            userId = Convert.ToInt32(Common.CacheHelper.Get2(token));
            userModel = new BLL.users().GetModel(userId);

            return id.ToString();
        }
        [AppFilter]
        public DataTable aa()
        {
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList(0,"","id desc").Tables[0];
            return dt;
        }

        public string bb()
        {
            return "{\"aa\": \"aaa\",\"bb\": \"bbb\"}";
        }
    }
    public class UserModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
    }
}
