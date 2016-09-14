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
    public class ForumsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Forums
        public ActionResult Index()
        {
            var topics = db.Topics.ToList();
            
            return View(topics);
        }

        //public ActionResult Topics(int forumId)
        //{
        //    var forum = db.Forums.Find(forumId);

        //    return View(forum);
        //}


        //public ActionResult Comments(int topicId)
        //{
        //    var topic = db.Topics.Find(topicId);

        //    return View(topic);
        //}



    }
}