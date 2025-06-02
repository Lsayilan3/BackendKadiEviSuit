
using Business.Handlers.Services.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Services.Queries.GetServiceQuery;
using Entities.Concrete;
using static Business.Handlers.Services.Queries.GetServicesQuery;
using static Business.Handlers.Services.Commands.CreateServiceCommand;
using Business.Handlers.Services.Commands;
using Business.Constants;
using static Business.Handlers.Services.Commands.UpdateServiceCommand;
using static Business.Handlers.Services.Commands.DeleteServiceCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ServiceHandlerTests
    {
        Mock<IServiceRepository> _serviceRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _serviceRepository = new Mock<IServiceRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Service_GetQuery_Success()
        {
            //Arrange
            var query = new GetServiceQuery();

            _serviceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Service, bool>>>())).ReturnsAsync(new Service()
//propertyler buraya yazılacak
//{																		
//ServiceId = 1,
//ServiceName = "Test"
//}
);

            var handler = new GetServiceQueryHandler(_serviceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ServiceId.Should().Be(1);

        }

        [Test]
        public async Task Service_GetQueries_Success()
        {
            //Arrange
            var query = new GetServicesQuery();

            _serviceRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Service, bool>>>()))
                        .ReturnsAsync(new List<Service> { new Service() { /*TODO:propertyler buraya yazılacak ServiceId = 1, ServiceName = "test"*/ } });

            var handler = new GetServicesQueryHandler(_serviceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Service>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Service_CreateCommand_Success()
        {
            Service rt = null;
            //Arrange
            var command = new CreateServiceCommand();
            //propertyler buraya yazılacak
            //command.ServiceName = "deneme";

            _serviceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Service, bool>>>()))
                        .ReturnsAsync(rt);

            _serviceRepository.Setup(x => x.Add(It.IsAny<Service>())).Returns(new Service());

            var handler = new CreateServiceCommandHandler(_serviceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _serviceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Service_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateServiceCommand();
            //propertyler buraya yazılacak 
            //command.ServiceName = "test";

            _serviceRepository.Setup(x => x.Query())
                                           .Returns(new List<Service> { new Service() { /*TODO:propertyler buraya yazılacak ServiceId = 1, ServiceName = "test"*/ } }.AsQueryable());

            _serviceRepository.Setup(x => x.Add(It.IsAny<Service>())).Returns(new Service());

            var handler = new CreateServiceCommandHandler(_serviceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Service_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateServiceCommand();
            //command.ServiceName = "test";

            _serviceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Service, bool>>>()))
                        .ReturnsAsync(new Service() { /*TODO:propertyler buraya yazılacak ServiceId = 1, ServiceName = "deneme"*/ });

            _serviceRepository.Setup(x => x.Update(It.IsAny<Service>())).Returns(new Service());

            var handler = new UpdateServiceCommandHandler(_serviceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _serviceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Service_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteServiceCommand();

            _serviceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Service, bool>>>()))
                        .ReturnsAsync(new Service() { /*TODO:propertyler buraya yazılacak ServiceId = 1, ServiceName = "deneme"*/});

            _serviceRepository.Setup(x => x.Delete(It.IsAny<Service>()));

            var handler = new DeleteServiceCommandHandler(_serviceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _serviceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

