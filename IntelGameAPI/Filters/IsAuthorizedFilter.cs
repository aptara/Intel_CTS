using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

using System.Web.Http.Filters;
using System.Net;
using System.Text;
using System.Net.Http.Headers;

namespace IntelGameAPI.Filters
{

    public class IsAuthorizedFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
           
            var headers = filterContext.Request.Headers;
            if (headers.Contains("Authorization") && headers.GetValues("Authorization").First() == ConfigurationManager.AppSettings["AuthorizeCode"].ToString())
            {
                base.OnActionExecuting(filterContext);                

            }
            else
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"message\":\"Unauthorized Request\",\"response\":\"401\"}"),
                    StatusCode = HttpStatusCode.Unauthorized                    
                };
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                filterContext.Response = responseMessage;
            }


            
        }
    }
}