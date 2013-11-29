using MvcBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBlog.WebUI.Models
{
    public class PostsListViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public PagingModel PagingModel { get; set; }
    }
}