using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

using SpravRemontMobileApi.ModelControllers.Request;

using SpravRemontMobileApi.DataObjects;
using Newtonsoft.Json;
using SpravRemontMobileApi.Constant;

namespace SpravRemontMobileApi.ModelControllers.Response
{
    public class ResponseLoadFiltrShops
    {

        public List<Metro> Metros { get; set; }
        public List<Metro> MetroLines { get; set; }
        public List<KATEGOR> KATEGORs { get; set; }
        public  bool Buy_card { get; set; }
        public int TimeWayMetro { get; set; }
        
        public bool DISCONT_CARD { get; set; }

        public List<City> Cities { get; set; }

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
                DISCONT_CARD = false;


                string sqlExpression = "";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                #region get metro

                sqlExpression = @"
                    SELECT 
                        m.ID_metro,
                        m.Name_line,
                        m.Station,
                        m.ID_Geo,
                        m.Color_Hex,
                        m.ID_City
                    
                     FROM  SPAVREMONT.METRO m
                     JOIN SPAVREMONT.SHOP sh ON m.ID_metro=sh.ID_metro
                     WHERE m.ID_City='" + req.ID_City + @"'
                        AND sh.id_type_shop in ('340eb5f2-0ffd-411b-9cf2-318a60b22604','350eb5f2-0ffd-411b-9cf2-318a60b22604')
                     GROUP BY 
                        m.ID_metro,
                        m.Name_line,
                        m.Station,
                        m.ID_Geo,
                        m.Color_Hex,
                        m.ID_City
                     ORDER BY m.Station ASC

                    ";

                command.CommandText = sqlExpression;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    //int genreIDIndex = reader.GetOrdinal("GenreID");
                    //...
                    //while...
                    //GenreID = reader.IsDBNull(genreIDIndex) ? null : reader.GetInt32(genreIDIndex)
                    int ID_metro_Index = reader.GetOrdinal("ID_metro");
                    int Name_line_Index = reader.GetOrdinal("Name_line");
                    int Station_Index = reader.GetOrdinal("Station");
                  //  int ID_Geo_Index = reader.GetOrdinal("ID_Geo");
                    int Color_Hex_Index = reader.GetOrdinal("Color_Hex");
                   // int ID_city_Index = reader.GetOrdinal("ID_City");


                    while (reader.Read()) // построчно считываем данные
                    {
                        Metro item = new Metro
                        {
                            ID_metro = reader.GetString(ID_metro_Index),
                            Name_line= reader.GetString(Name_line_Index),
                            Station= reader.GetString(Station_Index),
                            Color_Hex= reader.GetString(Color_Hex_Index)
                        };

                        Metros.Add(item);
                    }

                }

                reader.Close();

                #endregion
                
                #region get metroLine

                sqlExpression = @"
                    SELECT 
                        m.Name_line,
                        m.Color_Hex
                    
                     FROM  SPAVREMONT.METRO m
                     JOIN SPAVREMONT.SHOP sh ON m.ID_metro=sh.ID_metro
                     WHERE m.ID_City='" + req.ID_City + @"'
                        AND sh.id_type_shop in ('340eb5f2-0ffd-411b-9cf2-318a60b22604','350eb5f2-0ffd-411b-9cf2-318a60b22604')
                     GROUP BY 
                        m.Name_line,
                        m.Color_Hex
                     ORDER BY m.Name_line ASC


                    ";

                command.CommandText = sqlExpression;
                reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    
                    int Name_line_Index = reader.GetOrdinal("Name_line");
                    int Color_Hex_Index = reader.GetOrdinal("Color_Hex");


                    int tmp_id = 0;
                    while (reader.Read()) // построчно считываем данные
                    {
                        tmp_id++;

                        Metro item = new Metro
                        {
                            ID_metro = tmp_id.ToString(),
                            Station = reader.GetString(Name_line_Index),                         
                            Color_Hex = reader.GetString(Color_Hex_Index)
                        };

                        MetroLines.Add(item);
                    }

                }

                reader.Close();

                #endregion

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
                            NAME_KATEGOR= reader.GetString(NAME_KATEGOR_Index)
                        };

                        KATEGORs.Add(item);
                    }

                }

                reader.Close();

                #endregion
                

                #region //get type_shop
                /*
                sqlExpression = @"
                   SELECT 
                    ID_TYPE_SHOP,
                    NAME_TYPE
                 FROM SPAVREMONT.Type_shop
                 WHERE ID_TYPE_SHOP in (
                '340eb5f2-0ffd-411b-9cf2-318a60b22604',
                '350eb5f2-0ffd-411b-9cf2-318a60b22604'
                 )

                    ";

                command.CommandText = sqlExpression;
                reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    int ID_TYPE_SHOP_Index = reader.GetOrdinal("ID_TYPE_SHOP");
                    int NAME_TYPE_Index = reader.GetOrdinal("NAME_TYPE");

                    while (reader.Read()) // построчно считываем данные
                    {
                        TYPE_SHOP item = new TYPE_SHOP
                        {
                            ID_TYPE_SHOP = reader.GetString(ID_TYPE_SHOP_Index),
                            NAME_TYPE = reader.GetString(NAME_TYPE_Index)
                        };

                        TYPE_SHOPs.Add(item);
                    }

                }

                reader.Close();

                */
                #endregion 


            }

            Cities = City.GetCities();

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