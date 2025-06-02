
using Business.Handlers.Evs.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Evs.Queries.GetEvQuery;
using Entities.Concrete;
using static Business.Handlers.Evs.Queries.GetEvsQuery;
using static Business.Handlers.Evs.Commands.CreateEvCommand;
using Business.Handlers.Evs.Commands;
using Business.Constants;
using static Business.Handlers.Evs.Commands.UpdateEvCommand;
using static Business.Handlers.Evs.Commands.DeleteEvCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class EvHandlerTests
    {
        Mock<IEvRepository> _evRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _evRepository = new Mock<IEvRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Ev_GetQuery_Success()
        {
            //Arrange
            var query = new GetEvQuery();

            _evRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ev, bool>>>())).ReturnsAsync(new Ev()
//propertyler buraya yazılacak
//{																		
//EvId = 1,
//EvName = "Test"
//}
);

            var handler = new GetEvQueryHandler(_evRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.EvId.Should().Be(1);

        }

        [Test]
        public async Task Ev_GetQueries_Success()
        {
            //Arrange
            var query = new GetEvsQuery();

            _evRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Ev, bool>>>()))
                        .ReturnsAsync(new List<Ev> { new Ev() { /*TODO:propertyler buraya yazılacak EvId = 1, EvName = "test"*/ } });

            var handler = new GetEvsQueryHandler(_evRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Ev>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Ev_CreateCommand_Success()
        {
            Ev rt = null;
            //Arrange
            var command = new CreateEvCommand();
            //propertyler buraya yazılacak
            //command.EvName = "deneme";

            _evRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ev, bool>>>()))
                        .ReturnsAsync(rt);

            _evRepository.Setup(x => x.Add(It.IsAny<Ev>())).Returns(new Ev());

            var handler = new CreateEvCommandHandler(_evRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _evRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Ev_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateEvCommand();
            //propertyler buraya yazılacak 
            //command.EvName = "test";

            _evRepository.Setup(x => x.Query())
                                           .Returns(new List<Ev> { new Ev() { /*TODO:propertyler buraya yazılacak EvId = 1, EvName = "test"*/ } }.AsQueryable());

            _evRepository.Setup(x => x.Add(It.IsAny<Ev>())).Returns(new Ev());

            var handler = new CreateEvCommandHandler(_evRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Ev_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateEvCommand();
            //command.EvName = "test";

            _evRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ev, bool>>>()))
                        .ReturnsAsync(new Ev() { /*TODO:propertyler buraya yazılacak EvId = 1, EvName = "deneme"*/ });

            _evRepository.Setup(x => x.Update(It.IsAny<Ev>())).Returns(new Ev());

            var handler = new UpdateEvCommandHandler(_evRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _evRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Ev_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteEvCommand();

            _evRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ev, bool>>>()))
                        .ReturnsAsync(new Ev() { /*TODO:propertyler buraya yazılacak EvId = 1, EvName = "deneme"*/});

            _evRepository.Setup(x => x.Delete(It.IsAny<Ev>()));

            var handler = new DeleteEvCommandHandler(_evRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _evRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

