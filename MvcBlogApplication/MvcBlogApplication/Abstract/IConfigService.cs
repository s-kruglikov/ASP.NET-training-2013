using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcBlog.WebUI.Abstract
{
    public interface IConfigService
    {
        int PostsPerPage { get; set; }

        int PostThumbImageHeight { get; set; }

        int PostThumbImageWidth { get; set; }

        string PostThumbPath { get; set; }

        string PostFeaturedPath { get; set; }
    }
}
