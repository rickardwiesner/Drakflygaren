using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }

        //Foreign key
        public string UserId { get; set; }

        //Navigational Prop

        [ForeignKey("UserId")]
        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual IList<Comment> Comments { get; set; }

    }
}