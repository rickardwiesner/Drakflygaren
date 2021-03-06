﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class EventAction
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}