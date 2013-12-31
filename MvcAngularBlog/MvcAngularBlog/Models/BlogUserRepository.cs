using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataAccess;
using Shared;
using Shared.Models;
namespace MvcAngularBlog.Models
{
    public class BlogUserRepository : IBlogUser
    {
        private Dictionary<String, Object> operationParams = new Dictionary<String, Object>(); //passing empty collection
        private DataController dataController = new DataController();

        /// <summary>
        /// Constructor
        /// </summary>
        public BlogUserRepository()
        {
            operationParams.Clear();
        }

        /// <summary>
        /// Returns the info of the current user
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogUser> GetCurrentUser()
        {
            List<BlogUser> blogUsers = new List<BlogUser>();
            Int32 userId;
            String userName = HttpContext.Current.User.Identity.Name;
            operationParams.Clear();
            operationParams.Add("userName", userName);
            userId = Convert.ToInt32(dataController.ExecuteOperation(OperationType.GetUserIdByUserName, operationParams));
            blogUsers.Add(new BlogUser { UserId = userId, UserName = userName });
            return blogUsers;
        }
    }
}