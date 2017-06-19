using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace RestfulServer.Common
{
    public static class HTTPBasicAuthorization
    {
        /// <summary>
        /// HTTP基本认证的格式，HTTP请求时使用HTTP头来传递参数：这种认证比较简单，但安全性一般
        ///明文格式： "username" + ":" + "password"，需要Base64编码明文。
        ///HTTP头设置：Authorization: Basic [Base64编码内容]
        /// </summary>
        /// <returns></returns>
        public static bool CheckAuth()
        {
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic",
                        StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    return AuthenticateUser(authHeaderVal.Parameter);
                }
            }
            return false;
        }
        // TODO: Here is where you would validate the username and password.
        private static bool CheckPassword(string username, string password)
        {
            return username == "username" && password == "password";
        }

        private static bool AuthenticateUser(string credentials)
        {
            try
            {
                var encoding = Encoding.UTF8;
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                if (CheckPassword(name, password))
                {
                    var identity = new GenericIdentity(name);
                    SetPrincipal(new GenericPrincipal(identity, null));
                    return true;
                }
                else
                {
                    // Invalid username or password.
                    HttpContext.Current.Response.StatusCode = 401;
                }
            }
            catch (FormatException)
            {
                // Credentials were not formatted correctly.
                HttpContext.Current.Response.StatusCode = 401;
            }
            return false;
        }
        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

    }
}