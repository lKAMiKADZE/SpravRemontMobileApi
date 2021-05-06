using System;
using System.Web.Http;
using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.ModelControllers;
using SpravRemontMobileApi.ModelControllers.Request;
using SpravRemontMobileApi.ModelControllers.Response;
using Newtonsoft.Json;

namespace SpravRemontMobileApi.Controllers
{
    //[ApiController]
    [Route("[controller]")]
    public class LoadFiltrShopsController : ApiController // NOT ACTIVE
    {
        // GET api/ItemsShop
        public string Get([FromUri]RequestLoadFiltrShops req)
        {
            string json = "";

            try
            {
                ResponseLoadFiltrShops resp = new ResponseLoadFiltrShops();
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
