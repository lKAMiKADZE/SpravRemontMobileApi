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
