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
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PostsPerPage"]);
            }
            set
            {
                ConfigurationManager.AppSettings["PostsPerPage"] = value.ToString();
            }
        }
    }
}