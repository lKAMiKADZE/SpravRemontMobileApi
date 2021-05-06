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
    public class LoadFiltrUslugController : ApiController
    {
        // GET api/ItemsShop
        public string Get([FromUri]RequestLoadFiltrUslugs req)
        {
            string json = "";

            try
            {
                ResponseLoadFiltrUslug resp = new ResponseLoadFiltrUslug();
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
