
using Business.Handlers.GalaryBlogs.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.GalaryBlogs.Queries.GetGalaryBlogQuery;
using Entities.Concrete;
using static Business.Handlers.GalaryBlogs.Queries.GetGalaryBlogsQuery;
using static Business.Handlers.GalaryBlogs.Commands.CreateGalaryBlogCommand;
using Business.Handlers.GalaryBlogs.Commands;
using Business.Constants;
using static Business.Handlers.GalaryBlogs.Commands.UpdateGalaryBlogCommand;
using static Business.Handlers.GalaryBlogs.Commands.DeleteGalaryBlogCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class GalaryBlogHandlerTests
    {
        Mock<IGalaryBlogRepository> _galaryBlogRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _galaryBlogRepository = new Mock<IGalaryBlogRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task GalaryBlog_GetQuery_Success()
        {
            //Arrange
            var query = new GetGalaryBlogQuery();

            _galaryBlogRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GalaryBlog, bool>>>())).ReturnsAsync(new GalaryBlog()
//propertyler buraya yazılacak
//{																		
//GalaryBlogId = 1,
//GalaryBlogName = "Test"
//}
);

            var handler = new GetGalaryBlogQueryHandler(_galaryBlogRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.GalaryBlogId.Should().Be(1);

        }

        [Test]
        public async Task GalaryBlog_GetQueries_Success()
        {
            //Arrange
            var query = new GetGalaryBlogsQuery();

            _galaryBlogRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<GalaryBlog, bool>>>()))
                        .ReturnsAsync(new List<GalaryBlog> { new GalaryBlog() { /*TODO:propertyler buraya yazılacak GalaryBlogId = 1, GalaryBlogName = "test"*/ } });

            var handler = new GetGalaryBlogsQueryHandler(_galaryBlogRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<GalaryBlog>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task GalaryBlog_CreateCommand_Success()
        {
            GalaryBlog rt = null;
            //Arrange
            var command = new CreateGalaryBlogCommand();
            //propertyler buraya yazılacak
            //command.GalaryBlogName = "deneme";

            _galaryBlogRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GalaryBlog, bool>>>()))
                        .ReturnsAsync(rt);

            _galaryBlogRepository.Setup(x => x.Add(It.IsAny<GalaryBlog>())).Returns(new GalaryBlog());

            var handler = new CreateGalaryBlogCommandHandler(_galaryBlogRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _galaryBlogRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task GalaryBlog_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateGalaryBlogCommand();
            //propertyler buraya yazılacak 
            //command.GalaryBlogName = "test";

            _galaryBlogRepository.Setup(x => x.Query())
                                           .Returns(new List<GalaryBlog> { new GalaryBlog() { /*TODO:propertyler buraya yazılacak GalaryBlogId = 1, GalaryBlogName = "test"*/ } }.AsQueryable());

            _galaryBlogRepository.Setup(x => x.Add(It.IsAny<GalaryBlog>())).Returns(new GalaryBlog());

            var handler = new CreateGalaryBlogCommandHandler(_galaryBlogRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task GalaryBlog_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateGalaryBlogCommand();
            //command.GalaryBlogName = "test";

            _galaryBlogRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GalaryBlog, bool>>>()))
                        .ReturnsAsync(new GalaryBlog() { /*TODO:propertyler buraya yazılacak GalaryBlogId = 1, GalaryBlogName = "deneme"*/ });

            _galaryBlogRepository.Setup(x => x.Update(It.IsAny<GalaryBlog>())).Returns(new GalaryBlog());

            var handler = new UpdateGalaryBlogCommandHandler(_galaryBlogRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _galaryBlogRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task GalaryBlog_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteGalaryBlogCommand();

            _galaryBlogRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GalaryBlog, bool>>>()))
                        .ReturnsAsync(new GalaryBlog() { /*TODO:propertyler buraya yazılacak GalaryBlogId = 1, GalaryBlogName = "deneme"*/});

            _galaryBlogRepository.Setup(x => x.Delete(It.IsAny<GalaryBlog>()));

            var handler = new DeleteGalaryBlogCommandHandler(_galaryBlogRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _galaryBlogRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

