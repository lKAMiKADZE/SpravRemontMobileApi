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
    public class ResponseLoadFiltrUslug
    {
        
        public List<USLUG> Uslugs { get; set; }

        public List<Metro> Metros { get; set; }
        public List<Metro> MetroLines { get; set; }        
        public bool Buy_card { get; set; }
        public int TimeWayMetro { get; set; }
        public List<City> Cities { get; set; }

        public  void GetLoadFiltr(string connectionString, RequestLoadFiltrUslugs req)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Uslugs = new List<USLUG>();
                Metros = new List<Metro>();
                MetroLines = new List<Metro>();

                string sqlExpression = "";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;                
                //SqlDataReader reader;
                
                #region get USLUG

                sqlExpression = @"
                   SELECT     
                     ID_USLUG,
                     NAME_USLUG,
                     NOTE_USLUG,
                     IMG_URL
                     FROM SPAVREMONT.USLUG 

                    ";

                command.CommandText = sqlExpression;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                   int ID_USLUG_Index = reader.GetOrdinal("ID_USLUG");
                    int NAME_USLUG_Index    = reader.GetOrdinal("NAME_USLUG");
                   int NOTE_USLUG_Index     = reader.GetOrdinal("NOTE_USLUG");
                   int IMG_URL_Index = reader.GetOrdinal("IMG_URL");

                    while (reader.Read()) // построчно считываем данные
                    {
                        USLUG item = new USLUG
                        {
                            ID_USLUG= reader.GetString(ID_USLUG_Index),
                            NAME_USLUG= reader.GetString(NAME_USLUG_Index),
                            NOTE_USLUG = reader.GetString(NOTE_USLUG_Index),
                            IMG_URL=reader.GetString(IMG_URL_Index)
                        };

                        Uslugs.Add(item);
                    }

                }

                reader.Close();

                #endregion

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
                        AND sh.id_type_shop = '360eb5f2-0ffd-411b-9cf2-318a60b22604'
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
                reader = command.ExecuteReader();

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
                            Name_line = reader.GetString(Name_line_Index),
                            Station = reader.GetString(Station_Index),
                            Color_Hex = reader.GetString(Color_Hex_Index)
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
                        AND sh.id_type_shop = '360eb5f2-0ffd-411b-9cf2-318a60b22604'
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

                // reader.Close();

                #endregion



            }//END using

            Cities = City.GetCities();

        }


    }



}