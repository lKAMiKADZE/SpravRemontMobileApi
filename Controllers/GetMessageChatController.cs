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
    public class GetMessageChatController : ApiController
    {
        // GET api/ItemsShop
        public string Get([FromUri]RequestGetMessageChat req)
        {
            string json = "";                       
            
            try
            {
                ResponseGetMessageChat resp = new ResponseGetMessageChat();
                resp.GetLastMessage(Constants.connectDB, req);
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
