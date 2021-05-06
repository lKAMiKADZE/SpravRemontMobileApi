using SpravRemontMobileApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.ModelControllers.Response
{
    public class ResponseItems
    {
        public List<ITEM> Items { get; set; }

        public void GetItemsKatalog(string connectionString, RequestItems req)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Items = new List<ITEM>();

                string sqlExpression = @"SELECT                    
                    itm.ID_ITEM,
                    itm.NAME_ITEM,
                    itm.NOTE_ITEM,
                    itm.IMG_URL,
                    kat.ID_KATEGOR
                    
                     FROM SPAVREMONT.ITEM itm
                     JOIN SPAVREMONT.ITEM_KATEGOR itk ON itk.ID_ITEM=itm.ID_ITEM
                     JOIN SPAVREMONT.KATEGOR kat ON itk.ID_KATEGOR=kat.ID_KATEGOR
                    
                     WHERE 1=1
                       AND kat.ID_KATEGOR='" + req.id_kategor + @"'
                        ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    
                    int itmID_ITEMIndex = reader.GetOrdinal("ID_ITEM");
                    int itmNAME_ITEMIndex = reader.GetOrdinal("NAME_ITEM");
                    int itmNOTE_ITEMIndex = reader.GetOrdinal("NOTE_ITEM");
                    int itmIMG_URLIndex = reader.GetOrdinal("IMG_URL");
                    int katID_KATEGORIndex = reader.GetOrdinal("ID_KATEGOR");


                    while (reader.Read()) // построчно считываем данные
                    {

                        ITEM Item = new ITEM
                        {
                            ID_ITEM= reader.GetString(itmID_ITEMIndex),
                            IMG_URL = reader.GetString(itmIMG_URLIndex),
                            NAME_ITEM = reader.GetString(itmNAME_ITEMIndex),
                            NOTE_ITEM = reader.GetString(itmNOTE_ITEMIndex)                            
                        };

                        //KATEGOR kategor = new KATEGOR
                        //{
                        //    ID_KATEGOR = reader.GetString(katID_KATEGORIndex)
                        //};
   
                        Items.Add(Item);
                    }
                }




                //return shops;
            }
        }

        
    }
}