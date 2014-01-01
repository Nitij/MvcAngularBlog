using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared.Models;

namespace MvcAngularBlog.Models
{
    interface IBlogArticle
    {
        IEnumerable<BlogArticle> GetAllArticles();
        IEnumerable<BlogArticle> GetArticle(Int32 id);
        IEnumerable<BlogArticle> GetArticlesByUser();
        IEnumerable<DateTime> GetArticleDates();
        IEnumerable<BlogArticle> GetArticlesByDateRange(ArchiveDate archiveDate);
        BlogArticle AddNewArticle(BlogArticle article);
        Int32 RemoveArticle(Int32 id);
        Int32 UpdateArticle(BlogArticle article);
    }
}
