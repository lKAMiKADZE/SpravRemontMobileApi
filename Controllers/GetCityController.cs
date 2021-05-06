using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using SpravRemontMobileApi.Constant;


using SpravRemontMobileApi.ModelControllers.Response;
using SpravRemontMobileApi.ModelControllers.Request;
//using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace SpravRemontMobileApi.Controllers
{
    //[ApiController]
    [Route("[controller]")]
    public class GetCityController : ApiController
    {
        // GET api/ItemsShop
        public string Get()
        {
            string json = "";
            

            try
            {
                ResponseGetCity resp = new ResponseGetCity();
                resp.GetCities(Constants.connectDB);
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
