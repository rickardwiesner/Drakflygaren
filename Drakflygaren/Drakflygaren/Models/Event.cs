﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime EventDateTime { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public int LocationId { get; set; }
        public int EventCategoryId { get; set; }

        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }

        //Navigation Properties

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        [ForeignKey("EventCategoryId")]
        public virtual EventCategory Category { get; set; }

        public virtual IList<EventParticipant> Participants { get; set; }
        public virtual IList<EventComment> EventComments { get; set; }
        public virtual IList<EventLike> EventLikes { get; set; }
    }
}