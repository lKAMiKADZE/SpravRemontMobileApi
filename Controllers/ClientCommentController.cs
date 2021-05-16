using System;

using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.ModelControllers;
using SpravRemontMobileApi.ModelControllers.Request;
using SpravRemontMobileApi.ModelControllers.Response;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;


namespace SpravRemontMobileApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientCommentController : ControllerBase
    {


        // GET api/<controller>
        public string Get([FromQuery]RequestClientComment req)//[FromUri]
        {
            //MobileAppSettingsDictionary settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            //ITraceWriter traceWriter = this.Configuration.Services.GetTraceWriter();
            string json = "";
            string step_err = "";
            try
            {
                if (req.ID_shop != null)
                {
                    step_err += "enter if = ";                   
                    ResponseCommentClient resp = new ResponseCommentClient();                    
                    resp.GetCommentShop(Constants.connectDB, req.ID_shop);
                    step_err += "get comment = ";
                    json = JsonConvert.SerializeObject(resp);
                    step_err += "convert json = ";
                }
            }
            catch(Exception ex)
            {
                json =  step_err+ ex.Message;
            }





                return json;
        }
            
        

        // POST api/<controller>
        public string Post([FromBody]RequestClientComment req)
        {
            //ITraceWriter traceWriter = this.Configuration.Services.GetTraceWriter();
            ResponseCommentClient resp = new ResponseCommentClient();

            string json = "";

            try
            {
                json= resp.SetCommentShop(Constants.connectDB, req);
            }
            catch (Exception ex)
            {
                json += ex.Message;
            }

            return "Спасибо за отзыв!";
            
        }

       
    }
}