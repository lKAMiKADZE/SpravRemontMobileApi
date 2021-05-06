using SpravRemontMobileApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.ModelControllers.Response
{
    public class ResponseItemsShop
    {
        public List<ITEMS_SHOP> ItemsShop { get; set; }
        public List<KATEGOR> Kategors { get; set; }

        public void GetItemsShop(string connectionString, RequestShopClient req)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                ItemsShop = new List<ITEMS_SHOP>();

                string sqlExpression = @"SELECT 
                    its.ID_ITEMS_SHOP,
                    its.ID_SHOP,
                    its.ID_ITEM_KATEGOR,
                    its.MIN_PRICE,
                    its.MAX_PRICE,
                    itm.ID_ITEM,
                    itm.NAME_ITEM,
                    itm.NOTE_ITEM,
                    itm.IMG_URL,
                    kat.ID_KATEGOR,
                    kat.NAME_KATEGOR
                    
                     FROM SPAVREMONT.ITEMS_SHOP its
                     JOIN SPAVREMONT.ITEM_KATEGOR itk ON its.ID_ITEM_KATEGOR=itk.ID_ITEM_KATEGOR
                     JOIN SPAVREMONT.ITEM itm ON itk.ID_ITEM=itm.ID_ITEM
                     JOIN SPAVREMONT.KATEGOR kat ON itk.ID_KATEGOR=kat.ID_KATEGOR
                    
                     WHERE its.ID_SHOP='" + req.id_shop + @"'
                                AND kat.ID_KATEGOR='" + req.id_kategor + @"'
                     
                     
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
                    
                    int itsID_ITEMS_SHOPIndex = reader.GetOrdinal("ID_ITEMS_SHOP");
                    int itsID_SHOPIndex = reader.GetOrdinal("ID_SHOP");
                    int itsID_ITEM_KATEGORIndex = reader.GetOrdinal("ID_ITEM_KATEGOR");
                    int itsMIN_PRICEIndex = reader.GetOrdinal("MIN_PRICE");
                    int itsMAX_PRICEIndex = reader.GetOrdinal("MAX_PRICE");
                    int itmID_ITEMIndex = reader.GetOrdinal("ID_ITEM");
                    int itmNAME_ITEMIndex = reader.GetOrdinal("NAME_ITEM");
                    int itmNOTE_ITEMIndex = reader.GetOrdinal("NOTE_ITEM");
                    int itmIMG_URLIndex = reader.GetOrdinal("IMG_URL");
                    int katID_KATEGORIndex = reader.GetOrdinal("ID_KATEGOR");
                    int katNAME_KATEGORIndex = reader.GetOrdinal("NAME_KATEGOR");


                    while (reader.Read()) // построчно считываем данные
                    {

                        ITEM item = new ITEM
                        {
                            ID_ITEM= reader.GetString(itmID_ITEMIndex),
                            IMG_URL = reader.GetString(itmIMG_URLIndex),
                            NAME_ITEM = reader.GetString(itmNAME_ITEMIndex),
                            NOTE_ITEM = reader.GetString(itmNOTE_ITEMIndex)                            
                        };

                        KATEGOR kategor = new KATEGOR
                        {
                            ID_KATEGOR = reader.GetString(katID_KATEGORIndex),
                            NAME_KATEGOR = reader.GetString(katNAME_KATEGORIndex)
                        };

                        ITEM_KATEGOR itemKategor = new ITEM_KATEGOR
                        {
                            ID_ITEM_KATEGOR = reader.GetString(itsID_ITEM_KATEGORIndex),
                            ITEM=item,
                            KATEGOR=kategor
                        };

                        ITEMS_SHOP items_shop = new ITEMS_SHOP
                        {
                            ID_ITEMS_SHOP= reader.GetString(itsID_ITEMS_SHOPIndex),
                            ID_SHOP=req.id_shop,
                            ITEM_KATEGOR=itemKategor,
                            MAX_PRICE = reader.GetInt32(itsMAX_PRICEIndex),
                            MIN_PRICE = reader.GetInt32(itsMIN_PRICEIndex)

                        };
                                              

                        
                            //Site = reader.IsDBNull(sSiteIndex) ? "" : reader.GetString(sSiteIndex),
                            
                        ItemsShop.Add(items_shop);
                    }
                }




                //return shops;
            }
        }


        public void GetKategory(string connectionString, RequestShopClient req)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Kategors = new List<KATEGOR>();

                string sqlExpression = @"SELECT 
                    kat.ID_KATEGOR,
                    kat.NAME_KATEGOR
                    
                     FROM SPAVREMONT.ITEMS_SHOP its
                     JOIN SPAVREMONT.ITEM_KATEGOR itk ON its.ID_ITEM_KATEGOR=itk.ID_ITEM_KATEGOR
                     JOIN SPAVREMONT.KATEGOR kat ON itk.ID_KATEGOR=kat.ID_KATEGOR
                    
                    
                     WHERE its.ID_SHOP='" + req.id_shop + @"'
                      
                     GROUP BY 
                     kat.ID_KATEGOR,
                     kat.NAME_KATEGOR
                    
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

                   
                    int katID_KATEGORIndex = reader.GetOrdinal("ID_KATEGOR");
                    int katNAME_KATEGORIndex = reader.GetOrdinal("NAME_KATEGOR");


                    while (reader.Read()) // построчно считываем данные
                    {
                        KATEGOR kategor = new KATEGOR
                        {
                            ID_KATEGOR = reader.GetString(katID_KATEGORIndex),
                            NAME_KATEGOR = reader.GetString(katNAME_KATEGORIndex)
                        };

                        //Site = reader.IsDBNull(sSiteIndex) ? "" : reader.GetString(sSiteIndex),

                        Kategors.Add(kategor);
                    }
                }
                
            }
        }// END METOD
    }
}