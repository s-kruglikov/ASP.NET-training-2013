using System.Globalization;
using MvcBlog.WebUI.Abstract;
using System.Configuration;
using MvcBlog.WebUI.Infrastructure;

namespace MvcBlog.WebUI.Concrete
{
    public class ConfigService : IConfigService
    {
        public int PostsPerPage
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostsPerPage"]); }
            set
            {
                if (value != default(int))
                    ConfigurationManager.AppSettings["PostsPerPage"] = value.ToString(CultureInfo.InvariantCulture);
                else ConfigurationManager.AppSettings["PostsPerPage"] = DefaultConfig.DefaultPostsPerPage.ToString(CultureInfo.InvariantCulture);
            }
        }

        public string AllowedImageTypes
        {
            get
            {
                return ConfigurationManager.AppSettings["AllowedMimeTypes"];
            }
            set
            {
                if (!string.IsNullOrEmpty(value)) ConfigurationManager.AppSettings["AllowedMimeTypes"] = value;
                else ConfigurationManager.AppSettings["AllowedMimeTypes"] = DefaultConfig.DefaultAllowedTypes;
            }
        }

        public int MaxImageSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["MaxImageSize"]);
            }

            set
            {
                if (value != default(int))
                    ConfigurationManager.AppSettings["MaxImageSize"] = value.ToString(CultureInfo.InvariantCulture);
                else ConfigurationManager.AppSettings["MaxImageSize"] = DefaultConfig.DefaultMaxSize.ToString(CultureInfo.InvariantCulture);
            }
        }

        #region Thumbnails
        public int PostThumbImageHeight
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostThumbImageHeight"]); }
            set
            {
                if (value != default(int))
                    ConfigurationManager.AppSettings["PostThumbImageHeight"] =
                        value.ToString(CultureInfo.InvariantCulture);
                else ConfigurationManager.AppSettings["PostThumbImageHeight"] = DefaultConfig.DefaultThumbHeight.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int PostThumbImageWidth
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostThumbImageWidth"]); }
            set
            {
                if (value != default(int))
                    ConfigurationManager.AppSettings["PostThumbImageWidth"] =
                        value.ToString(CultureInfo.InvariantCulture);
                else
                    ConfigurationManager.AppSettings["PostThumbImageWidth"] =
                        DefaultConfig.DefaultThumbWidth.ToString(CultureInfo.InvariantCulture);
            }
        }

        public string PostThumbPath
        {
            get { return ConfigurationManager.AppSettings["PostThumbPath"]; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    ConfigurationManager.AppSettings["PostThumbPath"] = value;
                else ConfigurationManager.AppSettings["PostThumbPath"] = DefaultConfig.DefaultThumbPath;
            }
        }

        #endregion

        #region Featured
        public int PostFeaturedImageHeight
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostFeaturedImageHeight"]); }
            set
            {
                if (value != default(int))
                    ConfigurationManager.AppSettings["PostFeaturedImageHeight"] =
                        value.ToString(CultureInfo.InvariantCulture);
                else ConfigurationManager.AppSettings["PostFeaturedImageHeight"] = DefaultConfig.DefaultFeaturedHeight.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int PostFeaturedImageWidth
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostFeaturedImageWidth"]); }
            set
            {
                if (value != default(int))
                    ConfigurationManager.AppSettings["PostFeaturedImageWidth"] =
                        value.ToString(CultureInfo.InvariantCulture);
                else ConfigurationManager.AppSettings["PostFeaturedImageWidth"] = DefaultConfig.DefaultFeaturedWidth.ToString(CultureInfo.InvariantCulture);
            }
        }

        public string PostFeaturedPath
        {
            get { return ConfigurationManager.AppSettings["PostFeaturedPath"]; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    ConfigurationManager.AppSettings["PostFeaturedPath"] = value;
                else ConfigurationManager.AppSettings["PostFeaturedPath"] = DefaultConfig.DefaultFeaturedPath;
            }
        }

        #endregion

        #region Avatar

        public int AvatarImageHeight
        {
            get { return int.Parse(ConfigurationManager.AppSettings["AvatarImageHeight"]); }
            set
            {
                if (value != default(int))
                    ConfigurationManager.AppSettings["AvatarImageHeight"] = value.ToString(CultureInfo.InvariantCulture);
                else ConfigurationManager.AppSettings["AvatarImageHeight"] = DefaultConfig.DefaultAvatarHeight.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int AvatarImageWidth
        {
            get { return int.Parse(ConfigurationManager.AppSettings["AvatarImageWidth"]); }
            set
            {
                if (value != default(int))
                    ConfigurationManager.AppSettings["AvatarImageWidth"] = value.ToString(CultureInfo.InvariantCulture);
                else ConfigurationManager.AppSettings["AvatarImageWidth"] = DefaultConfig.DefaultAvatarWidth.ToString(CultureInfo.InvariantCulture);
            }
        }

        public string AvatarImagePath
        {
            get { return ConfigurationManager.AppSettings["AvatarImagePath"]; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    ConfigurationManager.AppSettings["AvatarImagePath"] = value;
                else ConfigurationManager.AppSettings["AvatarImagePath"] = DefaultConfig.DefaultAvatarPath;
            }
        }

        #endregion
    }
}