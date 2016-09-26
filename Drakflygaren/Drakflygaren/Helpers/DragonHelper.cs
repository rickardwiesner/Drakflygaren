using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Helpers
{
    public class DragonHelper
    {
        static public string GetTimeSpan(DateTime lastPost)
        {
            if ((DateTime.Now.Subtract(lastPost.TimeOfDay).Hour < 1) && (DateTime.Now.Subtract(lastPost.Date).Days < 1))
            {
                return string.Format("{0}{1}", DateTime.Now.Subtract(lastPost.TimeOfDay).Minute, " m sedan");
            }

            else if ((DateTime.Now.Subtract(lastPost.TimeOfDay).Hour < 24) && (DateTime.Now.Subtract(lastPost.Date).Days < 1))
            {
                return string.Format("{0}{1}", DateTime.Now.Subtract(lastPost.TimeOfDay).Hour, " h sedan");

            }

            else if (DateTime.Now.Subtract(lastPost.Date).Days < 7)
            {
                return string.Format("{0}{1}", DateTime.Now.Subtract(lastPost.Date).Days, " d sedan");
            }

            else
            {
                return string.Format("{0}{1}", DateTime.Now.Subtract(lastPost.Date).Days / 7, " v sedan");
            }
        }
    }
}