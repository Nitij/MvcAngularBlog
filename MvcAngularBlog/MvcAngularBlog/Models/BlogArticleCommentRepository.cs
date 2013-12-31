using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataAccess;
using Shared;
using Shared.Models;
namespace MvcAngularBlog.Models
{
    public class BlogArticleCommentRepository : IBlogArticleComment
    {
        private Dictionary<String, Object> operationParams = new Dictionary<String, Object>();
        private List<BlogArticle> articleComments = new List<BlogArticle>();
        private DataController dataController = new DataController();

        /// <summary>
        /// Constructor, empty!
        /// </summary>
        public BlogArticleCommentRepository() { }

        /// <summary>
        /// Get All Comments of a particular article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IEnumerable<ArticleComment> GetAllComments(Int32 articleId)
        {
            List<ArticleComment> comments = new List<ArticleComment>();
            operationParams.Clear();
            operationParams.Add("articleId", articleId);
            comments = dataController.ExecuteOperation(OperationType.GetAllComments, operationParams) as List<ArticleComment>;
            return comments;
        }

        /// <summary>
        /// Adds a new comment
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public Int32 AddNewComment(ArticleComment articleComment)
        {
            ArticleComment comment = new ArticleComment();
            Int32 retVal;
            if (comment == null)
                throw new ArgumentNullException("articleComment");

            operationParams.Clear();
            operationParams.Add("name", articleComment.Name);
            operationParams.Add("email", articleComment.Email);
            operationParams.Add("comment", articleComment.Comment);
            operationParams.Add("createDate", DateTime.Parse(articleComment.CreateDate));
            operationParams.Add("articleId", articleComment.ArticleID);

            retVal = Convert.ToInt32(dataController.ExecuteOperation(OperationType.AddNewComment, operationParams));

            return retVal;
        }

        /// <summary>
        /// Updates the specified article 
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public Int32 UpdateComment(ArticleComment articleComment)
        {
            Int32 retVal = 0;

            //fire an update sql statement on the DB table to update the comment 
            //by the data provided.

            return retVal;
        }

        /// <summary>
        /// Removes an article by its article id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Int32 RemoveComment(Int32 id)
        {
            Int32 retVal = 0;

            //add the code here to remove the comment from the DB table by the provided article id.

            return retVal;
        }
    }
}