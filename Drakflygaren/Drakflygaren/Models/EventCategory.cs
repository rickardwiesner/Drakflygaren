using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Drakflygaren.Models
{
    public class EventCategory
    {
        public int Id { get; set; }
        [DisplayName("Typ")]
        public string CategoryName { get; set; }
    }
}