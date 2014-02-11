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
    public class BlogArticlesController : ApiController
    {
        //TODO: Calling new BlogArticleRepository() in the controller is not the best design,
        //because it ties the controller to a particular implementation of IBlogArticle. 
        //For a better approach, see Using the Web API Dependency Resolver.
        static readonly IBlogArticle blogArticleRepository = new BlogArticleRepository();

        /// <summary>
        /// Get All Blog Articles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogArticle> GetAllBlogArticles()
        {
            return blogArticleRepository.GetAllArticles();
        }

        /// <summary>
        /// Get Blog Article By Its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogArticle GetArticleByID(Int32 id)
        {
            return blogArticleRepository.GetArticle(id);
        }
    }
}
