using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SpravRemontMobileApi.ModelControllers.Request;

using SpravRemontMobileApi.DataObjects;
using Newtonsoft.Json;

namespace SpravRemontMobileApi.ModelControllers.Response
{
    public class ResponseLoadFiltrItem
    {

        public List<Metro> Metros { get; set; }
        public List<Metro> MetroLines { get; set; }
        public List<KATEGOR> KATEGORs { get; set; }
        public  bool Buy_card { get; set; }
        public int TimeWayMetro { get; set; }
       // public List<TYPE_SHOP> TYPE_SHOPs { get; set; }

        public  void GetLoadFiltr(string connectionString, RequestLoadFiltrShops req)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Metros = new List<Metro>();
                MetroLines = new List<Metro>();
                KATEGORs = new List<KATEGOR>();
               // TYPE_SHOPs = new List<TYPE_SHOP>();
                Buy_card = false;
                TimeWayMetro = 0;


                string sqlExpression = "";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                SqlDataReader reader;



                #region get kategor

                sqlExpression = @"
                   SELECT 
                    ID_KATEGOR, 
                    NAME_KATEGOR 
                    
                    FROM SPAVREMONT.KATEGOR

                    ";

                command.CommandText = sqlExpression;
                reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    int ID_KATEGOR_Index = reader.GetOrdinal("ID_KATEGOR");
                    int NAME_KATEGOR_Index = reader.GetOrdinal("NAME_KATEGOR");

                    while (reader.Read()) // построчно считываем данные
                    {
                        KATEGOR item = new KATEGOR
                        {
                            ID_KATEGOR = reader.GetString(ID_KATEGOR_Index),
                            NAME_KATEGOR = reader.GetString(NAME_KATEGOR_Index)
                        };

                        KATEGORs.Add(item);
                    }

                }

                reader.Close();

                #endregion           

               


            }//return shops;
        }

    }

    

}