using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataAccess;
using Shared;
using Shared.Models;
namespace MvcAngularBlog.Models
{
    public class BlogTagRepository : IBlogTag
    {
        private Dictionary<String, Object> operationParams = new Dictionary<String, Object>(); //passing empty collection
        private DataController dataController = new DataController();

        /// <summary>
        /// Constructor
        /// </summary>
        public BlogTagRepository()
        {
            operationParams.Clear();
        }

        /// <summary>
        /// Returns all blog tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogTag> GetAllTags()
        {
            List<BlogTag> blogtags = new List<BlogTag>();
            operationParams.Clear();
            blogtags = dataController.ExecuteOperation(OperationType.GetAllTags, operationParams) as List<BlogTag>;
            return blogtags;
        }

        /// <summary>
        /// Get all articles belonging to a particular tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public IEnumerable<BlogArticle> GetArticlesByTagName(String tagName)
        {
            List<BlogArticle> blogArticles = new List<BlogArticle>();
            operationParams.Clear();
            operationParams.Add("tagName", tagName);
            blogArticles = dataController.ExecuteOperation(OperationType.GetArticlesByTagName, operationParams) as List<BlogArticle>;
            return blogArticles;
        }
    }
}