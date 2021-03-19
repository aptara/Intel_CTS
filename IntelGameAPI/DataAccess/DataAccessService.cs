using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntelGameAPI.Models;
using System.Data;
using System.Web.Mvc;
using log4net;
using System.Net.Http;
using System.Xml.Linq;

namespace IntelGameAPI.DataAccess
{
    public class DataAccessService
    {

        public Question_bank GetQuestionsWithChoice(int gameID)
        {
            Question_bank objQuestionBank = new Question_bank();
            List<QuestionandAnswer> objQuestionsWithChoice = new List<QuestionandAnswer>();
            try
            {
                List<System.Data.Common.DbParameter> parameters = new List<System.Data.Common.DbParameter>();
                parameters.Add(DbHelper.CreateParameter("GameID", gameID));
                var data = DbHelper.ExecuteDataSet("USP_GETQuestionAndChoice", parameters.ToArray());
                if (data.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        QuestionandAnswer objquestion = new QuestionandAnswer();
                        objquestion.GameId = Convert.ToInt32(row["GameID"]);
                        objquestion.questionId = Convert.ToInt32(row["QuestionID"]);
                        objquestion.question = Convert.ToString(row["Question"]);
                        objquestion.correctChoice = Convert.ToString(row["CorrectChoice"]);
                        objquestion.Category = Convert.ToString(row["Category"]);
                        List<Choice> objchoiceLst = new List<Choice>();
                        if (!string.IsNullOrEmpty(Convert.ToString(row["Choice1"])))
                        {
                            objchoiceLst = addChoice(Convert.ToString(row["Choice1"]), "Choice1", Convert.ToString(row["CorrectChoice"]), ref objchoiceLst);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row["Choice2"])))
                        {
                            objchoiceLst = addChoice(Convert.ToString(row["Choice2"]), "Choice2", Convert.ToString(row["CorrectChoice"]), ref objchoiceLst);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row["Choice3"])))
                        {
                            objchoiceLst = addChoice(Convert.ToString(row["Choice3"]), "Choice3", Convert.ToString(row["CorrectChoice"]), ref objchoiceLst);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row["Choice4"])))
                        {
                            objchoiceLst = addChoice(Convert.ToString(row["Choice4"]), "Choice4", Convert.ToString(row["CorrectChoice"]), ref objchoiceLst);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row["Choice5"])))
                        {
                            objchoiceLst = addChoice(Convert.ToString(row["Choice5"]), "Choice5", Convert.ToString(row["CorrectChoice"]), ref objchoiceLst);
                        }
                        objquestion.choices = objchoiceLst;
                        objquestion.UpdatedDate = Convert.ToDateTime(row["UpdatedDate"]);
                        objQuestionsWithChoice.Add(objquestion);

                    }

                }

                objQuestionBank.question_bank = objQuestionsWithChoice;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return objQuestionBank;


            }


            return objQuestionBank;
        }

        public List<Choice> addChoice(string choice, string CorrectChoice, string Correct, ref List<Choice> choicelist)
        {

            Choice objchoice = new Choice();
            objchoice.Text = Convert.ToString(choice);
            objchoice.Value = Convert.ToString(CorrectChoice) == Correct ? 1 : 0;
            choicelist.Add(objchoice);
            return choicelist;
        }

        public bool SaveResult(Result modelResult)
        {
            bool response = true;
            
            var ResultDetailXML = new XElement("ResultDetails", from resultData in modelResult.ResultDetail
                                                                select new XElement("resultDetail",
                           new XElement("QuestionID", resultData.QuestionID),
                           new XElement("SelectedChoice", resultData.SelectedChoice),
                           new XElement("IsCorrectChoice", resultData.IsCorrectChoice)

                          ));
            string resultDetailString = Convert.ToString(ResultDetailXML);
            Logger.Info("SaveResult " + resultDetailString);

            try
            {
                List<System.Data.Common.DbParameter> parameters = new List<System.Data.Common.DbParameter>();
                parameters.Add(DbHelper.CreateParameter("GameID", modelResult.GameID));
                parameters.Add(DbHelper.CreateParameter("FirstName", modelResult.FirstName));
                parameters.Add(DbHelper.CreateParameter("LastName", modelResult.LastName));
                parameters.Add(DbHelper.CreateParameter("EmailID", modelResult.EmailID));
                parameters.Add(DbHelper.CreateParameter("Score", modelResult.Score));
                parameters.Add(DbHelper.CreateParameter("WWID", modelResult.WWID));
                parameters.Add(DbHelper.CreateParameter("ResultDetails", resultDetailString));
                DbHelper.ExecuteNonQuery("USP_SaveResult", parameters.ToArray());

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                response = false;

            }

            return response;
        }

        public List<Report> GetReporDAL(string WWID)
        {
             List<Report> objResultList = new List<Report>();

            try
            {
                List<System.Data.Common.DbParameter> parameters = new List<System.Data.Common.DbParameter>();
                parameters.Add(DbHelper.CreateParameter("WWID", WWID));
                var data = DbHelper.ExecuteDataSet("USP_GETReport", parameters.ToArray());
                if (data.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        Report objReport = new Report();
                        objReport.WWID = Convert.ToString(row["WWID"]);
                        objReport.FirstName = Convert.ToString(row["FirstName"]);
                        objReport.LastName = Convert.ToString(row["LastName"]);
                        objReport.Category = Convert.ToString(row["Category"]);
                        objReport.questionId = Convert.ToInt32(row["questionId"]);
                        objReport.question = Convert.ToString(row["question"]);
                        objReport.CorrectAnswer = Convert.ToString(row["CorrectAnswer"]);
                        objReport.SelectedAnswer = Convert.ToString(row["SelectedAnswer"]);
                        objReport.CorrectChoice = Convert.ToString(row["CorrectChoice"]);
                        objReport.SelectedChoice = Convert.ToString(row["SelectedChoice"]);
                        objReport.IsCorrectChoice = Convert.ToBoolean(row["IsCorrectChoice"]);
                        objReport.AttemptDateandTime = Convert.ToDateTime(row["AttemptDateandTime"]);
                        objReport.Score = Convert.ToDecimal(row["Score"]);
                        objResultList.Add(objReport);


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return objResultList;


            }


            return objResultList;
        }


        public int ValidateWWID(string WWID)
        {    int count = 0;
            List<System.Data.Common.DbParameter> parameters = new List<System.Data.Common.DbParameter>();
            parameters.Add(DbHelper.CreateParameter("WWID", WWID));
            var Data = DbHelper.ExecuteDataSet("USP_ValidateID", parameters.ToArray());
            if (Data.Tables[0].Rows.Count > 0)
            {
                count = 1;
            }
            return count;
        }

    }
}