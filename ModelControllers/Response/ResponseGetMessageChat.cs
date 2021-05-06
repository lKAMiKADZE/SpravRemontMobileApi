using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SpravRemontMobileApi.DataObjects;


namespace SpravRemontMobileApi.ModelControllers
{
    public class ResponseGetMessageChat
    {
       public List<Messages> Messages { get; set; }



        public void GetLastMessage(string connectionString, RequestGetMessageChat req)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Messages = new List<Messages>();

                string sqlExpression = @"SELECT 
                    ID_message,
                    ID_from,
                    ID_to,
                    Message,
                    ID_type_message,
                    Date_send,
                    new_message  
                     FROM messages 
                    WHERE ((ID_from='" + req.ID_Client + @"' AND ID_TO='" + req.ID_Shop + @"') OR
                    (ID_from='" + req.ID_Shop + @"' AND ID_TO='" + req.ID_Client + @"'))
                     AND date_send > DATEADD(hour, -15, getdate()) 

                    ORDER BY date_send ASC -- 
                     
                     
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



                    int ID_message_index = reader.GetOrdinal("ID_message");
                    int ID_from_index = reader.GetOrdinal("ID_from");
                    int ID_to_index = reader.GetOrdinal("ID_to");
                    int Message_index = reader.GetOrdinal("Message");
                    int ID_type_message_index = reader.GetOrdinal("ID_type_message");
                    int Date_send_index = reader.GetOrdinal("Date_send");
                    int new_message_index = reader.GetOrdinal("new_message");


                    while (reader.Read()) // построчно считываем данные
                    {
                        Messages.Add(new Messages()
                        {
                            ID_from = reader.GetString(ID_from_index),
                            Date_send = reader.GetDateTime(Date_send_index),
                            ID_message = reader.GetString(ID_message_index),
                            ID_to = reader.GetString(ID_to_index),
                            ID_type_message = reader.GetByte(ID_type_message_index),
                            Message = reader.GetString(Message_index),
                            new_message = reader.GetBoolean(new_message_index)
                        });

                        
                    }
                }




                //return shops;
            }
        }
    }
}