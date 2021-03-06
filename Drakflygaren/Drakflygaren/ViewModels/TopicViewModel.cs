﻿using Drakflygaren.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Drakflygaren.ViewModels
{
    public class TopicViewModel
    {
        public Topic Topic { get; set; }

        public string Text { get; set; }

        public bool Liked { get; set; }
    }
}