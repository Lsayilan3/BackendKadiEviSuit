
using Business.Handlers.OdaOlanaks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OdaOlanaks.Queries.GetOdaOlanakQuery;
using Entities.Concrete;
using static Business.Handlers.OdaOlanaks.Queries.GetOdaOlanaksQuery;
using static Business.Handlers.OdaOlanaks.Commands.CreateOdaOlanakCommand;
using Business.Handlers.OdaOlanaks.Commands;
using Business.Constants;
using static Business.Handlers.OdaOlanaks.Commands.UpdateOdaOlanakCommand;
using static Business.Handlers.OdaOlanaks.Commands.DeleteOdaOlanakCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OdaOlanakHandlerTests
    {
        Mock<IOdaOlanakRepository> _odaOlanakRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _odaOlanakRepository = new Mock<IOdaOlanakRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OdaOlanak_GetQuery_Success()
        {
            //Arrange
            var query = new GetOdaOlanakQuery();

            _odaOlanakRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OdaOlanak, bool>>>())).ReturnsAsync(new OdaOlanak()
//propertyler buraya yazılacak
//{																		
//OdaOlanakId = 1,
//OdaOlanakName = "Test"
//}
);

            var handler = new GetOdaOlanakQueryHandler(_odaOlanakRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OdaOlanakId.Should().Be(1);

        }

        [Test]
        public async Task OdaOlanak_GetQueries_Success()
        {
            //Arrange
            var query = new GetOdaOlanaksQuery();

            _odaOlanakRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OdaOlanak, bool>>>()))
                        .ReturnsAsync(new List<OdaOlanak> { new OdaOlanak() { /*TODO:propertyler buraya yazılacak OdaOlanakId = 1, OdaOlanakName = "test"*/ } });

            var handler = new GetOdaOlanaksQueryHandler(_odaOlanakRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OdaOlanak>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OdaOlanak_CreateCommand_Success()
        {
            OdaOlanak rt = null;
            //Arrange
            var command = new CreateOdaOlanakCommand();
            //propertyler buraya yazılacak
            //command.OdaOlanakName = "deneme";

            _odaOlanakRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OdaOlanak, bool>>>()))
                        .ReturnsAsync(rt);

            _odaOlanakRepository.Setup(x => x.Add(It.IsAny<OdaOlanak>())).Returns(new OdaOlanak());

            var handler = new CreateOdaOlanakCommandHandler(_odaOlanakRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _odaOlanakRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OdaOlanak_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOdaOlanakCommand();
            //propertyler buraya yazılacak 
            //command.OdaOlanakName = "test";

            _odaOlanakRepository.Setup(x => x.Query())
                                           .Returns(new List<OdaOlanak> { new OdaOlanak() { /*TODO:propertyler buraya yazılacak OdaOlanakId = 1, OdaOlanakName = "test"*/ } }.AsQueryable());

            _odaOlanakRepository.Setup(x => x.Add(It.IsAny<OdaOlanak>())).Returns(new OdaOlanak());

            var handler = new CreateOdaOlanakCommandHandler(_odaOlanakRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OdaOlanak_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOdaOlanakCommand();
            //command.OdaOlanakName = "test";

            _odaOlanakRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OdaOlanak, bool>>>()))
                        .ReturnsAsync(new OdaOlanak() { /*TODO:propertyler buraya yazılacak OdaOlanakId = 1, OdaOlanakName = "deneme"*/ });

            _odaOlanakRepository.Setup(x => x.Update(It.IsAny<OdaOlanak>())).Returns(new OdaOlanak());

            var handler = new UpdateOdaOlanakCommandHandler(_odaOlanakRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _odaOlanakRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OdaOlanak_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOdaOlanakCommand();

            _odaOlanakRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OdaOlanak, bool>>>()))
                        .ReturnsAsync(new OdaOlanak() { /*TODO:propertyler buraya yazılacak OdaOlanakId = 1, OdaOlanakName = "deneme"*/});

            _odaOlanakRepository.Setup(x => x.Delete(It.IsAny<OdaOlanak>()));

            var handler = new DeleteOdaOlanakCommandHandler(_odaOlanakRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _odaOlanakRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

