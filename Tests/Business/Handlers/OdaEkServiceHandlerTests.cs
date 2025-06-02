
using Business.Handlers.OdaEkServices.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OdaEkServices.Queries.GetOdaEkServiceQuery;
using Entities.Concrete;
using static Business.Handlers.OdaEkServices.Queries.GetOdaEkServicesQuery;
using static Business.Handlers.OdaEkServices.Commands.CreateOdaEkServiceCommand;
using Business.Handlers.OdaEkServices.Commands;
using Business.Constants;
using static Business.Handlers.OdaEkServices.Commands.UpdateOdaEkServiceCommand;
using static Business.Handlers.OdaEkServices.Commands.DeleteOdaEkServiceCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OdaEkServiceHandlerTests
    {
        Mock<IOdaEkServiceRepository> _odaEkServiceRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _odaEkServiceRepository = new Mock<IOdaEkServiceRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OdaEkService_GetQuery_Success()
        {
            //Arrange
            var query = new GetOdaEkServiceQuery();

            _odaEkServiceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OdaEkService, bool>>>())).ReturnsAsync(new OdaEkService()
//propertyler buraya yazılacak
//{																		
//OdaEkServiceId = 1,
//OdaEkServiceName = "Test"
//}
);

            var handler = new GetOdaEkServiceQueryHandler(_odaEkServiceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OdaEkServiceId.Should().Be(1);

        }

        [Test]
        public async Task OdaEkService_GetQueries_Success()
        {
            //Arrange
            var query = new GetOdaEkServicesQuery();

            _odaEkServiceRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OdaEkService, bool>>>()))
                        .ReturnsAsync(new List<OdaEkService> { new OdaEkService() { /*TODO:propertyler buraya yazılacak OdaEkServiceId = 1, OdaEkServiceName = "test"*/ } });

            var handler = new GetOdaEkServicesQueryHandler(_odaEkServiceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OdaEkService>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OdaEkService_CreateCommand_Success()
        {
            OdaEkService rt = null;
            //Arrange
            var command = new CreateOdaEkServiceCommand();
            //propertyler buraya yazılacak
            //command.OdaEkServiceName = "deneme";

            _odaEkServiceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OdaEkService, bool>>>()))
                        .ReturnsAsync(rt);

            _odaEkServiceRepository.Setup(x => x.Add(It.IsAny<OdaEkService>())).Returns(new OdaEkService());

            var handler = new CreateOdaEkServiceCommandHandler(_odaEkServiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _odaEkServiceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OdaEkService_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOdaEkServiceCommand();
            //propertyler buraya yazılacak 
            //command.OdaEkServiceName = "test";

            _odaEkServiceRepository.Setup(x => x.Query())
                                           .Returns(new List<OdaEkService> { new OdaEkService() { /*TODO:propertyler buraya yazılacak OdaEkServiceId = 1, OdaEkServiceName = "test"*/ } }.AsQueryable());

            _odaEkServiceRepository.Setup(x => x.Add(It.IsAny<OdaEkService>())).Returns(new OdaEkService());

            var handler = new CreateOdaEkServiceCommandHandler(_odaEkServiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OdaEkService_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOdaEkServiceCommand();
            //command.OdaEkServiceName = "test";

            _odaEkServiceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OdaEkService, bool>>>()))
                        .ReturnsAsync(new OdaEkService() { /*TODO:propertyler buraya yazılacak OdaEkServiceId = 1, OdaEkServiceName = "deneme"*/ });

            _odaEkServiceRepository.Setup(x => x.Update(It.IsAny<OdaEkService>())).Returns(new OdaEkService());

            var handler = new UpdateOdaEkServiceCommandHandler(_odaEkServiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _odaEkServiceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OdaEkService_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOdaEkServiceCommand();

            _odaEkServiceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OdaEkService, bool>>>()))
                        .ReturnsAsync(new OdaEkService() { /*TODO:propertyler buraya yazılacak OdaEkServiceId = 1, OdaEkServiceName = "deneme"*/});

            _odaEkServiceRepository.Setup(x => x.Delete(It.IsAny<OdaEkService>()));

            var handler = new DeleteOdaEkServiceCommandHandler(_odaEkServiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _odaEkServiceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

