using System;
using System.Web.Http;
using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.ModelControllers;
using SpravRemontMobileApi.ModelControllers.Request;
using SpravRemontMobileApi.ModelControllers.Response;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace SpravRemontMobileApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
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
