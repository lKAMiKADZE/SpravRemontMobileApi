using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server;
using System.Web.Http.Tracing;
using System.Web;
using app20193Service.ModelControllers;
using System.Data.SqlClient;
using Newtonsoft.Json;
using app20193Service.Constant;


using app20193Service.Lib;
using System.Text;
using app20193Service.DataObjects;
using app20193Service.ModelControllers.Response;
using System.Runtime.InteropServices;

namespace app20193Service.Controllers
{
    [MobileAppController]
    public class GetReklamaSpravController : ApiController
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