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

        public string Post([Microsoft.AspNetCore.Mvc.FromBody]RequestFindUslug req)
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
