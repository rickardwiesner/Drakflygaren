using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;

namespace Drakflygaren.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateTimeRegistered { get; set; } = DateTime.Now;

        public string ImageUrl { get; set; } = "https://www.drupal.org/files/profile_default.jpg";

        public virtual ICollection<EventLike> LikedEvents { get; set; }

        public virtual ICollection<TopicLike> LikedTopics { get; set; }

        public virtual ICollection<LocationFavorite> FavoriteLocations { get; set; }

        public virtual ICollection<EventParticipant> JoinedEvents { get; set; }

        public virtual ICollection<TopicComment> WrittenComments { get; set; }

        public virtual ICollection<Topic> TopicsCreated { get; set; }

        public virtual ICollection<LocationRating> RatedLocations { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<TopicComment> TopicComments { get; set; }
        public DbSet<TopicView> TopicViews { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicLike> TopicLikes { get; set; }
        public DbSet<LocationRating> LocationRatings { get; set; }
        public DbSet<LocationFavorite> LocationFavorites { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventLike> EventLikes { get; set; }
        public DbSet<EventComment> EventComments { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Report> Reports { get; set; }

    }
}