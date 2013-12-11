using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBlog.Domain.Abstract;

namespace MvcBlog.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private readonly IPostsRepository _repository;

        public NavigationController(IPostsRepository repository)
        {
            _repository = repository;
        }


        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _repository.Posts
                .Where(p => p.PostIsVisible == true)
                .Select(p => p.PostCategory)
                .Distinct()
                .OrderBy(x => x).ToList();

            return PartialView(categories);
        }

    }
}
