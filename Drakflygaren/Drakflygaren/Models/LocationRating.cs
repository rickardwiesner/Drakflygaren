using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class LocationRating : LocationAction
    {
        public int Id { get; set; }

        public int Rating { get; set; }
    }
}