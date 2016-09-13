using Drakflygaren.Models;
using Drakflygaren.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drakflygaren.Controllers
{
    public class EventController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Event
        //Indexsidan visar alla events.
        public ActionResult Index()
        {
            var events = context.Events.ToList();
            return View(events);
        }

        public ActionResult Create()
        {
            return View();
        }

        //Lägger till ett Event.
        [HttpPost]
        public ActionResult Create(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                context.Events.Add(new Event
                {
                    Name = model.EventName,
                    Description = model.EventDesc,
                    ImageUrl = model.EventImageUrl,
                    EventDateTime = model.EventDateTime,
                    LocationId = model.EventLocationId,

                });
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}