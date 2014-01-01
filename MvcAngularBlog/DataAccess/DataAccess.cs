using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

using Shared;
using Shared.Models;

namespace DataAccess
{
    public class DataManager
    {
        private String connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        #region Sql Methods

        /// <summary>
        /// Returns a new sql parameter with the given paramenters.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        private SqlParameter GetParameter(String parameterName, Object value, DbType dbtype)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.Value = value;
            param.DbType = dbtype;
            return param;
        }

        /// <summary>
        /// Returns the connection string.
        /// </summary>
        /// <returns></returns>
        private String GetConnectionString()
        {
            return connectionString;
        }

        /// <summary>
        /// Returns a new sql connection.
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Closes the provided sql connection.
        /// </summary>
        /// <param name="connection"></param>
        private void CloseConnection(SqlConnection connection)
        {
            connection.Close();
        }

        /// <summary>
        /// Returns the SqlCommand object based on the type of operation passed
        /// </summary>
        /// <param name="operationType"></param>
        /// <returns></returns>
        private SqlCommand GetCommand(String operationType)
        {
            String dataQuery = OperationType.Map[operationType];
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand(dataQuery, connection);

            return command;
        }

        #endregion Sql Methods

        #region CRUD

        #region Blog Comments

        /// <summary>
        /// Read All Comments of a particular article
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public List<ArticleComment> ReadAllComments(Dictionary<String, Object> data)
        {
            ArticleComment comment;
            List<ArticleComment> returnData = new List<ArticleComment>();
            SqlDataReader dataReader;
            SqlCommand command = GetCommand(OperationType.GetAllComments);
            Int32 articleId = Convert.ToInt32(data["articleId"]);

            command.Parameters.Add(GetParameter("@articleId", articleId, DbType.Int32));
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                comment = GetArticleComment((Int32)dataReader["CommentId"],
                                dataReader["Name"].ToString(),
                                dataReader["Email"].ToString(),
                                dataReader["Comment"].ToString(),
                                dataReader["CreateDate"].ToString());
                returnData.Add(comment);
            }
            CloseConnection(command.Connection);
            return returnData;
        }

        /// <summary>
        /// Adds a new comment
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Int32 AddNewComment(Dictionary<String, Object> data)
        {
            Int32 returnData, commentId, articleId;
            SqlCommand command = GetCommand(OperationType.AddNewComment);
            String name = (data["name"]).ToString();
            String email = (data["email"]).ToString();
            String comment = (data["comment"]).ToString();
            DateTime createDate = Convert.ToDateTime(data["createDate"]);

            command.Parameters.Add(GetParameter("@name", name, DbType.String));
            command.Parameters.Add(GetParameter("@email", email, DbType.String));
            command.Parameters.Add(GetParameter("@comment", comment, DbType.String));
            command.Parameters.Add(GetParameter("@createDate", createDate, DbType.DateTime));

            commentId = Convert.ToInt32(command.ExecuteScalar());
            CloseConnection(command.Connection);
            //new add a new record into the Article-Comment relationship table
            articleId = Convert.ToInt32(data["articleId"]);
            command = GetCommand(OperationType.AddCommentArticleRelation);

            command.Parameters.Add(GetParameter("@articleId", articleId, DbType.Int32));
            command.Parameters.Add(GetParameter("@commentId", commentId, DbType.Int32));

            returnData = command.ExecuteNonQuery();

            CloseConnection(command.Connection);
            return returnData;
        }

        #endregion Blog Comments

        #region Articles

        /// <summary>
        /// Returns all articles which have their create date in a given range
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<BlogArticle> ReadArticleByDateRange(Dictionary<String, Object> data)
        {
            BlogArticle articleModel;
            List<BlogArticle> returnData = new List<BlogArticle>();
            SqlDataReader dataReader;
            SqlCommand command = GetCommand(OperationType.GetArticlesByDateRange);
            DateTime startDate = DateTime.Parse(data["startDate"].ToString());
            DateTime endDate = DateTime.Parse(data["endDate"].ToString());

            command.Parameters.Add(GetParameter("@startDate", startDate, DbType.String));
            command.Parameters.Add(GetParameter("@endDate", endDate, DbType.String));
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                articleModel = GetBlogArticle((Int32)dataReader["ID"],
                                Convert.ToInt32(dataReader["UserId"]),
                                dataReader["UserName"].ToString(),
                                dataReader["Title"].ToString(),
                                dataReader["Description"].ToString(),
                                dataReader["Data"].ToString(),
                                Convert.ToDateTime(dataReader["CreateDate"]),
                                String.Join(",", GetArticleTags((Int32)dataReader["ID"]).ToArray()));
                returnData.Add(articleModel);
            }
            CloseConnection(command.Connection);
            return returnData;
        }

        /// <summary>
        /// Retruns all the create dates of all articles in a list format
        /// </summary>
        /// <returns></returns>
        public List<DateTime> GetArticleDates()
        {
            List<DateTime> returnData = new List<DateTime>();
            SqlDataReader dataReader;
            SqlCommand command = GetCommand(OperationType.GetArticleDates);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                returnData.Add(DateTime.Parse(dataReader["Create_Date"].ToString()));
            }
            CloseConnection(command.Connection);
            return returnData;
        }

        /// <summary>
        /// Removes all article-tag rows for an article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public Int32 RemoveArticleTagRelation(Int32 articleId)
        {
            Int32 returnData = 0;
            SqlCommand command = GetCommand(OperationType.RemoveArticleTagRelationByArticleID);

            command.Parameters.Add(GetParameter("@articleId", articleId, DbType.Int32));
            returnData = command.ExecuteNonQuery();
            CloseConnection(command.Connection);

            return returnData;
        }

        /// <summary>
        /// Set tags for the specified article
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Int32 SetArticleTags(Dictionary<String, Object> data)
        {
            Int32 returnData = 0;
            SqlDataReader dataReader;
            List<String> tagList = new List<String>();
            SqlCommand command, selectTagCommand;
            Int32 articleId = Convert.ToInt32(data["articleId"]);
            String tags = (data["tags"]).ToString();

            //we dont want to insert empty tags
            if (tags == ",") tags = String.Empty;

            //First we need to remove all tags for the given article ID
            RemoveArticleTagRelation(articleId);

            //Now we need to add any tag which does not already exist in the DB
            //code to convert comma separated string to list
            //http://stackoverflow.com/questions/910119/how-to-create-a-listt-from-a-comma-separated-string
            if (tags.Length > 0)
                tagList.AddRange(tags.Split(',').Select(i => i));

            //make sure we have distinct tags
            tagList = tagList.Distinct().ToList();
            selectTagCommand = GetCommand(OperationType.SelectTag);
            command = GetCommand(OperationType.AddNewTag);
            foreach (String str in tagList)
            {
                selectTagCommand.Parameters.Add(GetParameter("@tagName", str, DbType.String));
                dataReader = selectTagCommand.ExecuteReader();
                //if the tag does not exist in the DB then add the tag
                if (!dataReader.Read())
                {
                    command.Parameters.Add(GetParameter("@tagName", str, DbType.String));
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                dataReader.Close();
                selectTagCommand.Parameters.Clear();
            }
            CloseConnection(command.Connection);
            CloseConnection(selectTagCommand.Connection);

            //Now set the Article-Tag relationship for each tag passed
            command = GetCommand(OperationType.AddArticleTagRelation);
            foreach (String str in tagList)
            {
                command.Parameters.Add(GetParameter("@tagName", str, DbType.String));
                command.Parameters.Add(GetParameter("@articleId", articleId, DbType.Int32));
                command.ExecuteNonQuery();
                returnData++;
                command.Parameters.Clear();
            }

            CloseConnection(command.Connection);
            return returnData;
        }

        /// <summary>
        /// Adds a new article
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BlogArticle AddNewArticle(Dictionary<String, Object> data)
        {
            BlogArticle blogArticle;
            Int32 articleId, userId;
            SqlCommand command = GetCommand(OperationType.AddNewArticle);
            String title = (data["title"]).ToString();
            String description = (data["description"]).ToString();
            String articleData = (data["data"]).ToString();
            String userName = (data["userName"]).ToString();
            DateTime createDate = Convert.ToDateTime(data["createDate"]);
            blogArticle = new BlogArticle
            {
                Title = title,
                Description = description,
                Data = articleData,
                CreateDate = createDate,
                UserName = userName
            };

            command.Parameters.Add(GetParameter("@title", title, DbType.String));
            command.Parameters.Add(GetParameter("@description", description, DbType.String));
            command.Parameters.Add(GetParameter("@data", articleData, DbType.String));
            command.Parameters.Add(GetParameter("@createDate", createDate, DbType.DateTime));

            //get our new article's id now
            articleId = Convert.ToInt32(command.ExecuteScalar());
            CloseConnection(command.Connection);
            blogArticle.ID = articleId;

            //now lets get the user id
            command = GetCommand(OperationType.GetUserIdByUserName);
            command.Parameters.Add(GetParameter("@userName", userName, DbType.String));
            userId = Convert.ToInt32(command.ExecuteScalar());
            CloseConnection(command.Connection);
            blogArticle.UserID = userId;

            //new add a new record into the Article-User relationship table
            command = GetCommand(OperationType.AddArticleUserRelation);
            command.Parameters.Add(GetParameter("@userId", userId, DbType.Int32));
            command.Parameters.Add(GetParameter("@articleId", articleId, DbType.Int32));
            command.ExecuteNonQuery();

            //we got this far so this means everything is good to go
            CloseConnection(command.Connection);

            //add the new article Id to the data dictionary
            data.Add("articleId", articleId);

            //now lets update the tags
            SetArticleTags(data);

            return blogArticle;
        }

        /// <summary>
        /// Read article by its ID
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BlogArticle ReadArticleById(Dictionary<String, Object> data)
        {
            BlogArticle articleModel;
            SqlDataReader dataReader;
            BlogArticle returnData = new BlogArticle();
            SqlCommand command = GetCommand(OperationType.ReadArticleById);
            Int32 id = Convert.ToInt32(data["id"]);

            command.Parameters.Add(GetParameter("@articleId", id, DbType.Int32));
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                articleModel = GetBlogArticle((Int32)dataReader["ID"],
                                Convert.ToInt32(dataReader["UserId"]),
                                dataReader["UserName"].ToString(),
                                dataReader["Article_Title"].ToString(),
                                dataReader["Article_Description"].ToString(),
                                dataReader["Article_Data"].ToString(),
                                Convert.ToDateTime(dataReader["Create_Date"]),
                                String.Join(",", GetArticleTags(id).ToArray()));

                returnData = articleModel;
            }
            CloseConnection(command.Connection);
            return returnData;
        }

        /// <summary>
        /// Read All Articles
        /// </summary>
        /// <returns></returns>
        public List<BlogArticle> ReadAllArticles()
        {
            BlogArticle articleModel;
            List<BlogArticle> returnData = new List<BlogArticle>();
            SqlDataReader dataReader;
            SqlCommand command = GetCommand(OperationType.ReadAllArticles);

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                articleModel = GetBlogArticle((Int32)dataReader["ID"],
                                0, //empty user id
                                "",// empty user name
                                dataReader["Article_Title"].ToString(),
                                dataReader["Article_Description"].ToString(),
                                dataReader["Article_Data"].ToString(),
                                Convert.ToDateTime(dataReader["Create_Date"]),
                                String.Join(",", GetArticleTags((Int32)dataReader["ID"]).ToArray()));
                returnData.Add(articleModel);
            }
            CloseConnection(command.Connection);
            return returnData;
        }

        /// <summary>
        /// Read All Articles by User Name
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<BlogArticle> ReadArticlesByUserName(Dictionary<String, Object> data)
        {
            BlogArticle articleModel;
            List<BlogArticle> returnData = new List<BlogArticle>();
            SqlDataReader dataReader;
            SqlCommand command = GetCommand(OperationType.GetArticlesByUser);
            String userName = data["userName"].ToString();

            command.Parameters.Add(GetParameter("@userName", userName, DbType.String));
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                articleModel = GetBlogArticle((Int32)dataReader["ID"],
                                Convert.ToInt32(dataReader["UserId"]),
                                dataReader["UserName"].ToString(),
                                dataReader["Title"].ToString(),
                                dataReader["Description"].ToString(),
                                dataReader["Data"].ToString(),
                                Convert.ToDateTime(dataReader["CreateDate"]),
                                String.Join(",", GetArticleTags((Int32)dataReader["ID"]).ToArray()));
                returnData.Add(articleModel);
            }
            CloseConnection(command.Connection);
            return returnData;
        }

        /// <summary>
        /// Get all tags of an article.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public List<String> GetArticleTags(Int32 articleId)
        {
            List<String> returnValue = new List<String>();
            SqlDataReader dataReader;
            SqlCommand command = GetCommand(OperationType.GetArticleTags);
            command.Parameters.Add(GetParameter("@articleId", articleId, DbType.Int32));
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                returnValue.Add(dataReader["TagName"].ToString());
            }
            return returnValue;
        }

        /// <summary>
        /// Updates an article by using the Id provided.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Int32 UpdateArticleById(Dictionary<String, Object> data)
        {
            Int32 returnData;
            SqlCommand command = GetCommand(OperationType.UpdateArticleById);
            Int32 id = Convert.ToInt32(data["articleId"]);
            String title = data["title"].ToString();
            String description = data["description"].ToString();
            String articleData = data["data"].ToString();

            command.Parameters.Add(GetParameter("@articleId", id, DbType.Int32));
            command.Parameters.Add(GetParameter("@title", title, DbType.String));
            command.Parameters.Add(GetParameter("@description", description, DbType.String));
            command.Parameters.Add(GetParameter("@data", articleData, DbType.String));
            returnData = command.ExecuteNonQuery();

            CloseConnection(command.Connection);

            //now lets update the tags
            SetArticleTags(data);
            return returnData;
        }

        /// <summary>
        /// Deletes an article
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Int32 DeleteArticleById(Dictionary<String, Object> data)
        {
            Int32 returnData;
            SqlCommand command;
            Int32 id = Convert.ToInt32(data["articleId"]);

            command = GetCommand(OperationType.DeleteArticleUserRelation);
            command.Parameters.Add(GetParameter("@articleId", id, DbType.Int32));
            returnData = command.ExecuteNonQuery();
            CloseConnection(command.Connection);

            //Now we need to remove all the article-tag relaton rows
            RemoveArticleTagRelation(id);

            //only proceed if the article-user relation record was deleted
            if (returnData > 0)
            {
                command = GetCommand(OperationType.DeleteArticle);
                command.Parameters.Add(GetParameter("@articleId", id, DbType.Int32));
                returnData = command.ExecuteNonQuery();
            }

            CloseConnection(command.Connection);
            return returnData;
        }

        /// <summary>
        /// Read Articles By Tag Name
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<BlogArticle> ReadArticlesByTagName(Dictionary<String, Object> data)
        {
            BlogArticle articleModel;
            List<BlogArticle> returnData = new List<BlogArticle>();
            SqlDataReader dataReader;
            SqlCommand command = GetCommand(OperationType.GetArticlesByTagName);
            String tagName = data["tagName"].ToString();

            command.Parameters.Add(GetParameter("@tagName", tagName, DbType.String));
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                articleModel = GetBlogArticle((Int32)dataReader["ID"],
                                Convert.ToInt32(dataReader["UserId"]),
                                dataReader["UserName"].ToString(),
                                dataReader["Title"].ToString(),
                                dataReader["Description"].ToString(),
                                dataReader["Data"].ToString(),
                                Convert.ToDateTime(dataReader["CreateDate"]),
                                String.Join(",", GetArticleTags((Int32)dataReader["ID"]).ToArray()));
                returnData.Add(articleModel);
            }
            CloseConnection(command.Connection);
            return returnData;
        }

        #endregion Articles

        #region User

        /// <summary>
        /// Gets the user id from the user name
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Int32 GetUserId(Dictionary<String, Object> data)
        {
            Int32 returnValue;
            String userName = (data["userName"]).ToString();
            SqlCommand command = GetCommand(OperationType.GetUserIdByUserName);
            command.Parameters.Add(GetParameter("@userName", userName, DbType.String));
            returnValue = Convert.ToInt32(command.ExecuteScalar());

            CloseConnection(command.Connection);
            return returnValue;
        }

        #endregion User

        #region Tags

        /// <summary>
        /// Returns all tags with their article count
        /// </summary>
        /// <returns></returns>
        public List<BlogTag> GetAllTags()
        {
            BlogTag blogTag;
            List<BlogTag> returnData = new List<BlogTag>();
            SqlDataReader dataReader;
            SqlCommand command = GetCommand(OperationType.GetAllTags);

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                blogTag = GetBlogTag(dataReader["TagName"].ToString(),
                                (Int32)dataReader["ArticleCount"]);
                returnData.Add(blogTag);
            }
            CloseConnection(command.Connection);
            return returnData;
        }

        #endregion Tags

        #endregion CRUD

        #region Helper Methods

        /// <summary>
        /// Returns new ArticleComment object with the passed parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="comment"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        private ArticleComment GetArticleComment(Int32 id, String name, String email, String comment, String createDate)
        {
            return new ArticleComment
            {
                ID = id,
                Name = name,
                Email = email,
                Comment = comment,
                CreateDate = createDate
            };
        }

        /// <summary>
        /// Returns new BlogArticle object with the passed parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="data"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        private BlogArticle GetBlogArticle(Int32 id, Int32 userId, String userName, String title, String description, String data, DateTime createDate, String tags)
        {
            return new BlogArticle
            {
                ID = id,
                UserID = userId,
                UserName = userName,
                Title = title,
                Description = description,
                Data = data,
                CreateDate = createDate,
                Tags = tags
            };
        }

        /// <summary>
        /// Retruns a new instance of BlogTag object with the passed parameters
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="articleCount"></param>
        /// <returns></returns>
        private BlogTag GetBlogTag(String tagName, Int32 articleCount)
        {
            return new BlogTag
            {
                TagName = tagName,
                ArticleCount = articleCount
            };
        }

        #endregion Helper Methods
    }
}