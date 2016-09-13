using Drakflygaren.Models;
using Drakflygaren.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
                locationViewModels.Add(new LocationViewModel { Location = location, Favorite = db.FavoriteLocations.Any(fl => fl.UserId == userId && fl.LocationId == location.Id) });
            }

            return View(locationViewModels);
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

        [HttpPost]
        public void ToggleFavorite(int locationId)
        {
            var userId = User.Identity.GetUserId();
            var favoriteLocation = db.FavoriteLocations.FirstOrDefault(fl => fl.UserId == userId && fl.LocationId == locationId);

            if (favoriteLocation == null)
            {
                db.FavoriteLocations.Add(new FavoriteLocation { UserId = userId, LocationId = locationId });
            }

            else
            {
                db.FavoriteLocations.Remove(favoriteLocation);
            }

            db.SaveChanges();
        }
    }
}
