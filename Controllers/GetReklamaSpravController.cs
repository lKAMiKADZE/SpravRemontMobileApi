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
    public class GetReklamaSpravController : ControllerBase
    {
        
        public string Get()
        {
            string json = "";
            try
            {
                ResponseGetReklamaSprav resp = new ResponseGetReklamaSprav();
                resp.GetReklamaSprav(Constants.connectDB);
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