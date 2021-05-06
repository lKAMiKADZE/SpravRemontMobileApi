using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.DataObjects
{
    public class ITEMS_SHOP
    {
        public string ID_ITEMS_SHOP { get; set; }
        public string ID_SHOP { get; set; }
        public ITEM_KATEGOR ITEM_KATEGOR { get; set; }
        public int MIN_PRICE { get; set; }
        public int MAX_PRICE { get; set; }

    }
}