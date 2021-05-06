using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using SpravRemontMobileApi.Constant;


using SpravRemontMobileApi.ModelControllers.Response;
using SpravRemontMobileApi.ModelControllers.Request;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace SpravRemontMobileApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class ClientCommentController : ApiController
    {


        // GET api/<controller>
        public string Get([FromUri]RequestClientComment req)
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
        public string Post([Microsoft.AspNetCore.Mvc.FromBody]RequestClientComment req)
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

        // PUT api/<controller>/5
        public void Put(int id, [Microsoft.AspNetCore.Mvc.FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}