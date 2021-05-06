using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.DataObjects;
using Newtonsoft.Json;

namespace SpravRemontMobileApi.ModelControllers.Response
{
    public class ResponseShopClient
    {
        [JsonProperty("Shops")]
        public List<Shop> Shops { get; set; }


        public ResponseShopClient()
        {
           // Shops = new List<Shop>();
        }

        public  void GetShops(string connectionString)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Shops = new List<Shop>();

                string sqlExpression = @"SELECT 
                    s.ID_shop,
                    s.Type,
                    s.Name,
                    s.Phone,
                    s.Site,
                    s.Adress,
                    m.ID_Metro,
                    m.station,
                    m.Color_hex,
                    (
                        SELECT sum(CC.Count_star) AS avg_star FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=S.ID_shop
                            AND CC.visible=1
                    ) AS AVG_Star,
                    (
                        SELECT count(*) AS count_feedback FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=S.ID_shop
                            AND CC.visible=1
                    ) AS Count_feedback
                    
                    FROM SPAVREMONT.Shop s
                    JOIN SPAVREMONT.Metro m ON s.ID_Metro=m.ID_Metro
                    WHERE s.Type='Магазин' 
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
                    int sID_shopIndex = reader.GetOrdinal("ID_shop");
                    int sTypeIndex = reader.GetOrdinal("Type");
                    int sNameIndex       = reader.GetOrdinal("Name");
                    int sPhoneIndex      =  reader.GetOrdinal("Phone");
                    int sSiteIndex       =  reader.GetOrdinal("Site");
                    int sAdressIndex     =  reader.GetOrdinal("Adress");
                    int mID_MetroIndex   =  reader.GetOrdinal("ID_Metro");
                    int mstationIndex    =  reader.GetOrdinal("station");
                    int mColor_hexIndex = reader.GetOrdinal("Color_hex");
                    int AVG_StarIndex = reader.GetOrdinal("AVG_Star");
                    int Count_feedbackIndex = reader.GetOrdinal("Count_feedback");

                    while (reader.Read()) // построчно считываем данные
                    {
                        Metro metro = new Metro
                        {
                            ID_metro = reader.GetString(mID_MetroIndex),
                            Station = reader.GetString(mstationIndex),
                            Color_Hex = reader.GetString(mColor_hexIndex)                            
                        };
                        
                        // подсчет среднего кол-во звезд
                        double _avg_star = reader.IsDBNull(AVG_StarIndex) ? 0 : (double)reader.GetInt32(AVG_StarIndex);
                        int _Count_feedback = reader.IsDBNull(Count_feedbackIndex) ? 0 : reader.GetInt32(Count_feedbackIndex);

                        int tmp = _Count_feedback;
                        if (_Count_feedback == 0)
                            tmp = 1;
                        _avg_star = _avg_star / tmp;// округление до2  знаков


                        Shop item = new Shop
                        {
                            ID_shop = reader.GetString(sID_shopIndex),
                            Type = reader.GetString(sTypeIndex),
                            Name = reader.GetString(sNameIndex),
                            Phone = reader.GetString(sPhoneIndex),
                            Adress = reader.GetString(sAdressIndex),
                            Metro = metro,                            
                            Site = reader.IsDBNull(sSiteIndex) ? "" : reader.GetString(sSiteIndex),
                            AVG_Star = Math.Round(_avg_star, 1),
                            Count_feedback = _Count_feedback

                        };
                        Shops.Add(item);
                    }
                }




                //return shops;
            }
        }

        /*

        private void _GetShops(string connectionString)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Shops = new List<Shop>();

                string sqlExpression = @"SELECT 
                    s.ID_shop,
                    s.Type,
                    s.Name,
                    s.Phone,
                    s.Site,
                    s.Adress,
                    m.ID_Metro,
                    m.station,
                    m.Color_hex,
                    (
                        SELECT sum(CC.Count_star) AS avg_star FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=S.ID_shop
                            AND CC.visible=1
                    ) AS AVG_Star,
                    (
                        SELECT count(*) AS count_feedback FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=S.ID_shop
                            AND CC.visible=1
                    ) AS Count_feedback
                    
                    FROM SPAVREMONT.Shop s
                    JOIN SPAVREMONT.Metro m ON s.ID_Metro=m.ID_Metro
                    WHERE s.Type='Магазин' 
                    ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

            


                while (reader.Read()) // построчно считываем данные
                {
                    Metro metro = new Metro
                    {
                        ID_metro = reader.GetString(mID_MetroIndex),
                        Station = reader.GetString(mstationIndex),
                        Color_Hex = reader.GetString(mColor_hexIndex)
                    };

                    // подсчет среднего кол-во звезд
                    double _avg_star = reader.IsDBNull(AVG_StarIndex) ? 0 : (double)reader.GetInt32(AVG_StarIndex);
                    int _Count_feedback = reader.IsDBNull(Count_feedbackIndex) ? 0 : reader.GetInt32(Count_feedbackIndex);

                    int tmp = _Count_feedback;
                    if (_Count_feedback == 0)
                        tmp = 1;
                    _avg_star = _avg_star / tmp;// округление до2  знаков


                    Shop item = new Shop
                    {
                        ID_shop = reader.GetString(sID_shopIndex),
                        Type = reader.GetString(sTypeIndex),
                        Name = reader.GetString(sNameIndex),
                        Phone = reader.GetString(sPhoneIndex),
                        Adress = reader.GetString(sAdressIndex),
                        Metro = metro,
                        Site = reader.IsDBNull(sSiteIndex) ? "" : reader.GetString(sSiteIndex),
                        AVG_Star = Math.Round(_avg_star, 1),
                        Count_feedback = _Count_feedback

                    };
                    Shops.Add(item);
                }
                
                //return shops;
            }

            SqlParameter[] parameters = new SqlParameter[]
        {
                new SqlParameter(@"ID_SHOP",SqlDbType.NVarChar) { Value =ID_SHOP }
        };

            #region sql

            string sqlText = $@"
SELECT 
                    s.ID_shop,
                    s.Type,
                    s.Name,
                    s.Phone,
                    s.Site,
                    s.Adress,
                    m.ID_Metro,
                    m.station,
                    m.Color_hex,
                    (
                        SELECT sum(CC.Count_star) AS avg_star FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=S.ID_shop
                            AND CC.visible=1
                    ) AS AVG_Star,
                    (
                        SELECT count(*) AS count_feedback FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=S.ID_shop
                            AND CC.visible=1
                    ) AS Count_feedback
                    
                    FROM SPAVREMONT.Shop s
                    JOIN SPAVREMONT.Metro m ON s.ID_Metro=m.ID_Metro
                    WHERE s.Type='Магазин' 

";

            #endregion

            DataTable dt = new DataTable();// при наличии данных
                                           // получаем данные из запроса
            dt = ExecuteSqlGetDataTableStatic(sqlText, parameters);


            foreach (DataRow row in dt.Rows)
            {

                // попали в цикл, значит авторизовались, т.к. такой пользователь существует
                IMG_Shop img = new IMG_Shop
                {
                    Date_add = (DateTime)row["Date_add"],
                    Deleted = (bool)row["Deleted"],
                    ID_IMG_Shop = (long)row["ID_IMG_Shop"],
                    ID_SHOP = (string)row["ID_SHOP"],
                    Type = (string)row["Type"],
                    Url = (string)row["Url"]
                };

                iMG_Shops.Add(img);
            }

        }

        */


        private void GetShopIMGs(string ID_SHOP)
        {
            List<IMG_Shop> iMG_Shops = new List<IMG_Shop>();


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"ID_SHOP",SqlDbType.NVarChar) { Value =ID_SHOP }
            };

            #region sql

            string sqlText = $@"
SELECT TOP(10)
       [ID_IMG_SHOP]
      ,[ID_SHOP]
      ,[Url]
      ,[Type]
      ,[Deleted]
      ,[Date_add]
  FROM [SPAVREMONT].[IMG_Shop]
  WHERE 1=1
	AND Deleted=0
	AND ID_SHOP=@ID_SHOP
    AND Type='VITRINA'

	ORDER BY Date_add DESC

";

            #endregion

            DataTable dt = new DataTable();// при наличии данных
                                           // получаем данные из запроса
            dt = ExecuteSqlGetDataTableStatic(sqlText, parameters);


            foreach (DataRow row in dt.Rows)
            {

                // попали в цикл, значит авторизовались, т.к. такой пользователь существует
                IMG_Shop img = new IMG_Shop
                {
                    Date_add = (DateTime)row["Date_add"],
                    Deleted = (bool)row["Deleted"],
                    ID_IMG_Shop = (long)row["ID_IMG_Shop"],
                    ID_SHOP = (string)row["ID_SHOP"],
                    Type = (string)row["Type"],
                    Url = (string)row["Url"]
                };

                iMG_Shops.Add(img);
            }


            //return iMG_Shops;
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