﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class Forum : Topic
    {
        public int ForumId { get; set; }
        public virtual IList<Topic> Topics { get; set; }   
    }
}