using System;
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

        //Navigation Properties

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public virtual IList<EventParticipant> Participants { get; set; }
        public virtual IList<EventComment> EventComments { get; set; }

    }
}