﻿using System;
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
    [Authorize]
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        EventViewModel GetEventViewModel(Event @event)
        {
            var userId = User.Identity.GetUserId();

            return new EventViewModel
            {
                Event = @event,
                Liked = db.EventLikes.Any(el => el.EventId == @event.EventId && el.UserId == userId),
                Participating = db.EventParticipants.Any(ep => ep.EventId == @event.EventId && ep.UserId == userId)
            };
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var eventViewModels = new List<EventViewModel>();

            foreach (var @event in db.Events.ToList())      
            {
                eventViewModels.Add(GetEventViewModel(@event));
            }

            return View(eventViewModels);
        }

        [HttpPost]
        public int EventLike(int eventId)
        {
            var userId = User.Identity.GetUserId();
            var userEventLike = db.EventLikes.FirstOrDefault(el => el.EventId == eventId && el.UserId == userId);

            if (userEventLike == null)
            {
                db.EventLikes.Add(new EventLike { UserId = userId, EventId = eventId });
            }

            else
            {
                db.EventLikes.Remove(userEventLike);
            }

            db.SaveChanges();

            var eventLikesCount = db.Events.Find(eventId).EventLikes.Count;
            return eventLikesCount;
        }

        [HttpPost]
        public int EventJoin(int eventId)
        {
            var userId = User.Identity.GetUserId();
            var userEventParticipant = db.EventParticipants.FirstOrDefault(el => el.EventId == eventId && el.UserId == userId);

            if (userEventParticipant == null)
            {
                db.EventParticipants.Add(new EventParticipant { UserId = userId, EventId = eventId });
            }

            else
            {
                db.EventParticipants.Remove(userEventParticipant);
            }

            db.SaveChanges();

            var eventParticipants = db.Events.Find(eventId).Participants.Count;
            return eventParticipants;
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Event @event = db.Events.Find(id);

            if (@event == null)
            {
                return HttpNotFound();
            }

            return View(GetEventViewModel(@event));
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.EventCategoryId = new SelectList(db.EventCategories, "Id", "CategoryName");
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,Name,EventDateTime,ImageUrl,Description,LocationId,EventCategoryId,CreatorId")] Event model)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.Identity.GetUserId();

                model.CreatorId = currentUserId;
                db.Events.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventCategoryId = new SelectList(db.EventCategories, "Id", "CategoryName", model.EventCategoryId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", model.LocationId);
            return View(model);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventCategoryId = new SelectList(db.EventCategories, "Id", "CategoryName", @event.EventCategoryId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", @event.LocationId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,Name,EventDateTime,ImageUrl,Description,LocationId,EventCategoryId,CreatorId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventCategoryId = new SelectList(db.EventCategories, "Id", "CategoryName", @event.EventCategoryId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", @event.LocationId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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

        public ActionResult DeleteEvent(int id)
        {
            var currentEvent = db.Events.FirstOrDefault(x => x.EventId == id);

            db.Events.Remove(currentEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
            //db.Events.Remove;
        }
    }
}