using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.WebUI.Models
{
    public class CommentViewModel
    {
        public int CommentID { get; set; }

        public int PostID { get; set; }

        [DataType(DataType.MultilineText)]
        public string CommentContent { get; set; }

        [HiddenInput]
        public string CommentCreatedBy { get; set; }

        public string Avatar { get; set; }

        [HiddenInput]
        public DateTime? CommentCreationDate { get; set; }
    }
}