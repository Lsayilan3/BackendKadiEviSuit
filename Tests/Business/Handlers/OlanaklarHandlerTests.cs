
using Business.Handlers.Olanaklars.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Olanaklars.Queries.GetOlanaklarQuery;
using Entities.Concrete;
using static Business.Handlers.Olanaklars.Queries.GetOlanaklarsQuery;
using static Business.Handlers.Olanaklars.Commands.CreateOlanaklarCommand;
using Business.Handlers.Olanaklars.Commands;
using Business.Constants;
using static Business.Handlers.Olanaklars.Commands.UpdateOlanaklarCommand;
using static Business.Handlers.Olanaklars.Commands.DeleteOlanaklarCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OlanaklarHandlerTests
    {
        Mock<IOlanaklarRepository> _olanaklarRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _olanaklarRepository = new Mock<IOlanaklarRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Olanaklar_GetQuery_Success()
        {
            //Arrange
            var query = new GetOlanaklarQuery();

            _olanaklarRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Olanaklar, bool>>>())).ReturnsAsync(new Olanaklar()
//propertyler buraya yazılacak
//{																		
//OlanaklarId = 1,
//OlanaklarName = "Test"
//}
);

            var handler = new GetOlanaklarQueryHandler(_olanaklarRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OlanaklarId.Should().Be(1);

        }

        [Test]
        public async Task Olanaklar_GetQueries_Success()
        {
            //Arrange
            var query = new GetOlanaklarsQuery();

            _olanaklarRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Olanaklar, bool>>>()))
                        .ReturnsAsync(new List<Olanaklar> { new Olanaklar() { /*TODO:propertyler buraya yazılacak OlanaklarId = 1, OlanaklarName = "test"*/ } });

            var handler = new GetOlanaklarsQueryHandler(_olanaklarRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Olanaklar>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Olanaklar_CreateCommand_Success()
        {
            Olanaklar rt = null;
            //Arrange
            var command = new CreateOlanaklarCommand();
            //propertyler buraya yazılacak
            //command.OlanaklarName = "deneme";

            _olanaklarRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Olanaklar, bool>>>()))
                        .ReturnsAsync(rt);

            _olanaklarRepository.Setup(x => x.Add(It.IsAny<Olanaklar>())).Returns(new Olanaklar());

            var handler = new CreateOlanaklarCommandHandler(_olanaklarRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _olanaklarRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Olanaklar_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOlanaklarCommand();
            //propertyler buraya yazılacak 
            //command.OlanaklarName = "test";

            _olanaklarRepository.Setup(x => x.Query())
                                           .Returns(new List<Olanaklar> { new Olanaklar() { /*TODO:propertyler buraya yazılacak OlanaklarId = 1, OlanaklarName = "test"*/ } }.AsQueryable());

            _olanaklarRepository.Setup(x => x.Add(It.IsAny<Olanaklar>())).Returns(new Olanaklar());

            var handler = new CreateOlanaklarCommandHandler(_olanaklarRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Olanaklar_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOlanaklarCommand();
            //command.OlanaklarName = "test";

            _olanaklarRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Olanaklar, bool>>>()))
                        .ReturnsAsync(new Olanaklar() { /*TODO:propertyler buraya yazılacak OlanaklarId = 1, OlanaklarName = "deneme"*/ });

            _olanaklarRepository.Setup(x => x.Update(It.IsAny<Olanaklar>())).Returns(new Olanaklar());

            var handler = new UpdateOlanaklarCommandHandler(_olanaklarRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _olanaklarRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Olanaklar_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOlanaklarCommand();

            _olanaklarRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Olanaklar, bool>>>()))
                        .ReturnsAsync(new Olanaklar() { /*TODO:propertyler buraya yazılacak OlanaklarId = 1, OlanaklarName = "deneme"*/});

            _olanaklarRepository.Setup(x => x.Delete(It.IsAny<Olanaklar>()));

            var handler = new DeleteOlanaklarCommandHandler(_olanaklarRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _olanaklarRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

