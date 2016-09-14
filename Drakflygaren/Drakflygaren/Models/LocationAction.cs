using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class LocationAction
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}