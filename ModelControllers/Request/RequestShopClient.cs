using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.ModelControllers
{
    public enum GetActionRequest { Shop, Shops, Kategorys, ItemsShop }
    
    public class RequestShopClient
    {
        public string action { get; set; }        
        public string id_shop { get; set; }
        public string id_kategor { get; set; }
    }
}