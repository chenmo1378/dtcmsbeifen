using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DTcms.Web
{
    public class Routing : RouteBase
    {
        //参考资料 http://blog.csdn.net/lwj0310/article/details/32929293

        public override RouteData GetRouteData(System.Web.HttpContextBase httpContext)
        {
            //return null;//断点1

            var data = new RouteData(this, new MvcRouteHandler());
            data.Values.Add("controller", "Home");
            data.Values.Add("action", "Index");
            return data;
            //你就会发现，无论你在http//localhost:1234/后面输入任何相对URL，都会被定向到HomeController.Index()方法。
            //因为返回的是路由值而不是null，表示已经找到匹配项，就不会再往下匹配了。<这条规则覆盖了后面所有的规则>s
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //return null;//断点2

            return new VirtualPathData(this, "This-is-a-Test-URL");
            //结果是你通过上述方法构造的URL不论请求来自哪里，全部都会显示成http://localhost:1234/This-is-a-Test-URL
            //因为我们返回的是一个相对路径，而不是null，表示已经找到匹配项，则匹配不会往下继续。<同上这条规则覆盖了后面所有的规则>
        }
    }
}