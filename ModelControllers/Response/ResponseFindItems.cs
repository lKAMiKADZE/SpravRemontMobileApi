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
    public class ResponseFindItems
    {
        [JsonProperty("Shops_short_item")]
        public List<Shop_short_Item> Shops_short_item { get; set; }


        public  void GetFindItems(string connectionString, RequestFindItem req)
        {
            //using  настройка запроса выбора товара
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Shops_short_item = new List<Shop_short_Item>();
               
                ////////////
                // ОБРАБОТКА ДАННЫХ ПЕРЕД ИНСЕРТОМ
                ///////////

                string idTypesShop = "";
                int intBuyCard = 0;
                int TimeWayMetro = req.TimeWayMetro;
                string idItem = "";
                string idMetro = "";
                string id_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604";// Москва

                if (req.geoPhone == null)
                    req.geoPhone = new Geo { X = 0, Y = 0 };

                if (req.item != null)
                    idItem = req.item.ID_ITEM;

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
                
                // подготовка к вставке в блок where
                string whereID_TYPE_SHOP = " AND sh.ID_TYPE_SHOP in (" + idTypesShop + ")";
                string whereTimeWayMetro = " AND sh.TimeWayMetro<=" + TimeWayMetro.ToString(); 
                string whereID_Item = " AND itm.ID_ITEM = '" + idItem + "'";
                string whereID_metro = " AND met.ID_metro in (" + idMetro + ")";
                string whereBuy_card = " AND sh.BuyCard=" + intBuyCard.ToString();

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

                if (idItem == "") return ;

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
                        SELECT sum(CC.Count_star) AS avg_star FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=sh.ID_Shop
                    ) AS AVG_Star,
                    (
                        SELECT count(*) AS count_feedback FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=sh.ID_Shop
                            AND CC.visible=1
                    ) AS Count_feedback,

                    --параметры товара,
                    -- название цена
                    itm.ID_ITEM,
                    itm.NAME_ITEM,
                    itm.NOTE_ITEM,
                    itm.IMG_URL,
                    sh.IMG_LOGO,
                    
                    its.MIN_PRICE,
                    its.MAX_PRICE

                    
                    
                    FROM SPAVREMONT.SHOP sh
                     JOIN SPAVREMONT.ITEMS_SHOP its ON sh.ID_Shop=its.ID_Shop
                     JOIN SPAVREMONT.ITEM_KATEGOR itk ON its.ID_ITEM_KATEGOR=itk.ID_ITEM_KATEGOR
                     JOIN SPAVREMONT.KATEGOR kat ON itk.ID_KATEGOR=kat.ID_KATEGOR
                     JOIN SPAVREMONT.ITEM itm ON itk.ID_ITEM=itm.ID_ITEM
                     JOIN SPAVREMONT.Metro met ON met.ID_Metro=sh.ID_metro
                     JOIN SPAVREMONT.Geo shgeo ON shgeo.ID_Geo=sh.ID_Geo
                    WHERE sh.ID_SHOP='6d0eb5f2-0ffd-411b-9cf2-318260b22604'
                           AND sh.VISIBLE=1
                             " + whereID_CIty + @"
                        " + whereID_TYPE_SHOP + @"
                        " + whereID_Item + @"

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

                    itm.ID_ITEM,
                    itm.NAME_ITEM,
                    itm.NOTE_ITEM,
                    itm.IMG_URL,
                    sh.IMG_LOGO,
                    
                    its.MIN_PRICE,
                    its.MAX_PRICE
                    
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
                        SELECT sum(CC.Count_star) AS avg_star FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=sh.ID_Shop
                    ) AS AVG_Star,
                    (
                        SELECT count(*) AS count_feedback FROM SPAVREMONT.Comment_client CC 
                        WHERE CC.ID_SHOP=sh.ID_Shop
                            AND CC.visible=1
                    ) AS Count_feedback ,                    

                    --параметры товара,
                    -- название цена
                    itm.ID_ITEM,
                    itm.NAME_ITEM,
                    itm.NOTE_ITEM,
                    itm.IMG_URL,
                    sh.IMG_LOGO,
                    
                    its.MIN_PRICE,
                    its.MAX_PRICE
                    
                    FROM SPAVREMONT.SHOP sh
                     JOIN SPAVREMONT.ITEMS_SHOP its ON sh.ID_Shop=its.ID_Shop
                     JOIN SPAVREMONT.ITEM_KATEGOR itk ON its.ID_ITEM_KATEGOR=itk.ID_ITEM_KATEGOR
                     JOIN SPAVREMONT.KATEGOR kat ON itk.ID_KATEGOR=kat.ID_KATEGOR   
                     JOIN SPAVREMONT.ITEM itm ON itk.ID_ITEM=itm.ID_ITEM                  
                     LEFT JOIN SPAVREMONT.Metro met ON met.ID_Metro=sh.ID_metro
                     LEFT JOIN SPAVREMONT.Geo shgeo ON shgeo.ID_Geo=sh.ID_Geo
                    WHERE 1=1
                             " + whereID_CIty + @"
                        AND sh.ID_SHOP<>'6d0eb5f2-0ffd-411b-9cf2-318260b22604'
                        " + whereID_TYPE_SHOP + @"
                        " + whereID_Item + @"


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

                      itm.ID_ITEM,
                      itm.NAME_ITEM,
                      itm.NOTE_ITEM,
                      itm.IMG_URL,
                      sh.IMG_LOGO,
                    
                    its.MIN_PRICE,
                    its.MAX_PRICE


                    ";

                #endregion

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                Shop_short_Item Shop_short_ItemFirst = new Shop_short_Item();
                int tmp_count = 0;

                if (reader.HasRows) // если есть данные
                {
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
                    
                    //int katKategorIndex = reader.GetOrdinal("ID_KATEGOR");
                    int shgeoXIndex = reader.GetOrdinal("X");
                    int shgeoYIndex = reader.GetOrdinal("Y");

                    int itmID_ITEM_index = reader.GetOrdinal("ID_ITEM");
                    int itmNAME_ITEM_index = reader.GetOrdinal("NAME_ITEM");
                    int itmNOTE_ITEM_index = reader.GetOrdinal("NOTE_ITEM");
                    int itmIMG_URL_index = reader.GetOrdinal("IMG_URL");
                    int sIMG_LOGO_index = reader.GetOrdinal("IMG_LOGO");

                    int MAX_PRICE_Index = reader.GetOrdinal("MAX_PRICE");
                    int MIN_PRICE_Index = reader.GetOrdinal("MIN_PRICE");

                    while (reader.Read()) // построчно считываем данные
                    {

                        Metro metro = new Metro
                        {
                            ID_metro = reader.IsDBNull(mID_MetroIndex) ? "" : reader.GetString(mID_MetroIndex),
                            Station = reader.IsDBNull(mstationIndex) ? "" : reader.GetString(mstationIndex),
                            Color_Hex = reader.IsDBNull(mColor_hexIndex) ? "" : reader.GetString(mColor_hexIndex)                            
                        };
                        
                        Geo geoShop = new Geo
                        {
                            X =0,// (float)reader.GetDouble(shgeoXIndex),
                            Y =0// (float)reader.GetDouble(shgeoYIndex)
                        };
                                               
                        // Вывести подсчет дистанции между двумя точками
                        float distanse = -1;
                        double distanseResult = 0;
                        float xPhone = req.geoPhone.X;
                        float yPhone = req.geoPhone.Y;

                        distanse = (xPhone - geoShop.X) * (xPhone - geoShop.X) + (yPhone - geoShop.Y) * (yPhone - geoShop.Y);
                        distanseResult = Math.Sqrt(distanse);
                        

                        // подсчет среднего кол-во звезд
                        double _avg_star = reader.IsDBNull(AVG_StarIndex) ? 0 : (double)reader.GetInt32(AVG_StarIndex);
                        int _Count_feedback = reader.IsDBNull(Count_feedbackIndex) ? 0 : reader.GetInt32(Count_feedbackIndex);

                        int tmp = _Count_feedback;
                        if (_Count_feedback == 0)
                            tmp = 1;
                        _avg_star = _avg_star / tmp;// округление до2  знаков

                        ITEM item_katalog = new ITEM
                        {
                            ID_ITEM = reader.GetString(itmID_ITEM_index),
                            IMG_URL = reader.GetString(itmIMG_URL_index),
                            NAME_ITEM = reader.GetString(itmNAME_ITEM_index),
                            NOTE_ITEM = reader.GetString(itmNOTE_ITEM_index)
                        };

                        Shop_short_Item item = new Shop_short_Item
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
                            IMG_LOGO= reader.IsDBNull(sIMG_LOGO_index) ? "emptylogo.png" : reader.GetString(sIMG_LOGO_index),

                            item=item_katalog,
                            MaxPrice=reader.GetInt32(MAX_PRICE_Index),
                            MinPrice = reader.GetInt32(MIN_PRICE_Index)

                        };

                        if (tmp_count == 0)
                            Shop_short_ItemFirst = item;
                        else
                            Shops_short_item.Add(item);

                        tmp_count++;
                    }
                }


               // distanseSortItemComparer dsc = new distanseSortItemComparer();

                //Shops_short_item.Sort(dsc);

                if (tmp_count > 0)
                    Shops_short_item.Insert(0, Shop_short_ItemFirst);
                //по желанию заказчика, если магазин заказчика выводится в списке, он должен быть на первом месте




            }//return shops;
        }

    }


    public class distanseSortItemComparer : IComparer<Shop_short_Item>
    {
        public int Compare(Shop_short_Item o1, Shop_short_Item o2)
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