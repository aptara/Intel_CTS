using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntelGameAPI.Models;
using System.Data;
using System.Web.Mvc;
using log4net;
using System.Net.Http;

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
                var data =  DbHelper.ExecuteDataSet("USP_GETQuestionAndChoice", parameters.ToArray());
                if (data.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        QuestionandAnswer objquestion = new QuestionandAnswer();
                        objquestion.GameId = Convert.ToInt32(row["GameID"]);
                        objquestion.questionId = Convert.ToInt32(row["QuestionID"]);
                        objquestion.question = Convert.ToString(row["Question"]);
                        objquestion.correctChoice = Convert.ToString(row["CorrectChoice"]);
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

        public List<Choice> addChoice(string choice, string CorrectChoice,string Correct,ref List<Choice> choicelist)
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
            try
            {
                List<System.Data.Common.DbParameter> parameters = new List<System.Data.Common.DbParameter>();
                parameters.Add(DbHelper.CreateParameter("GameID", modelResult.GameID));
                parameters.Add(DbHelper.CreateParameter("FirstName", modelResult.FirstName));
                parameters.Add(DbHelper.CreateParameter("LastName", modelResult.LastName));
                parameters.Add(DbHelper.CreateParameter("EmailID", modelResult.EmailID));
                parameters.Add(DbHelper.CreateParameter("Score", modelResult.Score));
                DbHelper.ExecuteNonQuery("USP_SaveResult", parameters.ToArray());
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                response = false;

            }

            return response;
        }


        

    }
}