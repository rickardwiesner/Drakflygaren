using Drakflygaren.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string text { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser Reporter { get; set; }

        public int CommentId { get; set; }
        public virtual TopicComment CommentToReport { get; set; }

        public ReportCategory Category { get; set; }
    }
}