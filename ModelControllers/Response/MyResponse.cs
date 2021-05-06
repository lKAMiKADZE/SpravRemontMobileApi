using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SpravRemontMobileApi.DataObjects;


namespace SpravRemontMobileApi.ModelControllers
{
    public class MyResponse
    {
        public string Action { get; set; }
        public IEnumerable<Dictionary<string, object>> SqlDataDictionary { get; set; }
       // public List<TodoItem> SqlDataTodoItem { get; set; }



    }
}