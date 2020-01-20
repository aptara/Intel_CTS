using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntelGameAPI.Models;
using IntelGameAPI.DataAccess;
using System.Web.Helpers;
using System.Text;
using System.Configuration;
using IntelGameAPI.Filters;
using System.Web.Http.Cors;

namespace IntelGameAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GameController : ApiController
    {

        [IsAuthorizedFilter]
        [HttpPost]
        [Route("api/Game/GetQuestionsWithChoice")]
        public HttpResponseMessage GetQuestionsWithChoice(int gameID)
        {
            
                    Question_bank objqb = new Question_bank();
                    DataAccessService objservice = new DataAccessService();
                    var data = objservice.GetQuestionsWithChoice(gameID);
                    return Request.CreateResponse(HttpStatusCode.OK, data, Configuration.Formatters.JsonFormatter);
       }


        [IsAuthorizedFilter]
        [HttpPost]
        [Route("api/Game/SaveProfile")]
        public HttpResponseMessage SaveResult([FromBody] Result modelResult) //[FromBody]
        {
            DataAccessService objservice = new DataAccessService();
            try
            {
                if (!String.IsNullOrEmpty(modelResult.FirstName) && !String.IsNullOrEmpty(modelResult.LastName))
                {
                    var data = objservice.SaveResult(modelResult);
                    if (data)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Saved Successfully", Configuration.Formatters.JsonFormatter);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Exception Occured!", Configuration.Formatters.JsonFormatter);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "First Name and LastName is required.", Configuration.Formatters.JsonFormatter);
                
                }

	            }
	            catch (Exception ex)
	            {
		
		             return Request.CreateErrorResponse( HttpStatusCode.ExpectationFailed, "Failed",ex);
	            }
           
        }
    }
}
