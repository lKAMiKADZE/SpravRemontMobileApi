using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SpravRemontMobileApi.DataObjects;

namespace SpravRemontMobileApi.ModelControllers
{
    public class ResponseGetCity
    {
       public List<City> Cities { get; set; }



        public void GetCities(string connectionString)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Cities = new List<City>();

                string sqlExpression = @"SELECT ID_City, NAME_city FROM SPAVREMONT.City    ORDER BY NAME_city ASC                  
                     
                        ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    //int genreIDIndex = reader.GetOrdinal("GenreID");
                    //...
                    //while...
                    //GenreID = reader.IsDBNull(genreIDIndex) ? null : reader.GetInt32(genreIDIndex)



                    int ID_City_index = reader.GetOrdinal("ID_City");
                    int NAME_city_index = reader.GetOrdinal("NAME_city");


                    while (reader.Read()) // построчно считываем данные
                    {
                        Cities.Add(new City()
                        {
                            ID_City = reader.GetString(ID_City_index),
                            NAME_city = reader.GetString(NAME_city_index)                            
                        });

                        
                    }
                }




                //return shops;
            }
        }
    }
}