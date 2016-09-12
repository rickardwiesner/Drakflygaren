using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
    }
}