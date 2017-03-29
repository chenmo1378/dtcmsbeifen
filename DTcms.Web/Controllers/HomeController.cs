using DTcms.Common;
using DTcms.Web.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTcms.Web.Controllers
{
    public class HomeController : BaseController
    {
        

        //
        // GET: /Home/
        public ActionResult Index()
        {
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList(10,"","id desc").Tables[0];
            ViewBag.dt = dt;
            return View();
        }        
    }
}
