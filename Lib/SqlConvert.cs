using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


using SpravRemontMobileApi.DataObjects;


namespace SpravRemontMobileApi.Lib
{
    public static class SqlConvert
    {
        public static IEnumerable<Dictionary<string, object>> SerializeDictionary(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerializeRowDictionary(cols, reader));

            return results;
        }

        private static Dictionary<string, object> SerializeRowDictionary(IEnumerable<string> cols,
                                                                SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }


       


    }
}