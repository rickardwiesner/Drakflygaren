using Drakflygaren.Models;
using Drakflygaren.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Drakflygaren.Controllers
{
    [Authorize]
    public class LocationsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var locationViewModels = new List<LocationViewModel>();

            foreach (var location in db.Locations.ToList())
            {
                locationViewModels.Add(GetLocationViewModel(location));               
            }

            return View(locationViewModels);
        }

        [AllowAnonymous]
        public ActionResult Details(int locationId)
        {
            return View(GetLocationViewModel(db.Locations.Find(locationId)));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                db.Locations.Add(new Location
                {
                    Name = model.Name,
                    City = model.City,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    CreatorId = userId
                });

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? locationId)
        {
            if (locationId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(locationId);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,City,Longitude,Latitude,CreatorId")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? locationId)
        {
            if (locationId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(locationId);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int locationId)
        {
            Location location = db.Locations.Find(locationId);
            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LocationFavorites()
        {
            var userId = User.Identity.GetUserId();
            var locationFavorites = db.LocationFavorites.Where(fl => fl.UserId == userId).ToList();

            return PartialView("_LocationFavorites", locationFavorites);
        }

        [HttpPost]
        public ActionResult ToggleFavorite(int locationId)
        {
            var userId = User.Identity.GetUserId();
            var locationFavorite = db.LocationFavorites.FirstOrDefault(fl => fl.UserId == userId && fl.LocationId == locationId);

            if (locationFavorite == null)
            {
                db.LocationFavorites.Add(new LocationFavorite { UserId = userId, LocationId = locationId });
            }

            else
            {
                db.LocationFavorites.Remove(locationFavorite);
            }

            db.SaveChanges();

            var locationFavorites = db.LocationFavorites.Include("Location").Include("User").Where(fl => fl.UserId == userId).ToList();
                
            return PartialView("_LocationFavorites", locationFavorites);
        }

        [HttpPost]
        public JsonResult RateLocation(int locationId, int rating)
        {
            var userId = User.Identity.GetUserId();
            var userLocationRating = db.LocationRatings.FirstOrDefault(lr => lr.LocationId == locationId && lr.UserId == userId);

            if (userLocationRating == null)
            {
                db.LocationRatings.Add(new LocationRating { LocationId = locationId, UserId = userId, Rating = rating });
            }

            else
            {
                if (userLocationRating.Rating == rating)
                {
                    db.LocationRatings.Remove(userLocationRating);
                }

                else
                {
                    userLocationRating.Rating = rating;
                }
            }

            db.SaveChanges();
            var locationRating = db.LocationRatings.Where(lr => lr.LocationId == locationId).Select(lr => lr.Rating).DefaultIfEmpty(0).Average();

            return Json(locationRating);
        }

        LocationViewModel GetLocationViewModel(Location location)
        {
            var userId = User.Identity.GetUserId();
            var userLocationRating = db.LocationRatings.FirstOrDefault(lr => lr.UserId == userId && lr.LocationId == location.Id);

            return new LocationViewModel
            {
                Location = location,
                Favorite = db.LocationFavorites.Any(fl => fl.UserId == userId && fl.LocationId == location.Id),
                Rating = db.LocationRatings.Where(lr => lr.LocationId == location.Id).Select(lr => lr.Rating).DefaultIfEmpty(0).Average(),
                UserRating = userLocationRating != null ? userLocationRating.Rating : 0
            };
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
