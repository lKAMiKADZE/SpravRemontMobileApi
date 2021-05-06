using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.ModelControllers.Request
{
    public class RequestClientComment
    {

        public string ID_shop { get; set; }
             
        public string Email { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Count_star { get; set; }        
    }
}