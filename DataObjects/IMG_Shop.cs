using app20193Service.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.DataObjects
{
    public class IMG_Shop
    {
        public long ID_IMG_Shop { get; set; }
        public string ID_SHOP { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public bool Deleted { get; set; }
        public DateTime Date_add { get; set; }






        public static List<IMG_Shop> GetShopIMGs(string ID_SHOP)
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