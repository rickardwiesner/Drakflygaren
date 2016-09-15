using Drakflygaren.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drakflygaren.ViewModels
{
    public class EventViewModel
    {
        public Event Event { get; set; }

        public bool Liked { get; set; }
    }
}