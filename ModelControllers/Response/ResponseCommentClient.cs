using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SpravRemontMobileApi.Constant;
using SpravRemontMobileApi.DataObjects;
using SpravRemontMobileApi.ModelControllers.Request;
using Newtonsoft.Json;

namespace SpravRemontMobileApi.ModelControllers.Response
{
    public class ResponseCommentClient
    {
        
        public List<CommentClient> Comments { get; set; }

        public ResponseCommentClient()
        {
           // Comment_client = new List<CommentClient>();
        }

        public  void GetCommentShop(string connectionString, string id_shop)
        {
            Comments = new List<CommentClient>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlExpression = @"SELECT 
                    COMCL.ID_comment_client,
                    COMCL.ID_shop,
                    COMCL.Email,
                    COMCL.Name,
                    COMCL.Comment,
                    COMCL.Count_star,
                    COMCL.Date_add,
                    CS.ID_comment_shop,
                    CS.Comment AS comment_shop,
                    CS.Date_add AS Date_add_shop

                        FROM SPAVREMONT.Comment_Client COMCL
                    LEFT JOIN SPAVREMONT.Comment_shop CS ON CS.id_comment_shop=COMCL.id_comment_shop
                    WHERE COMCL.id_shop='" + id_shop+ @"'
                        AND COMCL.DELETED=0
                        AND COMCL.VISIBLE=1
                    ORDER BY COMCL.Date_add DESC
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

                    int ccID_comment_clientIndex = reader.GetOrdinal("ID_comment_client");
                    int ccID_shopIndex = reader.GetOrdinal("ID_shop");
                    int ccEmailIndex = reader.GetOrdinal("Email");
                    int ccNameIndex = reader.GetOrdinal("Name");
                    int ccCommentIndex = reader.GetOrdinal("Comment");
                    int ccCount_starIndex = reader.GetOrdinal("Count_star");
                    int ccDate_addIndex = reader.GetOrdinal("Date_add");
                    int csID_comment_shopIndex = reader.GetOrdinal("ID_comment_shop");
                    int csCommentShopIndex = reader.GetOrdinal("comment_shop");
                    int csDate_add_shopIndex = reader.GetOrdinal("Date_add_shop");




                    while (reader.Read()) // построчно считываем данные
                    {
                        CommentShop comment_shop = new CommentShop
                        {
                            ID_comment_shop = reader.IsDBNull(csID_comment_shopIndex) ? null : reader.GetString(csID_comment_shopIndex),
                            Comment= reader.IsDBNull(csCommentShopIndex) ? null : reader.GetString(csCommentShopIndex)                            
                        };

                        if (!reader.IsDBNull(csDate_add_shopIndex))// если дата существует в запросе
                            comment_shop.Date_add = reader.GetDateTime(csDate_add_shopIndex);

                        CommentClient itemCommentClient = new CommentClient
                        {
                            Comment = reader.IsDBNull(ccCommentIndex) ? null : reader.GetString(ccCommentIndex),
                            Comment_shop = comment_shop,
                            Count_star = reader.IsDBNull(ccCount_starIndex) ? 0 : reader.GetByte(ccCount_starIndex),
                            Date_add = reader.GetDateTime(ccDate_addIndex),
                            //Email= reader.IsDBNull(ccCommentIndex) ? null : reader.GetString(ccCommentIndex),// аноним для всех, виден только для магазина
                            ID_comment_client = reader.IsDBNull(ccID_comment_clientIndex) ? null : reader.GetString(ccID_comment_clientIndex),
                            ID_shop = reader.IsDBNull(ccID_shopIndex) ? null : reader.GetString(ccID_shopIndex),
                            Name = reader.IsDBNull(ccNameIndex) ? null : reader.GetString(ccNameIndex)
                        };

                        Comments.Add(itemCommentClient);

                    }
                }




                //return shops;
            }
        }



        public string SetCommentShop(string connectionString, RequestClientComment req)
        {

            if (req.ID_shop == null ||
                req.Comment == null ||
                req.Email == null ||
                req.Name == null ||
                req.Count_star==0
                )
                return "Не все поля заполнены";
        
                

            string sqlExpression = @"INSERT INTO SPAVREMONT.Comment_Client
                (
                ID_comment_client,
                ID_shop,
                ID_comment_shop,
                Email,
                Name,
                Comment,
                Count_star,
                Date_add,
                Visible,
                Deleted
                )
                VALUES(
                '" + Guid.NewGuid().ToString() + @"',
                '" + req.ID_shop + @"',
                NULL,
                '" + req.Email + @"',
                '" + req.Name + @"',
                '" + req.Comment + @"',
                " + req.Count_star.ToString() + @",
                Current_timestamp,
                1,
                0
                )
                
                ";

            ////////////////////
            //TEST для теста параметр  Visible=1
            //PROD данный параметр должен быть 0
            ///одобрять комментарий могут владельцы магазинов
            ////////////////////

            int number = 0;

            SqlConnection connection = new SqlConnection(Constants.connectDB);
            
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                number = command.ExecuteNonQuery();
            


            if (number > 0)
                return Constants.RESULT_OK;
            else
                return Constants.RESULT_ERR;

            
        }
    }
}