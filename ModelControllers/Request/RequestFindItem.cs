using SpravRemontMobileApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.ModelControllers.Request
{
    public class RequestFindItem
    {
        public string action { get; set; }
        public ITEM  item { get; set; }
        public int type_shop_012 { get; set; } // 0 все 1 магазины 2 разборки
        public List<Metro> metro { get; set; }
        public bool buy_card { get; set; }
        public int TimeWayMetro { get; set; }
        public Geo geoPhone { get; set; }
               
        public bool AllMetro { get; set; }

        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }


        public string ID_City { get; set; }

    }
}