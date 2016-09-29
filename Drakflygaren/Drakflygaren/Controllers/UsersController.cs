using AutoMapper;
using Drakflygaren.Models;
using Drakflygaren.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Drakflygaren.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Details(string username)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == username);

            return View(user);
        }

        public JsonResult GetUserDetails(string username)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == username);

            return Json(new
            {
                userName = user.UserName,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                dateTimeRegistered = user.DateTimeRegistered,
                imageUrl = user.ImageUrl,
                likedEvents = user.LikedEvents.Count,
                likedTopics = user.LikedTopics.Count,
                favoriteLocations = user.FavoriteLocations.Count,
                joinedEvents = user.JoinedEvents.Count,
                writtenComments = user.WrittenComments.Count,
                topicsCreated = user.TopicsCreated.Count,
                ratedLocations = user.RatedLocations.Count,
                isUser = User.Identity.Name == user.UserName,
                isAdmin = User.Identity.Name == user.UserName && User.IsInRole("Admin")

            }, JsonRequestBehavior.AllowGet);
        }

        public void SaveProfile(string firstName, string lastName, string imageUrl)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.ImageUrl = imageUrl;

            db.SaveChanges();
        }
    }
}