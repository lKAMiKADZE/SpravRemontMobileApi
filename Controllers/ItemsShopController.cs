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
    public class ItemsShopController : ApiController
    {
        // GET api/ItemsShop
        public string Get([FromUri]RequestShopClient req)
        {
            string json = "";

            try
            {
                if (req.id_shop != null && req.id_kategor != null && req.action == GetActionRequest.ItemsShop.ToString())
                {
                    ResponseItemsShop resp = new ResponseItemsShop();
                    resp.GetItemsShop(Constants.connectDB, req);
                    json = JsonConvert.SerializeObject(resp); //, Formatting.Indented                    
                }
                else
                if (req.id_shop != null && req.action == GetActionRequest.Kategorys.ToString())
                {
                    ResponseItemsShop resp = new ResponseItemsShop();
                    resp.GetKategory(Constants.connectDB, req);
                    json = JsonConvert.SerializeObject(resp); //, Formatting.Indented  
                }

            }
            catch (Exception ex)
            {
                json += " err " + ex.Message;
            }


            return json;
        }
    }
}
