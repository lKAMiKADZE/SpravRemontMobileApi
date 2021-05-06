using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app20193Service.DataObjects
{
    public class Messages
    {
        public string ID_message { get; set; }
        public string ID_from { get; set; }
        public string ID_to { get; set; }
        public string Message { get; set; }
        public byte ID_type_message { get; set; }
        public DateTime Date_send { get; set; }
        public bool new_message { get; set; }

        //public string UserName { get; set; }



            


    }
}