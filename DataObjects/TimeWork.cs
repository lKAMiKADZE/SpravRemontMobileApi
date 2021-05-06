using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace app20193Service.DataObjects
{
    public class TimeWork
    {
        public string ID_timeWork { get; set; }
        public DateTime DateAdd { get; set; }
        public string MondayStart { get; set; }//ToShortTimeString
        public string MondayEnd { get; set; }
        public string TuesdayStart { get; set; }
        public string TuesdayEnd { get; set; }
        public string WednesdayStart { get; set; }
        public string WednesdayEnd { get; set; }
        public string ThursdayStart { get; set; }
        public string ThursdayEnd { get; set; }
        public string FridayStart { get; set; }
        public string FridayEnd { get; set; }
        public string SaturdayStart { get; set; }
        public string SaturdayEnd { get; set; }
        public string SundayStart { get; set; }
        public string SundayEnd { get; set; }



    }
}