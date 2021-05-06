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
    public class ShopClientController : ApiController
    {


        // GET api/<controller>
        public string Get([FromUri]RequestShopClient req)
        {
            MobileAppSettingsDictionary settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            //ITraceWriter traceWriter = this.Configuration.Services.GetTraceWriter();
            string json = "";


            try
            {
                if (req.id_shop == null)
                {
                    ResponseShopClient resp = new ResponseShopClient();
                    resp.GetShops(Constants.connectDB);
                    json = JsonConvert.SerializeObject(resp); //, Formatting.Indented
                }
                else
                {
                    ResponseShopIDClient resp = new ResponseShopIDClient();
                    resp.GetShop(Constants.connectDB, req.id_shop);
                    json = JsonConvert.SerializeObject(resp);
                }
            }
            catch(Exception ex)
            {
                json +=" err "+ ex.Message;
            }
            


            return json;
        }
            
        

       
    }
}