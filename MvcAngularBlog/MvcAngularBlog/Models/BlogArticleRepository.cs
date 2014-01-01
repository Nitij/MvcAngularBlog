using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataAccess;
using Shared;
using Shared.Models;
namespace MvcAngularBlog.Models
{
    public class BlogArticleRepository : IBlogArticle
    {
        private Dictionary<String, Object> operationParams = new Dictionary<String, Object>(); //passing empty collection
        private List<BlogArticle> articles = new List<BlogArticle>();
        private DataController dataController = new DataController();

        /// <summary>
        /// Constructor
        /// </summary>
        public BlogArticleRepository()
        {
            operationParams.Clear();
        }

        /// <summary>
        /// Adds a new article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public BlogArticle AddNewArticle(BlogArticle article)
        {
            BlogArticle blogArticle;
            if (article == null)
                throw new ArgumentNullException("article");
            operationParams.Clear();
            operationParams.Add("title", article.Title);
            operationParams.Add("description", article.Description);
            operationParams.Add("data", article.Data);
            operationParams.Add("createDate", DateTime.Now);
            operationParams.Add("userName", article.UserName);
            operationParams.Add("tags", article.Tags);

            blogArticle = dataController.ExecuteOperation(OperationType.AddNewArticle, operationParams) as BlogArticle;
            return blogArticle;
        }

        /// <summary>
        /// Get All Articles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogArticle> GetAllArticles()
        {
            articles = dataController.ExecuteOperation(OperationType.ReadAllArticles, operationParams) as List<BlogArticle>;
            return articles;
        }

        /// <summary>
        /// Get the article data by the article id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<BlogArticle> GetArticle(Int32 id)
        {
            List<BlogArticle> articles = new List<BlogArticle>();
            operationParams.Clear();
            operationParams.Add("id", id);
            articles.Add(dataController.ExecuteOperation(OperationType.ReadArticleById, operationParams) as BlogArticle);

            return articles;
        }

        /// <summary>
        /// Returns all articles which have their create date in a given range
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<BlogArticle> GetArticlesByDateRange(ArchiveDate archiveDate)
        {
            List<BlogArticle> articles;
            Boolean endDateIsInNextYear = Convert.ToInt32(archiveDate.Month) == 12;
            DateTime startDate = DateTime.Parse(archiveDate.Month.ToString() + "/1/" + archiveDate.Year.ToString());
            String endMonth = ((!endDateIsInNextYear) ? (Convert.ToInt32(archiveDate.Month) + 1).ToString() : "1");
            String endYear = ((!endDateIsInNextYear) ? archiveDate.Year.ToString() : (Convert.ToInt32(archiveDate.Year) + 1).ToString());
            DateTime endDate = DateTime.Parse(endMonth + "/1/" + endYear);
            operationParams.Clear();
            operationParams.Add("startDate", startDate);
            operationParams.Add("endDate", endDate);
            articles = dataController.ExecuteOperation(OperationType.GetArticlesByDateRange, operationParams) as List<BlogArticle>;

            return articles;
        }

        /// <summary>
        /// Get all articles created by a particular user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<BlogArticle> GetArticlesByUser()
        {
            List<BlogArticle> articles;
            operationParams.Clear();
            operationParams.Add("userName", HttpContext.Current.User.Identity.Name);
            articles = dataController.ExecuteOperation(OperationType.GetArticlesByUser, operationParams) as List<BlogArticle>;

            return articles;
        }

        /// <summary>
        /// Returns a list of article create dates
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DateTime> GetArticleDates()
        {
            operationParams.Clear();
            return dataController.ExecuteOperation(OperationType.GetArticleDates, operationParams) as List<DateTime>;
        }

        /// <summary>
        /// Updates the specified article 
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public Int32 UpdateArticle(BlogArticle article)
        {
            Int32 retVal;

            operationParams.Clear();
            operationParams.Add("articleId", article.ID);
            operationParams.Add("title", article.Title);
            operationParams.Add("description", article.Description);
            operationParams.Add("data", article.Data);
            operationParams.Add("tags", article.Tags);

            retVal = Convert.ToInt32(dataController.ExecuteOperation(OperationType.UpdateArticleById, operationParams));
            return retVal;
        }

        /// <summary>
        /// Removes an article by its article id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Int32 RemoveArticle(Int32 id)
        {
            Int32 retVal;

            operationParams.Clear();
            operationParams.Add("articleId", id);
            retVal = Convert.ToInt32(dataController.ExecuteOperation(OperationType.DeleteArticle, operationParams));

            return retVal;
        }
    }
}