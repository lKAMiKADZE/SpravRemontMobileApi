using SpravRemontMobileApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.ModelControllers.Request
{
    public class RequestFindUslug
    {
        public string ID_uslug { get; set; }
        public string ID_City { get; set; }

        public bool AllMetro { get; set; }
        public List<Metro> metro { get; set; }
        public bool buy_card { get; set; }
        public int TimeWayMetro { get; set; }
    }
}