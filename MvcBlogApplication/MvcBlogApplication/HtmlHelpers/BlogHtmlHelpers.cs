using MvcBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.WebUI.HtmlHelpers
{
    public static class BlogHtmlHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingModel pagingModel, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a"); //<a>
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = string.Format("{0}", i.ToString());

                if (i == pagingModel.CurrentPage)
                {
                    tag.AddCssClass("selected");
                }
                
                result.Append(tag.ToString());
                result.Append(" ");
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString PostDetailsLinks(this HtmlHelper html, int postId, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(postId));
            tag.InnerHtml = "Read more...";

            result.Append(tag.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}