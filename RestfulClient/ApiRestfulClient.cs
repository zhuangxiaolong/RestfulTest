
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Authenticators;

namespace RestfulClient
{
    public class ApiRestfulClient
    {
        private RestClient restClient;

        public ApiRestfulClient(string baseurl)
        {
            this.restClient = new RestClient(baseurl);
        }

        public TR execute<TR>(Method method, string resource, Dictionary<string, string> headers, object input = null) //where TR : new()
        {
            var request = new RestRequest(resource, this.map(method));
            request.RequestFormat = DataFormat.Json;

            this.requestInit(method, request, headers, input);

            restClient.Authenticator = new HttpBasicAuthenticator("username", "password");
            var response = restClient.Execute(request);

            //if (response.Data != null)
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return (TR)JsonDeserializer.Deserialize(response.Content, typeof(TR));
                //return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                //return default(TR);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    //intenal error
                    //MainForm.LogExceptionToFile(IRR.Content);
                }
                throw new Exception(string.Format("{0}:{1}", response.StatusCode, response.StatusDescription ?? response.ErrorMessage ?? ""));
            }

            return default(TR);
        }

        public void executeAsync<TR>(Method method, string resource, Dictionary<string, string> headers, Action<TR, Exception> callBack, object input = null) //where TR : new()
        {
            var request = new RestRequest(resource, this.map(method));
            request.RequestFormat = DataFormat.Json;

            this.requestInit(method, request, headers, input);
            restClient.Authenticator = new HttpBasicAuthenticator("username", "password");
            restClient.ExecuteAsync(request, IRR =>
            {
                //JsonDeserializer.Deserialize(IRR.Content, typeof(TR));
                if (IRR.StatusCode == System.Net.HttpStatusCode.OK)
                    callBack((TR)JsonDeserializer.Deserialize(IRR.Content, typeof(TR)), null);
                else if (IRR.StatusCode == System.Net.HttpStatusCode.NoContent)
                    callBack(default(TR), null);
                else
                {
                    if (IRR.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        //intenal error
                        //MainForm.LogExceptionToFile(IRR.Content);
                    }
                    callBack(default(TR), new Exception(string.Format("{0}:{1}", IRR.StatusCode, IRR.StatusDescription ?? IRR.ErrorMessage ?? "")));
                }
            });
        }


        private void requestInit(Method method, RestRequest request, Dictionary<string, string> headers, object input)
        {
            if (input != null)
            {
                headers["Content-Type"] = "text/json";//"text/xml";
            }

            foreach (var kv in headers)
            {
                request.AddHeader(kv.Key, kv.Value);
            }

            if (input != null)
            {
                request.AddBody(input);
            }
        }

        private RestSharp.Method map(Method method)
        {
            switch (method)
            {
                case Method.POST:
                    return RestSharp.Method.POST;
                case Method.PUT:
                    return RestSharp.Method.PUT;
                case Method.DELETE:
                    return RestSharp.Method.DELETE;
                case Method.GET:
                    return RestSharp.Method.GET;
                default:
                    return RestSharp.Method.GET;
            }
        }
        public string GetTime()
        {
            var dicHeader = GetBasicAuthorizationHeader();
            return this.execute<string>(Method.GET,
                    "/api/product/GetTime",
                    dicHeader);
        }

        public string AddProduct(Model.Product p)
        {
            var dicHeader = GetBasicAuthorizationHeader();
            return this.execute<string>(Method.POST,
                    "/api/product/AddProduct",
                    dicHeader,
                    new { value = p });
        }
        public string GetProduct()
        {
            var dicHeader = GetBasicAuthorizationHeader();
            return execute<string>(Method.GET,
                    "/api/product/GetProduct",
                    dicHeader,
                    new {  });
        }

        private Dictionary<string, string> GetBasicAuthorizationHeader()
        {
            var dicHeader = new Dictionary<string, string>();
            return dicHeader;
            //由RestSharp的Authenticator实现
            string username = "username";
            string password = "password";

            string svcCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));

            dicHeader.Add("Authorization", "Basic " + svcCredentials);
            return dicHeader;
        }
    }

    public enum Method
    {
        GET,
        PUT,
        POST,
        DELETE
    }
}
