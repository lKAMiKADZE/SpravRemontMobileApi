using System;
using System.Web.Http;
using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.ModelControllers;
using SpravRemontMobileApi.ModelControllers.Request;
using SpravRemontMobileApi.ModelControllers.Response;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace SpravRemontMobileApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class ShopClientController : ApiController
    {


        // GET api/<controller>
        public string Get([FromUri]RequestShopClient req)
        {
            //MobileAppSettingsDictionary settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
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