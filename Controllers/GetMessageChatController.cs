using System;

using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.ModelControllers;
using SpravRemontMobileApi.ModelControllers.Request;
using SpravRemontMobileApi.ModelControllers.Response;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace SpravRemontMobileApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetMessageChatController : ControllerBase
    {
        // GET api/ItemsShop
        public string Get([FromQuery]RequestGetMessageChat req)
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
