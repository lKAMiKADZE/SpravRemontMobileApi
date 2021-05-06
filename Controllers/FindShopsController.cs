using System;
using System.Web.Http;
using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.ModelControllers;
using SpravRemontMobileApi.ModelControllers.Request;
using SpravRemontMobileApi.ModelControllers.Response;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace app20193Service.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
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
