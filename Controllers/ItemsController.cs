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
    public class ItemsController : ApiController
    {
        // GET api/ItemsShop
        public string Get([FromUri]RequestItems req)
        {
            string json = "";

            if (req.id_kategor == null) return "kategor is null";

            try
            {
                ResponseItems resp = new ResponseItems();
                resp.GetItemsKatalog(Constants.connectDB, req);
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
