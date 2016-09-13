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
        [Required]
        public string EventName { get; set; }
        [Required]
        public DateTime EventDateTime { get; set; }
        public string EventImageUrl { get; set; }
        [Required]
        public string EventDesc { get; set; }
        [Required]
        public int EventLocationId { get; set; }
        public bool Attending { get; set; }
        public bool Liked { get; set; }
    }
}