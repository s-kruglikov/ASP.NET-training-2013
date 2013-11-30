using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBlog.Domain.Entities;

namespace MvcBlog.WebUI.Models
{
    public class PostDetailedModel
    {
        public Post PostDetailed { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}