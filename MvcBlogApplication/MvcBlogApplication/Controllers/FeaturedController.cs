using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Domain.Abstract;

namespace MvcBlog.WebUI.Controllers
{
    public class FeaturedController : Controller
    {
        private readonly IPostsRepository _postsRepository;

        public FeaturedController(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        //
        // GET: /Featured/

        public PartialViewResult Slides()
        {
            var featuredPosts = _postsRepository.Posts
                .Where(p => p.PostFeatured == true)
                .OrderByDescending(p => p.PostID);

            return PartialView(featuredPosts);
        }


    }
}
