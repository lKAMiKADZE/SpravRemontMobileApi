using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontMobileApi.DataObjects
{
    public class CommentClient
    {
        public string ID_comment_client { get; set; }
        public string ID_shop { get; set; }
        public CommentShop Comment_shop { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Count_star { get; set; }
        public DateTime Date_add { get; set; }

    }
}