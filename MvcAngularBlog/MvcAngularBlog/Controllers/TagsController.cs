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
    public class TagsController : ApiController
    {
        //TODO: Calling new BlogTagRepository() in the controller is not the best design,
        //because it ties the controller to a particular implementation of IBlogTag. 
        //For a better approach, see Using the Web API Dependency Resolver.
        static readonly IBlogTag blogTagRepository = new BlogTagRepository();

        /// <summary>
        /// Returns all blog tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogTag> GetAllBlogTags()
        {
            return blogTagRepository.GetAllTags();
        }

        /// <summary>
        /// Get all articles belonging to a particular tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public IEnumerable<BlogArticle> PostArticlesByTagName([FromBody] String tagName)
        {
            return blogTagRepository.GetArticlesByTagName(tagName);
        }

    }
}
