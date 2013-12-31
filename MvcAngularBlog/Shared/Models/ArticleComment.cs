using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.Models
{
    public class ArticleComment
    {
        public Int32 ID {get; set; }
        public Int32 ArticleID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Comment { get; set; }
        public String CreateDate { get; set; }
    }
}