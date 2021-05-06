
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SpravRemontMobileApi.DataObjects
{
    public class Shop
    {
        //[JsonProperty("ID_shop")]
        public string ID_shop { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        //[JsonProperty("ID_Metro")]
        public Metro Metro { get; set; }
        //[JsonProperty("ID_Geo")]
        public Geo Geo { get; set; }
        public DateTime DateAdd { get; set; }
        public TimeWork TimeWork { get; set; }
        public string Note { get; set; }
        public bool BuyCard { get; set; }
        public int TimeWayMetro { get; set; }
        public string ID_City { get; set; }
        
        public double AVG_Star { get; set; }// рейтинг магазина по комментариям, дополнительный запрос
        public int Count_feedback { get; set; }// кол-во оставленных отзывов
        
        public string IMG_LOGO { get; set; }// URL link img
        public string IMG_DRIVE_TO { get; set; }
        public string IMG_MAP { get; set; }

        public string DISCONT_NOTE { get; set; }
        public bool DISCONT_CARD { get; set; }

        public string Descrip { get; set; }


        public List<IMG_Shop> iMG_Shops { get; set; }



    }
}