using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared.Models;

namespace MvcAngularBlog.Models
{
    /// <summary>
    /// Interface for article comments.
    /// </summary>
    interface IBlogArticleComment
    {
        IEnumerable<ArticleComment> GetAllComments(Int32 articleId);
        Int32 AddNewComment(ArticleComment articleComment);
        Int32 RemoveComment(Int32 id);
        Int32 UpdateComment(ArticleComment articleComment);
    }
}
