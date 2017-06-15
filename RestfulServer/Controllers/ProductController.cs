using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Model;
using Newtonsoft.Json;

namespace RestfulServer.Controllers
{
    public class ProductController : ApiController
    {
        public string GetTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        // GET: api/Product
        public IEnumerable<string> GetProducts()
        {
            return new string[] { "value1", "value2" };
        }
        
        public string GetProduct()
        {
            if (!Common.HTTPBasicAuthorization.CheckAuth())
                return "未授权";
            return "value";
        }

        // POST: api/Product
        public string AddProduct([FromBody]Newtonsoft.Json.Linq.JObject value)
        {
            var tmp = value["value"].ToString();
            var p = JsonConvert.DeserializeObject<Product>(tmp);
            return p.Name;
        }

        // PUT: api/Product/5
        public void UpdateProduct(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Product/5
        public void DeleteProduct(int id)
        {
        }
    }

}
