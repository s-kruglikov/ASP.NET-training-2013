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

        public string CommentCreatedBy { get; set; }

        public DateTime CommentCreationDate { get; set; }

        public string CommentLastModifiedBy { get; set; }

        public DateTime CommentLastModificationDate { get; set; }

        public bool CommentIsVisible { get; set; }
    }
}
