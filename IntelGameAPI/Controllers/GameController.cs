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
                    Logger.Info("GameID:" + gameID);
                    Question_bank objqb = new Question_bank();
                    DataAccessService objservice = new DataAccessService();
                    var data = objservice.GetQuestionsWithChoice(gameID);
                    return Request.CreateResponse(HttpStatusCode.OK, data, Configuration.Formatters.JsonFormatter);
                   
       }

        [IsAuthorizedFilter]
        [HttpPost]
        [Route("api/Game/SaveProfile")]
        public HttpResponseMessage SaveProfile([FromBody] Result modelResult) //[FromBody]
        {
            DataAccessService objservice = new DataAccessService();
            try
            {
                Logger.Info("SaveProfile Exception:" + modelResult.WWID + " Result Detail Count - "+ Convert.ToString(modelResult.ResultDetail.Count()));
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
                   Logger.Info("SaveProfile Exception:" + ex);
                   return Request.CreateErrorResponse( HttpStatusCode.ExpectationFailed, "Failed",ex);
	            }
           
        }

        [IsAuthorizedFilter]
        [HttpPost]
        [Route("api/Game/GetReport")]
        public HttpResponseMessage GetReport(string WWID)
        {
            Logger.Info("WWID:" + WWID);
            List<Report> objReportList = new List<Report>() ;
            DataAccessService objservice = new DataAccessService();
            objReportList = objservice.GetReporDAL(WWID);
            return Request.CreateResponse(HttpStatusCode.OK, objReportList, Configuration.Formatters.JsonFormatter);

        }

        [IsAuthorizedFilter]
        [HttpPost]
        [Route("api/Game/WWIDValidation")]
        public HttpResponseMessage WWIDValidation(string WWID)
        {
            Logger.Info("WWID:" + WWID);
            Question_bank objqb = new Question_bank();
            DataAccessService objservice = new DataAccessService();
            int Count = objservice.ValidateWWID(WWID);
            if (Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "WWID already Exist in Database.", Configuration.Formatters.JsonFormatter);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "WWID Not Exist", Configuration.Formatters.JsonFormatter);
            }

        }



    }
}
