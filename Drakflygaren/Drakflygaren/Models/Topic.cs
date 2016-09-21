using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class Topic
    {
        public int TopicId { get; set; }

        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Skapad")]
        public DateTime CreatedOn { get; set; }

        //Foreign key
        public string UserId { get; set; }

        public bool Liked { get; set; }
        //Navigational Prop

        [ForeignKey("UserId")]
        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual IList<TopicComment> Comments { get; set; }
        public virtual IList<TopicView> Views { get; set; }
        public virtual IList<TopicLike> TopicLikes { get; set; }

    }
}