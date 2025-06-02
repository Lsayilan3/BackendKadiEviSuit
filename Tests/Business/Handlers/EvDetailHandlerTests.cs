
using Business.Handlers.EvDetails.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.EvDetails.Queries.GetEvDetailQuery;
using Entities.Concrete;
using static Business.Handlers.EvDetails.Queries.GetEvDetailsQuery;
using static Business.Handlers.EvDetails.Commands.CreateEvDetailCommand;
using Business.Handlers.EvDetails.Commands;
using Business.Constants;
using static Business.Handlers.EvDetails.Commands.UpdateEvDetailCommand;
using static Business.Handlers.EvDetails.Commands.DeleteEvDetailCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class EvDetailHandlerTests
    {
        Mock<IEvDetailRepository> _evDetailRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _evDetailRepository = new Mock<IEvDetailRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task EvDetail_GetQuery_Success()
        {
            //Arrange
            var query = new GetEvDetailQuery();

            _evDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EvDetail, bool>>>())).ReturnsAsync(new EvDetail()
//propertyler buraya yazılacak
//{																		
//EvDetailId = 1,
//EvDetailName = "Test"
//}
);

            var handler = new GetEvDetailQueryHandler(_evDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.EvDetailId.Should().Be(1);

        }

        [Test]
        public async Task EvDetail_GetQueries_Success()
        {
            //Arrange
            var query = new GetEvDetailsQuery();

            _evDetailRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<EvDetail, bool>>>()))
                        .ReturnsAsync(new List<EvDetail> { new EvDetail() { /*TODO:propertyler buraya yazılacak EvDetailId = 1, EvDetailName = "test"*/ } });

            var handler = new GetEvDetailsQueryHandler(_evDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<EvDetail>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task EvDetail_CreateCommand_Success()
        {
            EvDetail rt = null;
            //Arrange
            var command = new CreateEvDetailCommand();
            //propertyler buraya yazılacak
            //command.EvDetailName = "deneme";

            _evDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EvDetail, bool>>>()))
                        .ReturnsAsync(rt);

            _evDetailRepository.Setup(x => x.Add(It.IsAny<EvDetail>())).Returns(new EvDetail());

            var handler = new CreateEvDetailCommandHandler(_evDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _evDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task EvDetail_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateEvDetailCommand();
            //propertyler buraya yazılacak 
            //command.EvDetailName = "test";

            _evDetailRepository.Setup(x => x.Query())
                                           .Returns(new List<EvDetail> { new EvDetail() { /*TODO:propertyler buraya yazılacak EvDetailId = 1, EvDetailName = "test"*/ } }.AsQueryable());

            _evDetailRepository.Setup(x => x.Add(It.IsAny<EvDetail>())).Returns(new EvDetail());

            var handler = new CreateEvDetailCommandHandler(_evDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task EvDetail_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateEvDetailCommand();
            //command.EvDetailName = "test";

            _evDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EvDetail, bool>>>()))
                        .ReturnsAsync(new EvDetail() { /*TODO:propertyler buraya yazılacak EvDetailId = 1, EvDetailName = "deneme"*/ });

            _evDetailRepository.Setup(x => x.Update(It.IsAny<EvDetail>())).Returns(new EvDetail());

            var handler = new UpdateEvDetailCommandHandler(_evDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _evDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task EvDetail_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteEvDetailCommand();

            _evDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EvDetail, bool>>>()))
                        .ReturnsAsync(new EvDetail() { /*TODO:propertyler buraya yazılacak EvDetailId = 1, EvDetailName = "deneme"*/});

            _evDetailRepository.Setup(x => x.Delete(It.IsAny<EvDetail>()));

            var handler = new DeleteEvDetailCommandHandler(_evDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _evDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

