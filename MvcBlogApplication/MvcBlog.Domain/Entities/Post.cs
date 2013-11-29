using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcBlog.Domain.Entities
{
    public class Post
    {
        public int PostID { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public string PostContent { get; set; }
        public string PostCategory { get; set; }
    }
}
