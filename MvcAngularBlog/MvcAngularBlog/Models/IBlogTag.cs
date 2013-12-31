using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared.Models;

namespace MvcAngularBlog.Models
{
    interface IBlogTag
    {
        IEnumerable<BlogTag> GetAllTags();
        IEnumerable<BlogArticle> GetArticlesByTagName(String tagName);
    }
}
