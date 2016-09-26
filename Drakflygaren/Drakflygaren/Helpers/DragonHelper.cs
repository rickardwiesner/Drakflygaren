using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drakflygaren.Helpers
{
    public class DragonHelper
    {
        static public string GetTimeSpan(DateTime dateTimeFinished)
        {
            if ((DateTime.Now.Subtract(dateTimeFinished.TimeOfDay).Hour < 1) && (DateTime.Now.Subtract(dateTimeFinished.Date).Days < 1))
            {
                return string.Format("{0}{1}", DateTime.Now.Subtract(dateTimeFinished.TimeOfDay).Minute, " minuter sedan");
            }

            else if ((DateTime.Now.Subtract(dateTimeFinished.TimeOfDay).Hour < 24) && (DateTime.Now.Subtract(dateTimeFinished.Date).Days < 1))
            {
                return string.Format("{0}{1}", DateTime.Now.Subtract(dateTimeFinished.TimeOfDay).Hour, " timmar sedan");

            }

            else if (DateTime.Now.Subtract(dateTimeFinished.Date).Days < 7)
            {
                return string.Format("{0}{1}", DateTime.Now.Subtract(dateTimeFinished.Date).Days, " dagar sedan");
            }

            else
            {
                return string.Format("{0}{1}", DateTime.Now.Subtract(dateTimeFinished.Date).Days / 7, " veckor sedan");
            }
        }
    }
}