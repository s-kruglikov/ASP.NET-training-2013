using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;
using MvcBlog.WebUI.Models;
using System.Configuration;

namespace MvcBlog.WebUI.Controllers
{
    public class PostsController : Controller
    {
        private IPostsRepository _postsRepository;

        public int PageSize { get; private set; } 

        public PostsController(IPostsRepository postsRepository)
        {
            this._postsRepository = postsRepository;
            PageSize = int.Parse(ConfigurationManager.AppSettings["PostsPerPage"]);
        }

        //
        // GET: /Posts/

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List(int page = 1)
        {
            PostsListViewModel model = new PostsListViewModel
            {
                Posts = _postsRepository.Posts
                .OrderByDescending(p => p.PostID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                  
                PagingModel = new PagingModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _postsRepository.Posts.Count()
                }
            };

            return View(model);
        }

    }
}
