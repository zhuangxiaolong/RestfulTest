using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            return "value";
        }
        public string GetProduct(string id)
        {
            return "value2";
        }

        // POST: api/Product
        public string AddProduct([FromBody]Newtonsoft.Json.Linq.JObject value)
        {
            var tmp = value["value"].ToString();
            var p = JsonConvert.DeserializeObject<Product>(tmp);
            return p.ToString();
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
