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
    public class LoadFiltrUslugController : ControllerBase
    {
        // GET api/ItemsShop
        public string Get([FromQuery]RequestLoadFiltrUslugs req)
        {
            string json = "";

            try
            {
                ResponseLoadFiltrUslug resp = new ResponseLoadFiltrUslug();
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
