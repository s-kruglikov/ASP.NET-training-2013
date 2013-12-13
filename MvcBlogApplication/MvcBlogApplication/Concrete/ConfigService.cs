using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBlog.WebUI.Abstract;
using System.Configuration;

namespace MvcBlog.WebUI.Concrete
{
    public class ConfigService : IConfigService
    {
        public int PostsPerPage
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostsPerPage"]); }
            set { ConfigurationManager.AppSettings["PostsPerPage"] = value.ToString(); }
        }

        public int PostThumbImageHeight
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostThumbImageHeight"]); }
            set { ConfigurationManager.AppSettings["PostThumbImageHeight"] = value.ToString(); }
        }

        public int PostThumbImageWidth
        {
            get { return int.Parse(ConfigurationManager.AppSettings["PostThumbImageWidth"]); }
            set { ConfigurationManager.AppSettings["PostThumbImageWidth"] = value.ToString(); }
        }

        public string PostThumbPath
        {
            get { return ConfigurationManager.AppSettings["PostThumbPath"]; }
            set { ConfigurationManager.AppSettings["PostThumbPath"] = value; }
        }

        public string PostFeaturedPath
        {
            get { return ConfigurationManager.AppSettings["PostFeaturedPath"]; }
            set { ConfigurationManager.AppSettings["PostFeaturedPath"] = value; }
        }
    }
}