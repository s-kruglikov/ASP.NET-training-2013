using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcBlog.WebUI.Models
{
    public class SiteConfigViewModel
    {
        [Display(Name = "Number of posts per page")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? PostsPerPage { get; set; }

        [Display(Name = "Acceptable image types")]
        public string AllowedImageTypes { get; set; }

        [Display(Name = "Max allowed image size (in kilobytes)")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? MaxImageSize { get; set; }

        [Display(Name = "Post thumbnail image height")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? PostThumbImageHeight { get; set; }

        [Display(Name = "Post thumbnail image width")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? PostThumbImageWidth { get; set; }

        [Display(Name = "Post thumbnail images folder")]
        public string PostThumbPath { get; set; }

        [Display(Name = "Post featured image height")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? PostFeaturedImageHeight { get; set; }

        [Display(Name = "Post featured image width")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? PostFeaturedImageWidth { get; set; }

        [Display(Name = "Post featured images folder")]
        public string PostFeaturedPath { get; set; }

        [Display(Name = "Avatar image height")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? AvatarImageHeight { get; set; }

        [Display(Name = "Avatar image width")]
        
        public int? AvatarImageWidth { get; set; }

        [Display(Name = "Avatar image folder")]
        public string AvatarImagePath { get; set; }
    }
}