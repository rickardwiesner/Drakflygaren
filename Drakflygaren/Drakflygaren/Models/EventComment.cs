using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class EventComment : EventAction
    {
        public int Id { get; set; }
        public string Comment { get; set; }
    }
}