using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Domain;
using MvcBlog.WebUI.Abstract;
using MvcBlog.WebUI.Mappers;
using Ninject;

namespace MvcBlog.WebUI.Controllers
{
    public class BaseController : Controller
    {
        [Inject]
        public IRepository Repository { get; set; }

        [Inject]
        public IConfigService ConfigService { get; set; }

        [Inject]
        public IMapper ModelMapper { get; set; }
    }
}
