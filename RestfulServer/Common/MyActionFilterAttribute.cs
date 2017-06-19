using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace RestfulServer.Common
{
    /// <summary>
    /// MVC支持的过滤器 Action（行为）
    /// 分别在Action执行之前和之后执行
    /// </summary>
    public class MyActionFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute    
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}