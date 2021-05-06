using SpravRemontMobileApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.ModelControllers.Response
{
    public class ResponseItems_buy
    {
        public List<ITEMS_BUY> Items { get; set; }

        public void GetItemsBuy(string connectionString, RequestItemsBuy req)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Items = new List<ITEMS_BUY>();

                string sqlExpression = @"SELECT 
                    ID_ITEMS_BUY,
                    ID_ITEMS_SHOP,
                    NAME_IB,
                    NOTE_IB,
                    IMG_URL,
                    PRICE
                    
                     FROM SPAVREMONT.ITEMS_BUY
                    WHERE id_items_shop='" + req.id_items_shop + @"'
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    
                    int ID_ITEMS_BUY_Index = reader.GetOrdinal("ID_ITEMS_BUY");
                    int ID_ITEMS_SHOP_Index = reader.GetOrdinal("ID_ITEMS_SHOP");
                    int NAME_IB_Index = reader.GetOrdinal("NAME_IB");
                    int NOTE_IB_Index = reader.GetOrdinal("NOTE_IB");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");
                    int PRICE_Index = reader.GetOrdinal("PRICE");


                    while (reader.Read()) // построчно считываем данные
                    {
                        ITEMS_BUY Item = new ITEMS_BUY
                        {
                            ID_ITEMS_BUY= reader.GetString(ID_ITEMS_BUY_Index),
                            IMG_URL = reader.GetString(IMG_URL_Index),
                            ID_ITEMS_SHOP = reader.GetString(ID_ITEMS_SHOP_Index),
                            NAME_IB = reader.GetString(NAME_IB_Index),
                            NOTE_IB= reader.GetString(NOTE_IB_Index),
                            PRICE= reader.GetInt32(PRICE_Index)
                        };
   
                        Items.Add(Item);
                    }
                }



                
            }
        }

        
    }
}