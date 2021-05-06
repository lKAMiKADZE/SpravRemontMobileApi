using System;
using System.Web.Http;
using app20193Service.Constant;
using app20193Service.ModelControllers;
using app20193Service.ModelControllers.Request;
using app20193Service.ModelControllers.Response;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;

namespace app20193Service.Controllers
{
    [MobileAppController]
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
