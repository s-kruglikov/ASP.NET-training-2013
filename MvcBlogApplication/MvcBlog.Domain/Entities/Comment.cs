using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcBlog.Domain.Entities
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }

        [DataType(DataType.MultilineText)]
        public string CommentContent { get; set; }
    }
}
