using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.Models
{
    public class BlogArticle
    {
        public Int32 ID {get; set; }
        public Int32 UserID { get; set; }
        public String UserName { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Data { get; set; }
        public DateTime CreateDate { get; set; }
        public String Tags { get; set; }
    }
}