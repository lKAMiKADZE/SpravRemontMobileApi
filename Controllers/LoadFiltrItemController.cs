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
    public class LoadFiltrItemController : ControllerBase
    {
        // GET api/ItemsShop
        public string Get([FromQuery]RequestLoadFiltrShops req)
        {
            string json = "";

            try
            {
                ResponseLoadFiltrItem resp = new ResponseLoadFiltrItem();
                resp.GetLoadFiltr(Constants.connectDB, req);
                json = JsonConvert.SerializeObject(resp);
                              
            }
            catch (Exception ex)
            {
                json += " err " + ex.Message;
            }


            return json;
        }


    }
}
