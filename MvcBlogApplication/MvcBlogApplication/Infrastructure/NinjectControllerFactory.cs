using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.Mvc;
using Ninject;
using Moq;
using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Concrete;
using MvcBlog.Domain.Entities;

namespace MvcBlog.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _ninjectKernel;

        // constructor
        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
        }


        private void AddBindings()
        {
            /* 
            Mock<IPostsRepository> mock = new Mock<IPostsRepository>();
            mock.Setup(p => p.Posts).Returns(new List<Post> {
                new Post { PostID = 1, PostContent = "Post content 1"},
                new Post { PostID = 2, PostContent = "Post content 2"},
                new Post { PostID = 3, PostContent = "Post content 3"},
                new Post { PostID = 4, PostContent = "Post content 4"},
                new Post { PostID = 5, PostContent = "Post content 5"},
            }.AsQueryable());

            _ninjectKernel.Bind<IPostsRepository>().ToConstant(mock.Object);
            */
            _ninjectKernel.Bind<IPostsRepository>().To<EFPostsRepository>();
            _ninjectKernel.Bind<ICommentsRepository>().To<EFCommentsRepository>();
        }
    }
}