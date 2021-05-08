using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.ModelControllers.Response;
using SpravRemontMobileApi.ModelControllers.Request;
using Microsoft.AspNetCore.Mvc;
using SpravRemontMobileApi.ModelControllers;

namespace SpravRemontMobileApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FindShopsController : ControllerBase
    {
        // GET api/
        public string Get([FromQuery]RequestShopClient req)
        {
            string json = "Empty";

            try
            {
                              
            }
            catch (Exception ex)
            {
                json += " err " + ex.Message;
            }


            return json;
        }

        [HttpPost]
        public string Post([FromBody]RequestFindShops req)
        {
            string json = "";

            try
            {
                ResponseFindShops resp = new ResponseFindShops();
                resp.GetFindShops(Constants.connectDB, req);
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
