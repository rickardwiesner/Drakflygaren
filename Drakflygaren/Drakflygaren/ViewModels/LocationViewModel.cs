using Drakflygaren.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drakflygaren.ViewModels
{
    public class LocationViewModel
    {
        //Index
        public Location Location { get; set; }

        public bool Favorite { get; set; }

        public double Rating { get; set; }

        public int UserRating { get; set; }

        //Create
        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name = "Plats")]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Stad")]
        public string City { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }
    }
}
