using SpravRemontMobileApi.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.DataObjects
{
    public class City
    {
        public string ID_City { get; set; }
        public string NAME_city { get; set; }





        public static List<City> GetCities()
        {
            List<City> Cities = new List<City>();


            SqlParameter[] parameters = new SqlParameter[]
            {
            };

            #region sql

            string sqlText = $@"
SELECT 
    ID_City,
    NAME_city 
FROM SPAVREMONT.City   

ORDER BY NAME_city ASC

";

            #endregion

            DataTable dt = new DataTable();// при наличии данных
                                           // получаем данные из запроса
            dt = ExecuteSqlGetDataTableStatic(sqlText);


            foreach (DataRow row in dt.Rows)
            {
                // попали в цикл, значит авторизовались, т.к. такой пользователь существует
                City city = new City
                {
                    ID_City = (string)row["ID_City"],
                    NAME_city = (string)row["NAME_city"]
                };

                Cities.Add(city);
            }


            return Cities;
        }


        ///////////////
        // Методы SQL
        ////////////////
        private async static void ExecuteSqlStatic(string sqlText, SqlParameter[] parameters = null)
        {

            using (SqlConnection connection = new SqlConnection(Constants.connectDB))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    // перехват ошибок и выполнение запроса
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception e) { }

                    command.Parameters.Clear();
                }

                connection.Close();
            }
        }

        private static DataTable ExecuteSqlGetDataTableStatic(string sqlText, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(Constants.connectDB))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        dt.Load(reader);
                    }
                    catch (Exception ex)
                    {

                    }

                    command.Parameters.Clear();


                }

                connection.Close();
            }
            return dt;

        }



    }
}