using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcBlog.Domain.Entities
{
    public class Post
    {
        public int PostID { get; set; }

        public string PostTitle { get; set; }

        [DataType(DataType.MultilineText)]
        public string PostDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string PostContent { get; set; }

        public string PostCategory { get; set; }
    }
}
