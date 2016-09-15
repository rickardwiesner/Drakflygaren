using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drakflygaren.Models
{
    public class TopicAction
    {
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
