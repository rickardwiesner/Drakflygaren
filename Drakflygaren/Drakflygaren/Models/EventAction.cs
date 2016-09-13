using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class EventAction
    {
        public virtual ApplicationUser User { get; set; }
        public virtual Event Event { get; set; }
    }
}