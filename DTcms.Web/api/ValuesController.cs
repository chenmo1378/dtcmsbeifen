using DTcms.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web.api
{
    public class ValuesController : ApiController
    {
        string token = DTRequest.GetFormString("token");
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage userInfo()
        {
            int userId = DTRequest.GetFormInt("userId");
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(userId);
            if (model == null)
            {
                //context.Response.Write("{\"status\":0, \"msg\":\"用户不存在！\"}");
                //return;
                string ss= "{\"status\": 0, \"msg\": \"错误提示：站点传输参数不正确！\"}";
                return new HttpResponseMessage { Content = new StringContent(ss, System.Text.Encoding.UTF8, "application/json") };
            }
            string s = LitJson.JsonMapper.ToJson(model);
            //return new HttpResponseMessage { Content = new StringContent(s, System.Text.Encoding.UTF8, "application/json") };
            JObject obj = JsonConvert.DeserializeObject(s) as JObject;

            //移除无用字段
            obj.Remove("password");
            obj.Remove("password2");
            obj.Remove("amount");
            obj.Remove("point");
            obj.Remove("exp");
            obj.Remove("isweixin");
            obj.Remove("wid");
            obj.Remove("wxCard");
            obj.Remove("wxOpenId");
            obj.Remove("wxName");
            obj.Remove("xin");
            obj.Remove("yushu");
            obj.Remove("shangjilist");
            //添加需要的
            obj["status"] = 1;
            obj["msg"] = "获取信息成功！";
            obj["provinceStr"] = "";
            obj["cityStr"] = "";
            obj["hangyeStr"] = "";
            s = JsonConvert.SerializeObject(obj);
            return new HttpResponseMessage { Content = new StringContent(s, System.Text.Encoding.UTF8, "application/json") };
            //return s;
        }

        [HttpPost]
        public HttpResponseMessage Upload()
        {
            string json = "{\"result\":\"true\"}";
            return new HttpResponseMessage { Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };
        }
    }
}