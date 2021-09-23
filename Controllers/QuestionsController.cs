using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using QnA_Website.Models;
using PagedList.Mvc;
using PagedList;
using System.Text.RegularExpressions;

namespace QnA_Website.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        [Authorize(Roles = "Admin")]
        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,DatePosted,NumOfVotes,IsUpvoted")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        [Authorize(Roles = "Admin")]
        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,DatePosted,NumOfVotes,IsUpvoted")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        [Authorize(Roles = "Admin")]
        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AllQuestions(int? i)
        {
            ViewBag.SortParm = "AllQuestions";
            var NewestSorted = db.Questions.OrderByDescending(q => q.DatePosted).ToList();
            return View("AllQuestions", NewestSorted.ToPagedList(i ?? 1, 10));
        }

        public ActionResult SortByPopular(int? i)
        {
            ViewBag.SortParm = "SortByPopular";
            var PopularSorted = db.Questions.OrderByDescending(q => q.Answers.Count).ToList();
            return View("AllQuestions", PopularSorted.ToPagedList(i ?? 1, 10));
        }

        public ActionResult SortByVotes(int? i)
        {
            ViewBag.SortParm = "SortByVotes";
            var SortedByVotes = db.Questions.OrderByDescending(q => q.QuestionVotes.Count).ToList();
            return View("AllQuestions", SortedByVotes.ToPagedList(i ?? 1, 10));
        }

        public ActionResult SortByOldest(int? i)
        {
            ViewBag.SortParm = "SortByOldest";
            var OldestSorted = db.Questions.OrderBy(q => q.DatePosted).ToList();
            return View("AllQuestions", OldestSorted.ToPagedList(i ?? 1, 10));
        }

        [Authorize(Roles = "Moderator,Admin")]
        public ActionResult AskQuestion()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AskQuestion([Bind(Include = "Title,Description,Tags")] string tags,Question question)
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);
            question.DatePosted = DateTime.Now;
            question.ApplicationUser = user;
            var Description = Regex.Replace(question.Description, "<.*?>", String.Empty);
            question.Description = Description;
            List<string> inputTags = tags.Split(new char[] { ',', ' ' },
                                StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach(var tag in inputTags)
            {
                Tag newTag = new Tag();
                newTag.QuestionId = question.Id;
                newTag.Name = tag;
                db.Tags.Add(newTag);
            }
            db.Questions.Add(question);
            db.SaveChanges();
            return View("QuestionDetail",question);
        }

        public ActionResult QuestionWithTag(int? i, string Name)
        {
            ViewBag.SubTitle = "tagged [" + Name + "]";
            var filteredQuestions = db.Questions.Where(q => q.Tags.Any(y => y.Name == Name)).ToList();
            return View("AllQuestions", filteredQuestions.ToPagedList(i ?? 1, 10));
        }

        [Authorize]
        public ActionResult QuestionDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionDetail(Question question)
        {
            return View(question);
        }

        public ActionResult UpVote(int id)
        {
            Question question = db.Questions.Find(id);
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.Find(userId);
            if (question.ApplicationUser != currentUser)
            {
                if(!currentUser.QuestionVotes.Any(v => v.QuestionId == id))
                {
                    QuestionVote newVote = new QuestionVote();
                    newVote.VoteType = VoteType.UpVote;
                    newVote.ApplicationUser = currentUser;
                    newVote.QuestionId = id;
                    question.ApplicationUser.Reputation += 5;
                    var badgeCount = (question.ApplicationUser.Reputation + 5 * 100 / 10) / 100 * 100;
                    badgeCount = int.Parse(badgeCount.ToString()[0].ToString());
                    question.ApplicationUser.GoldenBadges = badgeCount;
                    db.QuestionVotes.Add(newVote);
                }
                else
                {
                    ViewBag.Message = String.Format("Sorry,You cannot vote the same question again!");
                }
            }
            else
            {
                ViewBag.Message = String.Format("Sorry,You cannot vote your own question!");
            }
            db.SaveChanges();
            return View("QuestionDetail", question);
        }

        public ActionResult DownVote(int id)
        {
            Question question = db.Questions.Find(id);
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.Find(userId);
            if (question.ApplicationUser != currentUser)
            {
                if(!currentUser.QuestionVotes.Any(v => v.QuestionId == id))
                {
                    QuestionVote newVote = new QuestionVote();
                    newVote.VoteType = VoteType.DownVote;
                    newVote.ApplicationUser = currentUser;
                    newVote.QuestionId = id;
                    question.ApplicationUser.Reputation -= 5;
                    var badgeCount = (question.ApplicationUser.Reputation + 5 * 100 / 10) / 100 * 100;
                    badgeCount = int.Parse(badgeCount.ToString()[0].ToString());
                    question.ApplicationUser.GoldenBadges = badgeCount;
                    db.QuestionVotes.Add(newVote);
                }
                else
                {
                    ViewBag.Message = String.Format("Sorry,You cannot vote the same question again!");
                }
            }
            else
            {
                ViewBag.Message = String.Format("Sorry,You cannot vote your own question!");
            }
            db.SaveChanges();
            return View("QuestionDetail", question);
        }

        public ActionResult MarkAsAccepted(int Id)
        {
            Answer answer = db.Answers.Find(Id);
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.Find(userId);
            if (answer.Question.ApplicationUser == currentUser)
            {
                if(!answer.Question.Answers.Any(a => a.IsAccepted))
                {
                    answer.IsAccepted = true;
                }
                else
                {
                    ViewBag.Message = String.Format("Sorry,You already have marked one answer as accepted!");
                }
            }
            else
            {
                ViewBag.Message = String.Format("Sorry,You can only mark answer as accepted for your own question!");
            }
            db.SaveChanges();
            return View("QuestionDetail", answer.Question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostCommentOnQuestion(int Id, string comment)
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);
            CommentOnQuestion newComment = new CommentOnQuestion();
            newComment.Content = comment;
            newComment.QuestionId = Id;
            newComment.ApplicationUser = user;
            Question question = db.Questions.Find(Id);
            db.CommentOnQuestions.Add(newComment);
            db.SaveChanges();
            ModelState["comment"].Value = new ValueProviderResult("", "", CultureInfo.CurrentCulture);
            return View("QuestionDetail", question);
        }

        public ActionResult AllTags()
        {
            var newTagList = new Dictionary<Tag, int>();
            HashSet<string> tagNames = new HashSet<string>();
            foreach(var tag in db.Tags.ToList())
            {
                if (tagNames.Add(tag.Name))
                {
                    var filteredQuestions = db.Questions.Where(q => q.Tags.Any(y => y.Name == tag.Name)).ToList();
                    newTagList.Add(tag, filteredQuestions.Count);
                }
            }
            return View(newTagList);
        }

        //TODO: Add page to show all users and their details
    }
}
