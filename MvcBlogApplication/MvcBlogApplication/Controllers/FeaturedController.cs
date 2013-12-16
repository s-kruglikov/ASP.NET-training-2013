using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Domain;

namespace MvcBlog.WebUI.Controllers
{
    public class FeaturedController : Controller
    {
        private readonly IRepository _repository;

        public FeaturedController(IRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: 

        public PartialViewResult Slides()
        {
            var featuredPosts = _repository.Posts
                .Where(p => p.PostFeatured == true && p.PostIsVisible == true)
                .OrderByDescending(p => p.PostID);

            return PartialView(featuredPosts);
        }
    }
}
