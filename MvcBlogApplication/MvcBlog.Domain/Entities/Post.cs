using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcBlog.Domain.Entities
{
    public class Post
    {
        [HiddenInput]
        public int PostID { get; set; }

        public string PostTitle { get; set; }

        [DataType(DataType.MultilineText)]
        public string PostDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string PostContent { get; set; }

        public string PostCategory { get; set; }

        public string PostCreatedBy { get; set; }

        public DateTime? PostCreationDate { get; set; }

        public string PostLastModifiedBy { get; set; }

        public DateTime? PostLastModificationDate { get; set; }

        public bool PostIsVisible { get; set; }

        public bool PostCommentsEnabled { get; set; }
    }
}
