using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime CommentDateTime { get; set; }
        //public string ImageCommentUrl { get; set; } //incase user wishes to comment with an image

        //Foreign keys
        public string UserId { get; set; }
        public int TopicId { get; set; }


        //Navigation properties

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }

    }
}