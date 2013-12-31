using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBlog.WebUI.Infrastructure
{
    public static class DefaultConfig
    {
        #region default constants

        public const int DefaultPostsPerPage = 4;
        public const string DefaultAllowedTypes = "image/jpg;image/jpeg;image/png;image/gif";
        public const int DefaultMaxSize = 1024;
        public const int DefaultThumbHeight = 200;
        public const int DefaultThumbWidth = 300;
        public const string DefaultThumbPath = "PostsThumbs";
        public const int DefaultFeaturedHeight = 322;
        public const int DefaultFeaturedWidth = 940;
        public const string DefaultFeaturedPath = "FeaturedImages";
        public const int DefaultAvatarHeight = 90;
        public const int DefaultAvatarWidth = 90;
        public const string DefaultAvatarPath = "UsersAvatars";

        #endregion
    }
}