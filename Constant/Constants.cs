using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.Constant
{
    public static class Constants
    {
        // Replace strings with your Azure Mobile App endpoint.
        //private const string testURL = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-app20193Service-20190103064147.mdf;Initial Catalog = aspnet - app20193Service - 20190103064147; Integrated Security = True; MultipleActiveResultSets=True";
        //private const string prodURL = @"Data Source=tcp:appdev2019.database.windows.net,1433;Initial Catalog=BDdevrus;User ID=kamikadze;Password=13Kami580";
        
        private const string prodURL =   @"Data Source=WIN-WIHR5N9L3YD; Initial Catalog = BDSpravremont; User ID=zenin; Password=z5008706z";
        

        public  const string connectDB = prodURL;

        public const string RESULT_OK = "OK";
        public const string RESULT_ERR = "ERROR";

        //public const string Host = "https://app20193.azurewebsites.net"; 
        public const string Host = "http://masterclub-aap.ru:8011";

    }
}