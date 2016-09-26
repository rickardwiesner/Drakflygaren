using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Drakflygaren.Models;
using Microsoft.AspNet.Identity;
using Drakflygaren.ViewModels;
using Drakflygaren.Helpers;

namespace Drakflygaren.Controllers
{
    public class TopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Topics
        public ActionResult Index()
        {
            var topicViewModels = new List<TopicViewModel>();

            foreach (var topic in db.Topics.ToList())
            {
                topicViewModels.Add(GetTopicViewModel(topic));
            }
            ViewBag.User = User.Identity.Name;


            return View(topicViewModels);
        }

        private TopicViewModel GetTopicViewModel(Topic topic)
        {
            var userId = User.Identity.GetUserId();
            bool liked = db.TopicLikes.Any(tl => tl.TopicId == topic.TopicId && tl.UserId == userId);

            return new TopicViewModel { Topic = topic, Liked = liked };
            
        }

        // GET: Topics/Details/5
        public ActionResult Details(int? id, string message)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();

            if (!db.TopicViews.Any(tv => tv.UserId == userId && tv.TopicId == topic.TopicId) && userId != null)
            {
                db.TopicViews.Add(new TopicView { TopicId = topic.TopicId, UserId = userId });
                db.SaveChanges();
            }

            ViewBag.Message = message;
            return View(new TopicViewModel { Topic = topic });
        }

        [HttpPost]
        public ActionResult SendPost(TopicViewModel model)
        {
            var userId = User.Identity.GetUserId();
            db.TopicComments.Add(new TopicComment
            {
                Text = model.Text,
                TopicId = model.Topic.TopicId,
                UserId = userId,
                CommentDateTime = DateTime.Now,
                
            });

            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.Topic.TopicId });


        }

        [HttpPost]
        public int TopicLike(int topicId)
        {
            var userId = User.Identity.GetUserId();
            var userTopicLike = db.TopicLikes.FirstOrDefault(tl => tl.TopicId == topicId && tl.UserId == userId);

            if (userTopicLike == null)
            {
                db.TopicLikes.Add(new TopicLike { UserId = userId, TopicId = topicId });
            }
            else
            {
                db.TopicLikes.Remove(userTopicLike);

            }
            db.SaveChanges();

            return db.TopicLikes.Where(tl => tl.TopicId == topicId).Count();
        }

        public ActionResult ReportPost(int reportedPostId, ReportCategory reportCategory)
        {       
            var currentUserId = User.Identity.GetUserId();
            var message = string.Empty;
            
            //Bara kolla så att personen inte redan har anmält den här posten
            if (!db.Reports.Any(r => r.CommentId == reportedPostId && r.ReporterId == currentUserId))
            {
                //Här känns det ju helt rätt
                db.Reports.Add(new Report
                {
                    //Ändrade UserId till ReporterId för att EF ska fatta att det är en FK
                    //Hade självklart gått att ändra Reporter till User också..
                    //Annars måste man ange det manuellt [ForeignKey(Bla)]
                    ReporterId = currentUserId,
                    Category = reportCategory,
                    //Samma här CommentToReportId till CommentId
                    CommentId = reportedPostId
                });

                db.SaveChanges();
                message = "Kommentaren har blivit anmäld och kommer att åtgärdas så fort som möjligt";
        }

            else
            {
                message = "Du har redan anmält den här kommentaren";
            }

            //Ta fram postens topic så vi kan skicka tillbaka användaren till rätt ställe
            var topicId = db.TopicComments.Find(reportedPostId).TopicId;

            return RedirectToAction("Details", new { id = topicId, message = message });
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicId,Name,Description")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                topic.CreatedOn = DateTime.Now;
                topic.UserId = userId;


                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        // GET: Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicId,Name,Description,CreatedOn,UserId")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Topics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
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
    }
}
