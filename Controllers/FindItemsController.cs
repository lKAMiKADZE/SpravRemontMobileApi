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
    public class FindItemsController : ControllerBase
    {
        // GET api/ItemsShop
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


        public string Post([Microsoft.AspNetCore.Mvc.FromBody]RequestFindItem req)
        {
            string json = "";

            try
            {
               
                ResponseFindItems resp = new ResponseFindItems();
                resp.GetFindItems(Constants.connectDB, req);
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
