using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Drakflygaren.Models;
using Drakflygaren.ViewModels;

namespace Drakflygaren.Controllers
{
    public class TopicsController : Controller

    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Topics
        public ActionResult Topics(int forumId)
        {
            var forum = db.Forums.Find(forumId);

            return View(forum);
        }






    }
}