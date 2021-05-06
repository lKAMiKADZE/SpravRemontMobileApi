﻿using System;
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
    public class FindShopsController : ApiController
    {
        // GET api/
        public string Get([FromUri]RequestShopClient req)
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


        public string Post([Microsoft.AspNetCore.Mvc.FromBody]RequestFindShops req)
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
