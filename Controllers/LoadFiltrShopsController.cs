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
