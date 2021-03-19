using IntelGameAPI.DataAccess;
using IntelGameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IntelGameAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        [HttpPost]
        public ActionResult Report(string WWID = null)
        {
            List<Report> obj = new List<Report>();
            DataAccessService objservice = new DataAccessService();
            obj = objservice.GetReporDAL(WWID);
            StringBuilder str = new StringBuilder();
            str.Append("<table border=`" + "1px" + "`b>");
            str.Append("<tr>");
            str.Append("<td><b><font face=Arial Narrow size=3>FirstName</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>LastName</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>WWID</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Category</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>QuestionId</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Question</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>CorrectAnswer</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>SelectedAnswer</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>CorrectChoice</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>SelectedChoice</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>IsCorrectChoice</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>AttemptDateandTime</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Score</font></b></td>");
            str.Append("</tr>");
            foreach (Report val in obj)
            {
                str.Append("<tr>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.FirstName) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.LastName) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.WWID) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.Category) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.questionId) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.question) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.CorrectAnswer) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.SelectedAnswer) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.CorrectChoice) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.SelectedChoice) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.IsCorrectChoice) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.AttemptDateandTime) + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToString(val.Score) + "</font></td>");

                str.Append("</tr>");
            }
            str.Append("</table>");
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Report" + DateTime.Now.Year.ToString() + ".xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            return File(temp, "application/vnd.ms-excel");
        }



        //[HttpPost]
        //public System.Web.Http.IHttpActionResult GetSCARFSurveyAnswerData(string WWID = null)
        //{
        //    List<Report> objReportList = null;

        //    DataAccessService objservice = new DataAccessService();
        //    objReportList = objservice.GetReporDAL(WWID); ;
        //    if (objReportList == null)
        //    {
        //        return Json(new List<Report>(), JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(objReportList, JsonRequestBehavior.AllowGet);
        //}
    }
} 
   
