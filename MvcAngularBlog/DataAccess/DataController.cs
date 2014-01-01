using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared;

namespace DataAccess
{
    public class DataController
    {
        #region Private Variables
        private DataManager manager;
        #endregion Private Variables

        #region Constructer
        /// <summary>
        /// Constructer
        /// </summary>
        public DataController()
        {
            manager = new DataManager();
        }
        #endregion Constructer

        /// <summary>
        /// Execute Operation
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Object ExecuteOperation(String operationType, Dictionary<String, Object> data)
        {
            Object retVal = new Object();

            //Implementing a global try-catch block, never use this shortcut for production version 
            try
            {
                switch (operationType)
                {
                    case OperationType.ReadAllArticles:
                        retVal = manager.ReadAllArticles();
                        break;
                    case OperationType.ReadArticleById:
                        retVal = manager.ReadArticleById(data);
                        break;
                    case OperationType.AddNewComment:
                        retVal = manager.AddNewComment(data);
                        break;
                    case OperationType.GetAllComments:
                        retVal = manager.ReadAllComments(data);
                        break;
                    case OperationType.GetArticlesByUser:
                        retVal = manager.ReadArticlesByUserName(data);
                        break;
                    case OperationType.UpdateArticleById:
                        retVal = manager.UpdateArticleById(data);
                        break;
                    case OperationType.AddNewArticle:
                        retVal = manager.AddNewArticle(data);
                        break;
                    case OperationType.GetUserIdByUserName:
                        retVal = manager.GetUserId(data);
                        break;
                    case OperationType.DeleteArticle:
                        retVal = manager.DeleteArticleById(data);
                        break;
                    case OperationType.GetAllTags:
                        retVal = manager.GetAllTags();
                        break;
                    case OperationType.GetArticlesByTagName:
                        retVal = manager.ReadArticlesByTagName(data);
                        break;
                    case OperationType.GetArticleDates:
                        retVal = manager.GetArticleDates();
                        break;
                    case OperationType.GetArticlesByDateRange:
                        retVal = manager.ReadArticleByDateRange(data);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(String.Concat(Exceptions.Error, ": ", exception.Message));
            }
            return retVal;
        }
    }
}
