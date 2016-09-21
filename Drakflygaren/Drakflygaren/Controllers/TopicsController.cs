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

namespace Drakflygaren.Controllers
{
    public class TopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Topics
        public ActionResult Index()
        {
            var topics = db.Topics.Include(t => t.CreatedBy);
            return View(topics.ToList());
        }

        // GET: Topics/Details/5
        public ActionResult Details(int? id)
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
        public int TopicLikes(int topicId)
        {
            var userId = User.Identity.GetUserId();
            var userTopicLike = db.TopicLikes.FirstOrDefault(tl => tl.TopicId == topicId && tl.UserId == userId);

            if(userTopicLike == null)
            {
                db.TopicLikes.Add(new TopicLike { UserId = userId, TopicId = topicId });
            }
            else
            {
                db.TopicLikes.Remove(userTopicLike);
                
            }
            db.SaveChanges();

            var topicLikesCount = db.Topics.Find(topicId).TopicLikes.Count;
            return topicLikesCount;

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
