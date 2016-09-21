using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drakflygaren.Models
{
    public class TopicComment : TopicAction
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsReported { get; set; }

        public DateTime CommentDateTime { get; set; }
        //public string ImageCommentUrl { get; set; } //incase user wishes to comment with an image
    }
}
