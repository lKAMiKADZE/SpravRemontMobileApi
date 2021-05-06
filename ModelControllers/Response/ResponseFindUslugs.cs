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
    public class ResponseFindUslugs
    {
        [JsonProperty("Shops_short_uslug")]
        public List<Shop_short_Uslug> Shops_short_uslug { get; set; }


        public  void GetFindUslugs(string connectionString, RequestFindUslug req)
        {
            //using  настройка запроса выбора товара
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Shops_short_uslug = new List<Shop_short_Uslug>();
               
                ////////////
                // ОБРАБОТКА ДАННЫХ ПЕРЕД ИНСЕРТОМ
                ///////////
                
                string idUslug = "";
                string id_City = req.ID_City;//"6d0eb5f2-01fd-411b-9cf2-318a60b22604";// Москва
                int intBuyCard = 0;
                int TimeWayMetro = req.TimeWayMetro;
                string idMetro = "";

                if (req.ID_uslug != null)
                    idUslug = req.ID_uslug;


                // станции метро, если список не пустой
                if (req.metro != null && req.metro.Count > 0)
                    for (int i = 0; i < req.metro.Count; i++)
                    {
                        if (i > 0) idMetro += ",";
                        idMetro += "'" + req.metro[i].ID_metro + "'";
                    }

                if (req.buy_card) intBuyCard = 1;

                string whereID_Uslug = " AND usl.ID_USLUG =  '" + idUslug + "'";
                string whereID_CIty = "AND sh.ID_City='" + id_City + "'   ";
                string whereTypeShop = " AND sh.ID_TYPE_SHOP='360eb5f2-0ffd-411b-9cf2-318a60b22604'";// тип оргназации услуги

                string whereTimeWayMetro = " AND sh.TimeWayMetro<=" + TimeWayMetro.ToString();
                string whereID_metro = " AND met.ID_metro in (" + idMetro + ")";
                string whereBuy_card = " AND sh.BuyCard=" + intBuyCard.ToString();

                if (idUslug == "") return ;

                // если оплаты за безнал нет, то тогда выводим все магазины, если есть то выводим только магазины с оплатой по карте
                if (intBuyCard == 0)
                    whereBuy_card = "";

                if (req.AllMetro)
                    whereID_metro = "";

                // параметр отвечает за кол-во минут ходьбы от метро до организации
                if (TimeWayMetro <= 0)
                    whereTimeWayMetro = "";

                if (String.IsNullOrEmpty(id_City))
                    whereID_CIty = "";


                #region SQL query
                //string sqlFirstShopUnion = @"   ";

                string sqlExpression = @"
                        SELECT 
                        sh.ID_shop,
                        sh.Name,
                        sh.TimeWayMetro,
                        
                        met.ID_Metro,
                        met.station,
                        met.Color_hex,
                                             
                        usl.ID_USLUG,
                        usl.NAME_USLUG,
                        usl.NOTE_USLUG,
                        usl.IMG_URL,

                        uss.Price
                        
                        FROM SPAVREMONT.SHOP sh
                         JOIN SPAVREMONT.USLUGS_SHOP uss ON sh.ID_Shop=uss.ID_Shop 
                         JOIN SPAVREMONT.USLUG usl ON uss.ID_USLUG=usl.ID_USLUG
                         LEFT JOIN SPAVREMONT.Metro met ON met.ID_Metro=sh.ID_metro
                        
                         WHERE 1=1
                              AND sh.VISIBLE=1
                             " + whereID_CIty + @"
                             " + whereID_Uslug + @"   
                             " + whereTypeShop + @"   
                             " + whereTimeWayMetro + @"   
                             " + whereID_metro + @"   
                             " + whereBuy_card + @"      

                            GROUP BY 
                        sh.ID_shop,
                        sh.Name,
                        sh.TimeWayMetro,
                        
                        met.ID_Metro,
                        met.station,
                        met.Color_hex,
                                        
                        usl.ID_USLUG,
                        usl.NAME_USLUG,
                        usl.NOTE_USLUG,
                        usl.IMG_URL,

                        uss.Price
        
                    ";

                #endregion

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                

                if (reader.HasRows) // если есть данные
                {
                    int sID_shopIndex = reader.GetOrdinal("ID_shop");
                    int sNameIndex       = reader.GetOrdinal("Name");
                    int sTimeWayMetroIndex = reader.GetOrdinal("TimeWayMetro");                    
                    int mID_MetroIndex   =  reader.GetOrdinal("ID_Metro");
                    int mstationIndex    =  reader.GetOrdinal("station");
                    int mColor_hexIndex = reader.GetOrdinal("Color_hex");

                    int itmID_uslug_index = reader.GetOrdinal("ID_USLUG");
                    int itmNAME_uslug_index = reader.GetOrdinal("NAME_USLUG");
                    int itmNOTE_uslug_index = reader.GetOrdinal("NOTE_USLUG");
                    int itmIMG_URL_index = reader.GetOrdinal("IMG_URL");

                    int PRICE_Index = reader.GetOrdinal("Price");

                    while (reader.Read()) // построчно считываем данные
                    {

                        Metro metro = new Metro
                        {
                            ID_metro = reader.IsDBNull(mID_MetroIndex)? "": reader.GetString(mID_MetroIndex),
                            Station = reader.IsDBNull(mstationIndex) ? "" : reader.GetString(mstationIndex),
                            Color_Hex = reader.IsDBNull(mColor_hexIndex) ? "" : reader.GetString(mColor_hexIndex)                            
                        };
                                                

                        USLUG item_uslug = new USLUG
                        {
                            ID_USLUG = reader.GetString(itmID_uslug_index),
                            IMG_URL = reader.GetString(itmIMG_URL_index),
                            NAME_USLUG = reader.GetString(itmNAME_uslug_index),
                            NOTE_USLUG = reader.GetString(itmNOTE_uslug_index)
                        };

                        Shop_short_Uslug item = new Shop_short_Uslug
                        {
                            ID_shop = reader.GetString(sID_shopIndex),
                            Name = reader.GetString(sNameIndex),
                            Metro = metro,                                                     
                            TimeWayMetro= reader.GetInt32(sTimeWayMetroIndex),
                            

                            Uslug=item_uslug,
                            Price=reader.GetInt32(PRICE_Index),

                        };

                       
                            Shops_short_uslug.Add(item);

                        
                    }
                }


              
            



            }//return shops;
        }

    }


    

}