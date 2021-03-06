﻿using Drakflygaren.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drakflygaren.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("ReportedComments", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ReportedComments()
        {
            if (User.IsInRole("Admin"))
            {
                var reportedComments = context.Reports.ToList();
                return View(reportedComments);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChoosenComment(int commentId)
        {
            var choosenComment = context.Reports.Find(commentId);

            return PartialView(choosenComment);
        }
        public ActionResult DeleteChoosenComment(int commentId)
        {
            var choosenReportedComment = context.Reports.Find(commentId);
            context.Reports.Remove(choosenReportedComment);

            var comment = context.TopicComments.Find(commentId);
            context.TopicComments.Remove(comment);

            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult RemoveReportedCommentFromReport(int commentId)
        {
            var comment = context.Reports.Find(commentId);
            context.Reports.Remove(comment);
            context.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
    }
}