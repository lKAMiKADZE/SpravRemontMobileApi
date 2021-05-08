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
    public class ResponseFindShops
    {
        [JsonProperty("Shops_short")]
        public List<Shop_short> Shops_short { get; set; }


        public  void GetFindShops(string connectionString, RequestFindShops req)
        {

            //using (SqlConnection connection = new SqlConnection(connectionString))
            SqlConnection connection = new SqlConnection(connectionString);
            
                Shops_short = new List<Shop_short>();

                ////////////
                // ОБРАБОТКА ДАННЫХ ПЕРЕД ИНСЕРТОМ
                ///////////

                string idTypesShop = "";
                int intBuyCard = 0;
                int intDiscontCard = 0;
                int TimeWayMetro = req.TimeWayMetro;
                string idKategor = "";
                string idMetro = "";
                string id_City = req.ID_City;//"6d0eb5f2-01fd-411b-9cf2-318a60b22604";// Москва

                if (req.geoPhone == null)
                    req.geoPhone = new Geo { X = 0, Y = 0 };

                if (req.kategor != null)
                    idKategor = req.kategor.ID_KATEGOR;

                // тип магазина
                //for (int i=0; i< req.type_shop.Length; i++)
                //{
                //    if (i > 0) idTypesShop += ",";
                //    idTypesShop += "'" + req.type_shop[i].ID_TYPE_SHOP + "'";
                //}

                switch (req.type_shop_012)
                {
                    case 0: idTypesShop = "'340eb5f2-0ffd-411b-9cf2-318a60b22604','350eb5f2-0ffd-411b-9cf2-318a60b22604'"; break;
                    case 1: idTypesShop = "'340eb5f2-0ffd-411b-9cf2-318a60b22604'"; break;
                    case 2: idTypesShop = "'350eb5f2-0ffd-411b-9cf2-318a60b22604'"; break;
                }
                                
                // станции метро, если список не пустой
                if (req.metro != null && req.metro.Count>0)
                    for (int i = 0; i < req.metro.Count; i++)
                    {
                        if (i > 0) idMetro += ",";
                        idMetro += "'" + req.metro[i].ID_metro + "'";
                    }
                
                if (req.buy_card) intBuyCard = 1;
                if (req.DISCONT_CARD) intDiscontCard = 1;
                
                // подготовка к вставке в блок where
                string whereID_TYPE_SHOP = " AND sh.ID_TYPE_SHOP in (" + idTypesShop + ")";
                string whereTimeWayMetro = " AND sh.TimeWayMetro<=" + TimeWayMetro.ToString(); 
                string whereID_kategor = " AND itk.ID_kategor = '" + idKategor + "'";
                string whereID_metro = " AND met.ID_metro in (" + idMetro + ")";
                string whereBuy_card = " AND sh.BuyCard=" + intBuyCard.ToString();
                string whereDISCONT_CARD = " AND sh.DISCONT_CARD=" + intDiscontCard.ToString();
                string whereID_CIty = "AND sh.ID_City='" + id_City + "' -- Москва  ";

                // если флаг стоит что поиск по всем станциям, то выборку по поределенным станциям убираем
                if (req.AllMetro)
                    whereID_metro = "";

                // параметр отвечает за кол-во минут ходьбы от метро до организации
                if (TimeWayMetro <= 0)
                    whereTimeWayMetro = "";

                // если оплаты за безнал нет, то тогда выводим все магазины, если есть то выводим только магазины с оплатой по карте
                if (intBuyCard == 0)
                    whereBuy_card = "";
                //если нету дисконтной карты, то выводим все магазины
                if (intDiscontCard == 0)
                    whereDISCONT_CARD = "";

                if (idKategor == "")
                    whereID_kategor = "";

                if (String.IsNullOrEmpty(id_City))
                    whereID_CIty = "";

                #region SQL query
                string sqlFirstShopUnion = @"
                    SELECT 
                    sh.ID_shop,
                    sh.Type,
                    sh.Name,
                    sh.TimeWayMetro,
                    sh.Phone,
                    
                    met.ID_Metro,
                    met.station,
                    met.Color_hex,
                    
                    shgeo.X,
                    shgeo.Y,
                    
                    (
                        SELECT ISNULL(sum(CC.Count_star),0) AS avg_star FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=sh.ID_Shop
                    ) AS AVG_Star,
                    (
                        SELECT count(*) AS count_feedback FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=sh.ID_Shop
                            AND CC.visible=1
                    ) AS Count_feedback,

                    sh.IMG_LOGO

                    
                    

                    FROM SPAVREMONT.SHOP sh
                     JOIN SPAVREMONT.ITEMS_SHOP its ON sh.ID_Shop=its.ID_Shop
                     JOIN SPAVREMONT.ITEM_KATEGOR itk ON its.ID_ITEM_KATEGOR=itk.ID_ITEM_KATEGOR
                     JOIN SPAVREMONT.KATEGOR kat ON itk.ID_KATEGOR=kat.ID_KATEGOR
                     --JOIN SPAVREMONT.ITEM itm ON itk.ID_ITEM=itm.ID_ITEM
                     LEFT JOIN SPAVREMONT.Metro met ON met.ID_Metro=sh.ID_metro
                     LEFT JOIN SPAVREMONT.Geo shgeo ON shgeo.ID_Geo=sh.ID_Geo
                    WHERE sh.ID_SHOP='6d0eb5f2-0ffd-411b-9cf2-318260b22604'
                        AND sh.VISIBLE=1
                             " + whereID_CIty + @"
                        " + whereID_TYPE_SHOP + @"
                        " + whereBuy_card + @" 
                        " + whereTimeWayMetro + @" 
                        " + whereID_kategor + @"
                        " + whereID_metro + @"
                        " + whereDISCONT_CARD + @"
                    GROUP BY sh.ID_shop,
                    sh.Type,
                    sh.Name,
                    sh.TimeWayMetro, 
                    sh.Phone,
                    
                    met.ID_Metro,
                    met.station,
                    met.Color_hex,
                    
                    shgeo.X,
                    shgeo.Y,

                    sh.IMG_LOGO
                    
                    UNION ";

                string sqlExpression = sqlFirstShopUnion + @"
                    SELECT 
                    sh.ID_shop,
                    sh.Type,
                    sh.Name,
                    sh.TimeWayMetro,
                    sh.Phone,
                    
                    met.ID_Metro,
                    met.station,
                    met.Color_hex,

                    shgeo.X,
                    shgeo.Y,
                    
                    (
                        SELECT ISNULL(sum(CC.Count_star),0) AS avg_star FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=sh.ID_Shop
                    ) AS AVG_Star,
                    (
                        SELECT count(*) AS count_feedback FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=sh.ID_Shop
                            AND CC.visible=1
                    ) AS Count_feedback   ,

                    sh.IMG_LOGO                 
                    
                    FROM SPAVREMONT.SHOP sh
                     JOIN SPAVREMONT.ITEMS_SHOP its ON sh.ID_Shop=its.ID_Shop
                     JOIN SPAVREMONT.ITEM_KATEGOR itk ON its.ID_ITEM_KATEGOR=itk.ID_ITEM_KATEGOR
                     JOIN SPAVREMONT.KATEGOR kat ON itk.ID_KATEGOR=kat.ID_KATEGOR                     
                     LEFT JOIN SPAVREMONT.Metro met ON met.ID_Metro=sh.ID_metro
                     LEFT JOIN SPAVREMONT.Geo shgeo ON shgeo.ID_Geo=sh.ID_Geo
                    WHERE 1=1
                        AND sh.VISIBLE=1
                             " + whereID_CIty + @"
                        AND sh.ID_SHOP<>'6d0eb5f2-0ffd-411b-9cf2-318260b22604'
                        " + whereID_TYPE_SHOP + @"
                        " + whereBuy_card + @" 
                        " + whereTimeWayMetro + @" 
                        " + whereID_kategor + @"
                        " + whereID_metro + @"
                        " + whereDISCONT_CARD + @"

                    GROUP BY sh.ID_shop,
                      sh.Type,
                      sh.Name,
                      sh.TimeWayMetro,
                      sh.Phone,
                      
                      met.ID_Metro,
                      met.station,
                      met.Color_hex,

                      shgeo.X,
                      shgeo.Y,

                    sh.IMG_LOGO


                    ";

                #endregion

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                Shop_short shop_ShortFirst = new Shop_short();
                int tmp_count = 0;

                if (reader.HasRows) // если есть данные
                {
                    //int genreIDIndex = reader.GetOrdinal("GenreID");
                    //...
                    //while...
                    //GenreID = reader.IsDBNull(genreIDIndex) ? null : reader.GetInt32(genreIDIndex)
                    int sID_shopIndex = reader.GetOrdinal("ID_shop");
                    int sTypeIndex = reader.GetOrdinal("Type");
                    int sNameIndex       = reader.GetOrdinal("Name");
                    int sPhoneIndex = reader.GetOrdinal("Phone");
                    int sTimeWayMetroIndex = reader.GetOrdinal("TimeWayMetro");                    
                    int mID_MetroIndex   =  reader.GetOrdinal("ID_Metro");
                    int mstationIndex    =  reader.GetOrdinal("station");
                    int mColor_hexIndex = reader.GetOrdinal("Color_hex");
                    int AVG_StarIndex = reader.GetOrdinal("AVG_Star");
                    int Count_feedbackIndex = reader.GetOrdinal("Count_feedback");

                    int shIMG_LOGO_index = reader.GetOrdinal("IMG_LOGO");

                    //int katKategorIndex = reader.GetOrdinal("ID_KATEGOR");
                    //int shgeoXIndex = reader.GetOrdinal("X");
                    //int shgeoYIndex = reader.GetOrdinal("Y");


                    while (reader.Read()) // построчно считываем данные
                    {

                        Metro metro = new Metro
                        {
                            ID_metro = reader.IsDBNull(mID_MetroIndex) ? "" : reader.GetString(mID_MetroIndex),
                            Station = reader.IsDBNull(mstationIndex) ? "Не указано" : reader.GetString(mstationIndex),
                            Color_Hex = reader.IsDBNull(mColor_hexIndex) ? "#CFD8DC" : reader.GetString(mColor_hexIndex)                            
                        };


                        Geo geoShop = new Geo
                        {
                            X = 0,//(float)reader.GetDouble(shgeoXIndex),
                            Y = 0//(float)reader.GetDouble(shgeoYIndex)
                        };



                        // Вывести подсчет дистанции между двумя точками
                        float distanse = -1;
                        double distanseResult = 0;
                        //float xPhone = req.geoPhone.X;
                       // float yPhone = req.geoPhone.Y;

                        //distanse = (xPhone - geoShop.X) * (xPhone - geoShop.X) + (yPhone - geoShop.Y) * (yPhone - geoShop.Y);
                        //distanseResult = Math.Sqrt(distanse);
                        

                        // подсчет среднего кол-во звезд
                        double _avg_star = reader.IsDBNull(AVG_StarIndex) ? 0 : (double)reader.GetInt32(AVG_StarIndex);
                        int _Count_feedback = reader.IsDBNull(Count_feedbackIndex) ? 0 : reader.GetInt32(Count_feedbackIndex);

                        int tmp = _Count_feedback;
                        if (_Count_feedback == 0)
                            tmp = 1;
                        _avg_star = _avg_star / tmp;// округление до2  знаков


                        Shop_short item = new Shop_short
                        {
                            ID_shop = reader.GetString(sID_shopIndex),
                            Type = reader.GetString(sTypeIndex),
                            Name = reader.GetString(sNameIndex),
                            Metro = metro,
                            AVG_Star = Math.Round(_avg_star, 1),
                            Count_feedback = _Count_feedback,
                            Geo = geoShop,
                            distanse = distanseResult,
                           // id_kateg = reader.GetString(katKategorIndex),
                            Phone = reader.IsDBNull(sPhoneIndex) ? null : reader.GetString(sPhoneIndex),
                            TimeWayMetro= reader.GetInt32(sTimeWayMetroIndex),
                            IMG_LOGO= reader.IsDBNull(shIMG_LOGO_index) ? "": reader.GetString(shIMG_LOGO_index)

                        };

                        if (tmp_count == 0)
                            shop_ShortFirst = item;
                        else
                            Shops_short.Add(item);

                        tmp_count++;
                    }
                }


                //distanseSortComparer dsc = new distanseSortComparer();

                //Shops_short.Sort(dsc);

                if(tmp_count>0)
                    Shops_short.Insert(0, shop_ShortFirst);
                // по желанию заказчика, если магазин заказчика выводится в списке, он должен быть на первом месте

                
                
            //}//return shops;
        }

    }


    public class distanseSortComparer : IComparer<Shop_short>
    {
        public int Compare(Shop_short o1, Shop_short o2)
        {
            if (o1.distanse > o2.distanse)
            {
                return 1;
            }
            else if (o1.distanse < o2.distanse)
            {
                return -1;
            }

            return 0;
        }
    }

}