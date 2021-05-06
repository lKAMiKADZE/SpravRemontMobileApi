using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.DataObjects
{
    public class ITEM_KATEGOR
    {
        public string ID_ITEM_KATEGOR { get; set; }
        public ITEM ITEM { get; set; }
        public KATEGOR KATEGOR { get; set; }

    }
}