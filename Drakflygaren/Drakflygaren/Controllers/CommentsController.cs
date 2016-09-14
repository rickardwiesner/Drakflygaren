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
    public class CommentsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Comments
        public ActionResult Comments(int topicId)
        {
            var topic = db.Topics.Find(topicId);

            return View(topic);
        }
    }
}