using SpravRemontMobileApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.ModelControllers.Request
{
    public class RequestFindShops
    {
        public string action { get; set; }
        public KATEGOR  kategor { get; set; }
        public int type_shop_012 { get; set; } // 0 все 1 магазины 2 разборки
        public List<Metro> metro { get; set; }
        public bool buy_card { get; set; }
        public int TimeWayMetro { get; set; }
        public Geo geoPhone { get; set; }

        public bool AllMetro { get; set; }

        public string ID_City { get; set; }

        public bool DISCONT_CARD { get; set; }
    }
}