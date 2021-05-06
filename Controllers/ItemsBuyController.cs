using System;
using System.Web.Http;
using app20193Service.Constant;
using app20193Service.ModelControllers;
using app20193Service.ModelControllers.Response;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;

namespace app20193Service.Controllers
{
    [MobileAppController]
    public class ItemsBuyController : ApiController
    {
        // GET 
        public string Get([FromUri]RequestItemsBuy req)
        {
            string json = "";

            if (req.id_items_shop == null || req.id_items_shop=="") return "items is null";

            try
            {
                ResponseItems_buy resp = new ResponseItems_buy();
                resp.GetItemsBuy(Constants.connectDB, req);
                json = JsonConvert.SerializeObject(resp); //, Formatting.Indented                    
                
            }
            catch (Exception ex)
            {
                json += " err " + ex.Message;
            }


            return json;
        }
    }
}
