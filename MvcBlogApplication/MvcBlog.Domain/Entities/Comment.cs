using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcBlog.Domain.Entities
{
    public class Comment
    {
        [HiddenInput]
        public int CommentID { get; set; }

        [HiddenInput]
        public int PostID { get; set; }

        [DataType(DataType.MultilineText)]
        public string CommentContent { get; set; }

        [HiddenInput]
        public string CommentCreatedBy { get; set; }

        [HiddenInput]
        public DateTime CommentCreationDate { get; set; }

        [HiddenInput]
        public string CommentLastModifiedBy { get; set; }

        [HiddenInput]
        public DateTime CommentLastModificationDate { get; set; }

        public bool CommentIsVisible { get; set; }
    }
}
