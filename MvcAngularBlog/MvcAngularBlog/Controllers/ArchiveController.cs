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
    public class ArchiveController : ApiController
    {
        //TODO: Calling new BlogArticleRepository() in the controller is not the best design,
        //because it ties the controller to a particular implementation of IBlogArticleRepository. 
        //For a better approach, see Using the Web API Dependency Resolver.
        static readonly IBlogArticle blogArticleRepository = new BlogArticleRepository();

        /// <summary>
        /// Returns the list of article create dates
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DateTime> GetArticleDates()
        {
            return blogArticleRepository.GetArticleDates();
        }

        /// <summary>
        /// Get all articles that have their create date in a particular range
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogArticle> PostGetArticlesByDateRange(ArchiveDate archiveDate)
        {
            return blogArticleRepository.GetArticlesByDateRange(archiveDate);
        }
    }
}
