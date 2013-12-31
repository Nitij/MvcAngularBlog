using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MvcAngularBlog.Models;
using Shared.Models;

namespace MvcAngularBlog.Controllers
{
    public class BlogCommentsController : ApiController
    {
        static readonly IBlogArticleComment blogArticleCommentRepository = new BlogArticleCommentRepository();

        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="articleComment"></param>
        public void PostComment(ArticleComment articleComment)
        {
            blogArticleCommentRepository.AddNewComment(articleComment);
        }

        /// <summary>
        /// Get all comments of a particular article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IEnumerable<ArticleComment> GetAllComments(Int32 id)
        {
            List<ArticleComment> comments = new List<ArticleComment>();
            comments = blogArticleCommentRepository.GetAllComments(id) as List<ArticleComment>;
            return comments;
        }
    }
}
