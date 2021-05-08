using System;
using System.Web.Http;
using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.ModelControllers;
using SpravRemontMobileApi.ModelControllers.Request;
using SpravRemontMobileApi.ModelControllers.Response;
using Newtonsoft.Json;
//using Microsoft.AspNetCore.Mvc;

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
