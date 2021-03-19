using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelGameAPI.Models
{

    public class Question_bank
    {
        public List<QuestionandAnswer> question_bank { get; set; }
    }
    public class QuestionandAnswer
    {
        public int questionId { get; set; }
        public int GameId { get; set; }
        public string question { get; set; }
        public string correctChoice	{ get; set; }
        public DateTime UpdatedDate	 { get; set; }
        public string Category { get; set; }

        public List<Choice> choices = new List<Choice>();
    }  
    public class Choice
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class Result
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public decimal Score { get; set; }
        public DateTime AttemptDateandTime { get; set; }
        public string WWID { get; set; }
        public List<ResultDetail> ResultDetail = new List<ResultDetail>();

    }

    public class ResultDetail
    {
        public int QuestionID { get; set; }
        public string SelectedChoice { get; set; }
        public bool IsCorrectChoice { get; set; }
    }

    public class Report
    {
        public string WWID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }
        public int questionId { get; set; }
        public string question { get; set; }
        public string CorrectAnswer { get; set; }
        public string CorrectChoice { get; set; }
        public string SelectedChoice { get; set; }
        public string SelectedAnswer { get; set; }
        public bool IsCorrectChoice { get; set; }
        public decimal Score { get; set; }
        public DateTime AttemptDateandTime { get; set; }
    }

}