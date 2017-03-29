using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DTcms.Web.Controllers
{
    public class BaseController : Controller
    {
        protected internal Model.siteconfig config = new BLL.siteconfig().loadConfig();
        protected internal Model.userconfig uconfig = new BLL.userconfig().loadConfig();
        protected internal Model.channel_site site = new Model.channel_site();
    }
}
