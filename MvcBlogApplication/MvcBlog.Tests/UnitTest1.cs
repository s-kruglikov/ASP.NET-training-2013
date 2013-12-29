using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcBlog.Domain;
using MvcBlog.Domain.Entities;
using MvcBlog.WebUI.Abstract;
using MvcBlog.WebUI.Controllers;
using MvcBlog.WebUI.Models;

namespace MvcBlog.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PaginationShouldDisplayConcreteNumberOfPosts()
        {
            //Arrange
            var mockRepository = new Mock<IRepository>();
            var mockConfig = new Mock<IConfigService>();
            
            mockRepository.Setup(m => m.Posts).Returns(new[]
            {
                new Post {PostID = 1, PostTitle = "Post 1", PostIsVisible = true},
                new Post {PostID = 2, PostTitle = "Post 2", PostIsVisible = true},
                new Post {PostID = 3, PostTitle = "Post 3", PostIsVisible = true},
                new Post {PostID = 4, PostTitle = "Post 4", PostIsVisible = true},
                new Post {PostID = 5, PostTitle = "Post 5", PostIsVisible = true}
            }.AsQueryable());

            mockConfig.Setup(c => c.PostsPerPage).Returns(2);

            var controller = new PostsController
            {
                Repository = mockRepository.Object,
                ConfigService = mockConfig.Object
            };

            //Act
            PostsListViewModel model = (PostsListViewModel)controller.List(null, 2).Model;
            IEnumerable<Post> result = model.Posts;

            //Assert
            Post[] posts = result.ToArray();
            Assert.IsTrue(posts.Length == 2);
            Assert.AreEqual(posts[0].PostTitle, "Post 3");
            Assert.AreEqual(posts[1].PostTitle, "Post 2"); //posts retriewed desc
        }
    }
}
