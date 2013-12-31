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
    public class UserArticlesController : ApiController
    {
        //TODO: Calling new BlogArticleRepository() in the controller is not the best design,
        //because it ties the controller to a particular implementation of IBlogArticleRepository. 
        //For a better approach, see Using the Web API Dependency Resolver.
        static readonly IBlogArticle blogArticleRepository = new BlogArticleRepository();

        /// <summary>
        /// Get all articles belonging to the current user
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogArticle> GetArticlesByUser()
        {
            return blogArticleRepository.GetArticlesByUser();
        }

        /// <summary>
        /// Updates an article
        /// </summary>
        /// <param name="article"></param>
        public void PutArticle(BlogArticle article)
        {
            blogArticleRepository.UpdateArticle(article);
        }

        /// <summary>
        /// Adds a new article
        /// </summary>
        /// <param name="article"></param>
        public BlogArticle PostArticle(BlogArticle article)
        {
            return blogArticleRepository.AddNewArticle(article);
        }

        /// <summary>
        /// Deletes an article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Int32 DeleteArticle(Int32 id)
        {
            return blogArticleRepository.RemoveArticle(id);
        }
    }
}
