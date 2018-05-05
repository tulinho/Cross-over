using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crossblog.Controllers;
using crossblog.Domain;
using crossblog.Model;
using crossblog.Repositories;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace crossblog.tests.Controllers
{
    public class CommentsControllerTests
    {
        private CommentsController _commentController;
        private ArticlesController _articlesController;

        private Mock<IArticleRepository> _articleRepositoryMock = new Mock<IArticleRepository>();
        private Mock<ICommentRepository> _commentRepositoryMock = new Mock<ICommentRepository>();

        public CommentsControllerTests()
        {
            _commentController = new CommentsController(_articleRepositoryMock.Object, _commentRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            // Arrange
            _commentRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Comment>(null));

            // Act
            var result = await _commentController.Get(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
        }

        [Fact]
        public async Task Get_ReturnsItem()
        {
            // Arrange
            _commentRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Comment>(Builder<Comment>.CreateNew().Build()));
            var commentDbSetMock = Builder<Comment>.CreateListOfSize(3).Build().ToAsyncDbSetMock();
            _commentRepositoryMock.Setup(m => m.Query()).Returns(commentDbSetMock.Object);
            _articleRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Article>(Builder<Article>.CreateNew().Build()));
            // Act
            var result = await _commentController.Get(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);
        }

        [Fact]
        public async Task Post()
        {
            // Arrange
            _articleRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Article>(Builder<Article>.CreateNew().Build()));
            _commentRepositoryMock.Setup(m => m.InsertAsync(new Comment())).Returns(Task.FromResult<Comment>(Builder<Comment>.CreateNew().Build()));

            // Act
            var result = await _commentController.Post(1, new CommentModel());

            // Assert
            Assert.NotNull(result);
        }
    }
}