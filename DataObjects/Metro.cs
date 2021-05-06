using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace app20193Service.DataObjects
{
    public class Metro
    {
        public string ID_metro { get; set; }
        public string Name_line { get; set; }
        public string Station { get; set; }
        public Geo    Geo { get; set; }
        public string Color_Hex { get; set; }
        public string ID_City { get; set; }



    }
}