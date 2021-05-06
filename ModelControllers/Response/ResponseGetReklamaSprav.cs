using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SpravRemontMobileApi.DataObjects;
using Newtonsoft.Json;

namespace SpravRemontMobileApi.ModelControllers.Response
{
    public class ResponseGetReklamaSprav
    {
        
        public Reklama Reklama { get; set; }
                

        public  void GetReklamaSprav(string connectionString)
        {
            Reklama = new Reklama();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();                
                command.Connection = connection;
                
                #region sql reklama

                string sqlExpression = @"
                    SELECT 
                ID_Reklama,
                IMG_URL 
                 FROM SPAVREMONT.Reklama
                    WHERE ID_Reklama='4d0eb5f2-0ffd-411b-9cf2-318a60b22604' -- реклама, раздел справочник
                ";


                #endregion


                command.CommandText = sqlExpression;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int sID_ReklamaIndex = reader.GetOrdinal("ID_Reklama");
                    int sIMG_URLIndex = reader.GetOrdinal("IMG_URL");
                    
                    while (reader.Read()) // построчно считываем данные
                    {
                        Reklama.ID_Reklama = reader.GetString(sID_ReklamaIndex);
                        Reklama.IMG_URL = reader.GetString(sIMG_URLIndex);
                    }
                }

                reader.Close();

            }// end using

           
        }

        




    }
}