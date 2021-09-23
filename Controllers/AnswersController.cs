using Microsoft.AspNet.Identity;
using QnA_Website.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QnA_Website.Controllers
{
    public class AnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult UpVoteAnswer(int Id)
        {
            Answer answer = db.Answers.Find(Id);
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.Find(userId);
            if (answer.ApplicationUser != currentUser)
            {
                if (!currentUser.AnswerVotes.Any(v => v.AnswerId == Id))
                {
                    AnswerVote newVote = new AnswerVote();
                    newVote.VoteType = VoteType.UpVote;
                    newVote.ApplicationUser = currentUser;
                    newVote.AnswerId = Id;
                    answer.ApplicationUser.Reputation += 5;
                    var badgeCount = (answer.ApplicationUser.Reputation + 5 * 100 / 10) / 100 * 100;
                    badgeCount = int.Parse(badgeCount.ToString()[0].ToString());
                    answer.ApplicationUser.GoldenBadges = badgeCount;
                    db.AnswerVotes.Add(newVote);
                }
                else
                {
                    ViewBag.Message = String.Format("Sorry,You cannot vote the same answer again!");
                }
            }
            else
            {
                ViewBag.Message = String.Format("Sorry,You cannot vote your own answer!");
            }
            db.SaveChanges();
            return View("~/Views/Questions/QuestionDetail.cshtml", answer.Question);
        }

        public ActionResult DownVoteAnswer(int Id)
        {
            Answer answer = db.Answers.Find(Id);
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.Find(userId);
            if (answer.ApplicationUser != currentUser)
            {
                if (!currentUser.AnswerVotes.Any(v => v.AnswerId == Id))
                {
                    AnswerVote newVote = new AnswerVote();
                    newVote.VoteType = VoteType.DownVote;
                    newVote.ApplicationUser = currentUser;
                    newVote.AnswerId = Id;
                    answer.ApplicationUser.Reputation -= 5;
                    var badgeCount = (answer.ApplicationUser.Reputation + 5 * 100 / 10) / 100 * 100;
                    badgeCount = int.Parse(badgeCount.ToString()[0].ToString());
                    answer.ApplicationUser.GoldenBadges = badgeCount;
                    db.AnswerVotes.Add(newVote);
                }
                else
                {
                    ViewBag.Message = String.Format("Sorry,You cannot vote the same answer again!");
                }
            }
            else
            {
                ViewBag.Message = String.Format("Sorry,You cannot vote your own answer!");
            }
            db.SaveChanges();
            return View("~/Views/Questions/QuestionDetail.cshtml", answer.Question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostAnswer(int Id, string content)
        {
            Answer answer = new Answer();
            answer.DatePosted = DateTime.Now;
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);
            Question question = db.Questions.Find(Id);
            answer.ApplicationUser = user;
            answer.Content = content;
            answer.QuestionId = Id;
            db.Answers.Add(answer);
            db.SaveChanges();
            ModelState["content"].Value = new ValueProviderResult("", "", CultureInfo.CurrentCulture);
            return View("~/Views/Questions/QuestionDetail.cshtml", question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostCommentOnAnswer(int AnswerId, string comment)
        {
            Answer answer = db.Answers.Find(AnswerId);
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);
            CommentOnAnswer newComment = new CommentOnAnswer();
            newComment.Content = comment;
            newComment.Answer = answer;
            newComment.ApplicationUser = user;
            db.CommentOnAnswers.Add(newComment);
            db.SaveChanges();
            ModelState["comment"].Value = new ValueProviderResult("", "", CultureInfo.CurrentCulture);
            return View("~/Views/Questions/QuestionDetail.cshtml", answer.Question);
        }
    }
}