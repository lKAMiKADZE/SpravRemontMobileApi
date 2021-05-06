﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SpravRemontMobileApi.DataObjects
{
    public class Shop_short_Item
    {
        //[JsonProperty("ID_shop")]
        public string ID_shop { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Metro Metro { get; set; }
        
        public Geo Geo { get; set; }
        public DateTime DateAdd { get; set; }
        
        public string Note { get; set; }
        public bool BuyCard { get; set; }
        public int TimeWayMetro { get; set; }

        public double distanse { get; set; }


        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public double AVG_Star { get; set; }// рейтинг магазина по комментариям, дополнительный запрос
        public int Count_feedback { get; set; }// кол-во оставленных отзывов

        public string IMG_LOGO { get; set; }// URL link img

        public ITEM item { get; set; }//







    }
}