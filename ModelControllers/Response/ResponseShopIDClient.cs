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
    public class ResponseShopIDClient
    {

        [JsonProperty("Shop")]
        public Shop Shop { get; set; }
        public Reklama Reklama { get; set; }

        public List<Shop> OtherShops { get; set; }

        
        public ResponseShopIDClient()
        {
            //Shop = new Shop();
        }

        public  void GetShop(string connectionString, string id_shop)
        {
            OtherShops = new List<Shop>();
            Reklama = new Reklama();
            //Shop.OtherShops = new List<Shop>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                #region sql
                string sqlExpression = @"SELECT
                s.ID_shop,
                s.Type,
                s.Name,
                s.Phone,
                s.Adress,
                s.Email,
                s.Site,
                s.Id_Geo,
                s.DateAdd,
                s.TimeWork,
                s.Note,
                m.ID_Metro,
                m.Station,
                m.Color_hex,
                t.ID_timeWork ,
                t.MondayStart,
                t.MondayEnd ,
                t.TuesdayStart ,
                t.TuesdayEnd ,
                t.WednesdayStart,
                t.WednesdayEnd ,
                t.ThursdayStart ,
                t.ThursdayEnd ,
                t.FridayStart ,
                t.FridayEnd ,
                t.SaturdayStart ,
                t.SaturdayEnd ,
                t.SundayStart ,
                t.SundayEnd,
                (
                    SELECT sum(CC.Count_star) AS avg_star FROM SPAVREMONT.Comment_client CC 
                    WHERE CC.ID_SHOP='" + id_shop + @"'
                        AND CC.visible=1
                ) AS AVG_Star,
                (
                    SELECT count(*) AS count_feedback FROM SPAVREMONT.Comment_client CC 
                    WHERE CC.ID_SHOP='" + id_shop + @"'
                        AND CC.visible=1
                ) AS Count_feedback,

                s.IMG_LOGO,
                s.IMG_DRIVE_TO,
                s.IMG_MAP,

                s.DISCONT_NOTE ,
                s.DISCONT_CARD ,
                s.Descrip

                FROM SPAVREMONT.Shop S

                LEFT JOIN SPAVREMONT.Metro m ON s.ID_Metro=m.ID_Metro
                LEFT JOIN SPAVREMONT.Timework t ON t.ID_timework=s.timework
                WHERE 1=1 
                    AND S.ID_SHOP='" + id_shop + @"'

                    ";

            #endregion


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
                    int sNameIndex = reader.GetOrdinal("Name");
                    int sPhoneIndex = reader.GetOrdinal("Phone");
                    int sAdressIndex = reader.GetOrdinal("Adress");
                    int sEmailIndex = reader.GetOrdinal("Email");
                    int sSiteIndex = reader.GetOrdinal("Site");
                    int sId_GeoIndex = reader.GetOrdinal("Id_Geo");
                    int sDateAddIndex = reader.GetOrdinal("DateAdd");
                    int sTimeWorkIndex = reader.GetOrdinal("TimeWork");
                    int sNoteIndex = reader.GetOrdinal("Note");
                    int mID_MetroIndex = reader.GetOrdinal("ID_Metro");
                    int mStationIndex = reader.GetOrdinal("Station");
                    int mColor_hexIndex = reader.GetOrdinal("Color_hex");
                    int tID_timeWorkIndex = reader.GetOrdinal("ID_timeWork");
                    int tMondayStartIndex = reader.GetOrdinal("MondayStart");
                    int tMondayEndIndex = reader.GetOrdinal("MondayEnd");
                    int tTuesdayStartIndex = reader.GetOrdinal("TuesdayStart");
                    int tTuesdayEndIndex = reader.GetOrdinal("TuesdayEnd");
                    int tWednesdayStartIndex = reader.GetOrdinal("WednesdayStart");
                    int tWednesdayEndIndex = reader.GetOrdinal("WednesdayEnd");
                    int tThursdayStartIndex = reader.GetOrdinal("ThursdayStart");
                    int tThursdayEndIndex = reader.GetOrdinal("ThursdayEnd");
                    int tFridayStartIndex = reader.GetOrdinal("FridayStart");
                    int tFridayEndIndex = reader.GetOrdinal("FridayEnd");
                    int tSaturdayStartIndex = reader.GetOrdinal("SaturdayStart");
                    int tSaturdayEndIndex = reader.GetOrdinal("SaturdayEnd");
                    int tSundayStartIndex = reader.GetOrdinal("SundayStart");
                    int tSundayEndIndex = reader.GetOrdinal("SundayEnd");
                    int AVG_StarIndex = reader.GetOrdinal("AVG_Star");
                    int Count_feedbackIndex= reader.GetOrdinal("Count_feedback");

                    int sIMG_LOGO_index = reader.GetOrdinal("IMG_LOGO");
                    int sIMG_DRIVE_TO_index = reader.GetOrdinal("IMG_DRIVE_TO");
                    int sIMG_MAP_index = reader.GetOrdinal("IMG_MAP");

                    int sDISCONT_NOTE_index = reader.GetOrdinal("DISCONT_NOTE");
                    int sDISCONT_CARD_index = reader.GetOrdinal("DISCONT_CARD");

                    int sDescrip_index = reader.GetOrdinal("Descrip");
                    

                    while (reader.Read()) // построчно считываем данные
                    {
                        Metro metro = new Metro
                        {
                            ID_metro = reader.IsDBNull(mID_MetroIndex) ? "" : reader.GetString(mID_MetroIndex),
                            Color_Hex = reader.IsDBNull(mColor_hexIndex) ? "#CFD8DC" : reader.GetString(mColor_hexIndex),
                            Station = reader.IsDBNull(mStationIndex) ? "Не указано" : reader.GetString(mStationIndex)
                        };

                        TimeWork timework = new TimeWork
                        {
                            ID_timeWork = reader.GetString(tID_timeWorkIndex),
                            FridayEnd = reader.GetString(tFridayEndIndex),
                            FridayStart = reader.GetString(tFridayStartIndex),
                            MondayEnd = reader.GetString(tMondayEndIndex),
                            MondayStart = reader.GetString(tMondayStartIndex),
                            SaturdayEnd = reader.GetString(tSaturdayEndIndex),
                            SaturdayStart = reader.GetString(tSaturdayStartIndex),
                            SundayEnd = reader.GetString(tSundayEndIndex),
                            SundayStart = reader.GetString(tSundayStartIndex),
                            ThursdayEnd = reader.GetString(tThursdayEndIndex),
                            ThursdayStart = reader.GetString(tThursdayStartIndex),
                            TuesdayEnd = reader.GetString(tTuesdayEndIndex),
                            TuesdayStart = reader.GetString(tTuesdayStartIndex),
                            WednesdayEnd = reader.GetString(tWednesdayEndIndex),
                            WednesdayStart = reader.GetString(tWednesdayStartIndex)
                        };

                        // подсчет среднего кол-во звезд
                        double _avg_star = reader.IsDBNull(AVG_StarIndex) ? 0 : (double)reader.GetInt32(AVG_StarIndex);
                        int _Count_feedback = reader.IsDBNull(Count_feedbackIndex) ? 0 : reader.GetInt32(Count_feedbackIndex);

                        int tmp = _Count_feedback;

                        if (_Count_feedback == 0)
                            tmp = 1;

                        _avg_star = _avg_star / tmp;// округление до 2  знаков


                        Shop = new Shop
                        {
                            ID_shop = reader.GetString(sID_shopIndex),
                            Type = reader.GetString(sTypeIndex),
                            Name = reader.GetString(sNameIndex),
                            Phone = reader.IsDBNull(sPhoneIndex) ? "" : reader.GetString(sPhoneIndex),
                            Adress = reader.IsDBNull(sAdressIndex) ? "" : reader.GetString(sAdressIndex),
                            Metro = metro,
                            DateAdd = reader.GetDateTime(sDateAddIndex),
                            TimeWork = timework,
                            Email = reader.IsDBNull(sEmailIndex) ? "" : reader.GetString(sEmailIndex),
                            Note = reader.IsDBNull(sNoteIndex) ? "" : reader.GetString(sNoteIndex),
                            Site = reader.IsDBNull(sSiteIndex) ? "" : reader.GetString(sSiteIndex),
                            AVG_Star = Math.Round(_avg_star, 1),
                            Count_feedback = _Count_feedback,
                            IMG_LOGO = reader.IsDBNull(sIMG_LOGO_index) ? "" : reader.GetString(sIMG_LOGO_index),
                            IMG_DRIVE_TO = reader.IsDBNull(sIMG_DRIVE_TO_index) ? "" : reader.GetString(sIMG_DRIVE_TO_index),
                            IMG_MAP = reader.IsDBNull(sIMG_MAP_index) ? "https://pp.userapi.com/c853620/v853620255/b695/jRcH3pN3TaI.jpg" : reader.GetString(sIMG_MAP_index),
                            DISCONT_CARD= reader.IsDBNull(sDISCONT_CARD_index) ? false : reader.GetBoolean(sDISCONT_CARD_index),
                            DISCONT_NOTE = reader.IsDBNull(sDISCONT_NOTE_index) ? "" : reader.GetString(sDISCONT_NOTE_index),
                            Descrip = reader.IsDBNull(sDescrip_index) ? "" : reader.GetString(sDescrip_index)

                        };
                    }
                }

                reader.Close();// закрытие потока

                ////////////////////////////////////////////////////////////////////////
                ///
                #region sql other shop

                string sql_first_union = @"SELECT ID_shop,
                        S.NAME,
                        S.Note,
                        s.Type,
						s.DateAdd,
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

                     FROM  SPAVREMONT.Shop  S
                     JOIN SPAVREMONT.Metro m ON s.ID_Metro=m.ID_Metro
                     WHERE s.ID_shop='6d0eb5f2-0ffd-411b-9cf2-318260b22604'
                        AND m.Name_line in (
                            SELECT m1.name_line FROM SPAVREMONT.Metro m1
                            WHERE m1.ID_Metro='" + Shop.Metro.ID_metro + @"'
                            GROUP BY m1.name_line
                        )
                     UNION ";


                sqlExpression = sql_first_union + @"SELECT TOP(6) ID_shop,
                        S.NAME,
                        S.Note,
                        s.Type,
						s.DateAdd,
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

                     FROM  SPAVREMONT.Shop  S
                     JOIN SPAVREMONT.Metro m ON s.ID_Metro=m.ID_Metro
                    
                     WHERE  s.ID_shop<>'6d0eb5f2-0ffd-411b-9cf2-318260b22604'
                        AND s.VISIBLE=1  AND  (s.TYPE='Магазин' OR s.TYPE='Разборки')
                        AND m.Name_line in (
                            SELECT m1.name_line FROM SPAVREMONT.Metro m1
                            WHERE m1.ID_Metro='" + Shop.Metro.ID_metro+@"'
                            GROUP BY m1.name_line
                        )
                    
                    ";

                #endregion


                //connection1.Open();
                //SqlCommand command_otherShops = new SqlCommand();
                //command_otherShops.CommandText = sqlExpression;
                //command_otherShops.Connection = connection1;



                command.CommandText = sqlExpression;
                SqlDataReader reader_otherShop = command.ExecuteReader();
                

                if (reader_otherShop.HasRows) // если есть данные
                {
                    //int genreIDIndex = reader.GetOrdinal("GenreID");
                    //...
                    //while...
                    //GenreID = reader.IsDBNull(genreIDIndex) ? null : reader.GetInt32(genreIDIndex)

                    int sID_shopIndex = reader_otherShop.GetOrdinal("ID_shop");
                    int sNameIndex = reader_otherShop.GetOrdinal("Name");
                    int sNoteIndex = reader_otherShop.GetOrdinal("Note");
                    int sTypeIndex = reader_otherShop.GetOrdinal("Type");
                    int mID_MetroIndex = reader_otherShop.GetOrdinal("ID_Metro");
                    int mStationIndex = reader_otherShop.GetOrdinal("Station");
                    int mColor_hexIndex = reader_otherShop.GetOrdinal("Color_hex");
                    int AVG_StarIndex = reader_otherShop.GetOrdinal("AVG_Star");
                    int Count_feedbackIndex = reader_otherShop.GetOrdinal("Count_feedback");
                    int DateAddIndex = reader_otherShop.GetOrdinal("DateAdd");

                    



                    while (reader_otherShop.Read()) // построчно считываем данные
                    {
                        Metro metro = new Metro
                        {
                            ID_metro = reader_otherShop.IsDBNull(mID_MetroIndex) ? "" : reader_otherShop.GetString(mID_MetroIndex),
                            Color_Hex = reader_otherShop.IsDBNull(mColor_hexIndex) ? "#CFD8DC" : reader_otherShop.GetString(mColor_hexIndex),
                            Station = reader_otherShop.IsDBNull(mStationIndex) ? "Не указано" : reader_otherShop.GetString(mStationIndex)
                        };



                        // подсчет среднего кол-во звезд
                        double _avg_star = reader_otherShop.IsDBNull(AVG_StarIndex) ? 0 : (double)reader_otherShop.GetInt32(AVG_StarIndex);
                        int _Count_feedback = reader_otherShop.IsDBNull(Count_feedbackIndex) ? 0 : reader_otherShop.GetInt32(Count_feedbackIndex);

                        int tmp = _Count_feedback;
                        if (_Count_feedback == 0)
                            tmp = 1;


                        _avg_star = _avg_star / tmp;// округление до 2  знаков


                        Shop _OtherShop = new Shop
                        {
                            ID_shop = reader_otherShop.GetString(sID_shopIndex),
                            Name = reader_otherShop.GetString(sNameIndex),
                            Metro = metro,
                            Note = reader_otherShop.IsDBNull(sNoteIndex) ? null : reader_otherShop.GetString(sNoteIndex),
                            AVG_Star = Math.Round(_avg_star, 1),
                            Count_feedback = _Count_feedback,
                            Type= reader_otherShop.GetString(sTypeIndex)  ,
                            DateAdd=reader_otherShop.GetDateTime(DateAddIndex)
                            
                        };

                        OtherShops.Add(_OtherShop);
                    }
                }
                ////
                reader_otherShop.Close();

                #region sql reklama

                sqlExpression = @"
                    SELECT 
                ID_Reklama,
                IMG_URL 
                 FROM SPAVREMONT.Reklama
                    WHERE ID_Reklama='1d0eb5f2-0ffd-411b-9cf2-318a60b22604'
                ";


                #endregion


                command.CommandText = sqlExpression;
                reader = command.ExecuteReader();


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


                OtherShopSortComparer dsc = new OtherShopSortComparer();

                OtherShops.Sort(dsc);

            }// end using

            Shop.iMG_Shops = GetShopIMGs(id_shop);


        }


        public class OtherShopSortComparer : IComparer<Shop>
        {
            public int Compare(Shop o1, Shop o2)
            {
                if (o1.DateAdd > o2.DateAdd)
                {
                    return 1;
                }
                else if (o1.DateAdd < o2.DateAdd)
                {
                    return -1;
                }

                return 0;
            }
        }

        private List<IMG_Shop> GetShopIMGs(string ID_SHOP)
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


            return iMG_Shops;
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