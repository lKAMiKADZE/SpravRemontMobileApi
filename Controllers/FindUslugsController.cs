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
    public class FindUslugsController : ApiController
    {
        // GET api/
        public string Get([FromUri]RequestFindUslug req)
        {
            string json = "Empty";

            try
            {
                //ResponseFindUslugs resp = new ResponseFindUslugs();
                //resp.GetFindUslugs(Constants.connectDB, req);
                //json = JsonConvert.SerializeObject(resp);

            }
            catch (Exception ex)
            {
                json += " err " + ex.Message;
            }


            return json;
        }

        public string Post([FromBody]RequestFindUslug req)
        {
            string json = "";

            try
            {
                ResponseFindUslugs resp = new ResponseFindUslugs();
                resp.GetFindUslugs(Constants.connectDB, req);
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
