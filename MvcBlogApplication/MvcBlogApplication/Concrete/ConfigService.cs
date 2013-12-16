using System.Globalization;
using MvcBlog.WebUI.Abstract;
using System.Configuration;

namespace MvcBlog.WebUI.Concrete
{
    public class ConfigService : IConfigService
    {
        public int PostsPerPage
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostsPerPage"]); }
            set { ConfigurationManager.AppSettings["PostsPerPage"] = value.ToString(CultureInfo.InvariantCulture); }
        }

        #region Thumbnails
        public int PostThumbImageHeight
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostThumbImageHeight"]); }
            set { ConfigurationManager.AppSettings["PostThumbImageHeight"] = value.ToString(CultureInfo.InvariantCulture); }
        }

        public int PostThumbImageWidth
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostThumbImageWidth"]); }
            set { ConfigurationManager.AppSettings["PostThumbImageWidth"] = value.ToString(CultureInfo.InvariantCulture); }
        }

        public string PostThumbPath
        {
            get { return ConfigurationManager.AppSettings["PostThumbPath"]; }
            set { ConfigurationManager.AppSettings["PostThumbPath"] = value; }
        }

        #endregion

        public int PostFeaturedImageHeight
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostFeaturedImageHeight"]); }
            set
            {
                ConfigurationManager.AppSettings["PostFeaturedImageHeight"] =
                    value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int PostFeaturedImageWidth
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostFeaturedImageWidth"]); }
            set
            {
                ConfigurationManager.AppSettings["PostFeaturedImageWidth"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public string PostFeaturedPath
        {
            get { return ConfigurationManager.AppSettings["PostFeaturedPath"]; }
            set { ConfigurationManager.AppSettings["PostFeaturedPath"] = value; }
        }

        #region Avatar

        public int AvatarImageHeight
        {
            get { return int.Parse(ConfigurationManager.AppSettings["AvatarImageHeight"]); }
            set
            {
                ConfigurationManager.AppSettings["AvatarImageHeight"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int AvatarImageWidth
        {
            get { return int.Parse(ConfigurationManager.AppSettings["AvatarImageWidth"]); }
            set { ConfigurationManager.AppSettings["AvatarImageWidth"] = value.ToString(CultureInfo.InvariantCulture); }
        }

        public string AvatarImagePath
        {
            get { return ConfigurationManager.AppSettings["AvatarImagePath"]; }
            set { ConfigurationManager.AppSettings["AvatarImagePath"] = value; }
        }

        #endregion
    }
}