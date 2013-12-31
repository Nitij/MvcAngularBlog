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
    public class BlogUserController : ApiController
    {
        static readonly IBlogUser blogUserRepository = new BlogUserRepository();

        /// <summary>
        /// Returns the current user
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogUser> GetCurrentUser()
        {
            List<BlogUser> blogUsers;
            blogUsers = blogUserRepository.GetCurrentUser() as List<BlogUser>;
            return blogUsers;
        }
    }
}
