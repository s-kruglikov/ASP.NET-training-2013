using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MvcBlog.Domain.Entities
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        [HiddenInput]
        [Display(Name = "Comment ID")]
        public int CommentID { get; set; }

        [HiddenInput]
        [Display(Name = "Post ID")]
        public int PostID { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment Content")]
        public string CommentContent { get; set; }

        [HiddenInput]
        [Display(Name = "Comment Created By")]
        public string CommentCreatedBy { get; set; }

        [HiddenInput]
        [Display(Name = "Comment Creation Date")]
        public DateTime? CommentCreationDate { get; set; }

        [HiddenInput]
        [Display(Name = "Comment Last Modified By")]
        public string CommentLastModifiedBy { get; set; }

        [HiddenInput]
        [Display(Name = "Comment Last Modification Date")]
        public DateTime? CommentLastModificationDate { get; set; }

        [Display(Name = "Is Visible?")]
        public bool CommentIsVisible { get; set; }
    }
}
