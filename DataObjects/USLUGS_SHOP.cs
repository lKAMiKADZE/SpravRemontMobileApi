using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app20193Service.DataObjects
{
    public class USLUGS_SHOP
    {
        public string ID_USLUGS_SHOP { get; set; }
        public string ID_SHOP { get; set; }
        public USLUG Uslug { get; set; }
        public int PRICE { get; set; }

    }
}