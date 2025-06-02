
using Business.Handlers.Iletisims.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Iletisims.Queries.GetIletisimQuery;
using Entities.Concrete;
using static Business.Handlers.Iletisims.Queries.GetIletisimsQuery;
using static Business.Handlers.Iletisims.Commands.CreateIletisimCommand;
using Business.Handlers.Iletisims.Commands;
using Business.Constants;
using static Business.Handlers.Iletisims.Commands.UpdateIletisimCommand;
using static Business.Handlers.Iletisims.Commands.DeleteIletisimCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class IletisimHandlerTests
    {
        Mock<IIletisimRepository> _iletisimRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _iletisimRepository = new Mock<IIletisimRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Iletisim_GetQuery_Success()
        {
            //Arrange
            var query = new GetIletisimQuery();

            _iletisimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Iletisim, bool>>>())).ReturnsAsync(new Iletisim()
//propertyler buraya yazılacak
//{																		
//IletisimId = 1,
//IletisimName = "Test"
//}
);

            var handler = new GetIletisimQueryHandler(_iletisimRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.IletisimId.Should().Be(1);

        }

        [Test]
        public async Task Iletisim_GetQueries_Success()
        {
            //Arrange
            var query = new GetIletisimsQuery();

            _iletisimRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Iletisim, bool>>>()))
                        .ReturnsAsync(new List<Iletisim> { new Iletisim() { /*TODO:propertyler buraya yazılacak IletisimId = 1, IletisimName = "test"*/ } });

            var handler = new GetIletisimsQueryHandler(_iletisimRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Iletisim>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Iletisim_CreateCommand_Success()
        {
            Iletisim rt = null;
            //Arrange
            var command = new CreateIletisimCommand();
            //propertyler buraya yazılacak
            //command.IletisimName = "deneme";

            _iletisimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Iletisim, bool>>>()))
                        .ReturnsAsync(rt);

            _iletisimRepository.Setup(x => x.Add(It.IsAny<Iletisim>())).Returns(new Iletisim());

            var handler = new CreateIletisimCommandHandler(_iletisimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _iletisimRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Iletisim_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateIletisimCommand();
            //propertyler buraya yazılacak 
            //command.IletisimName = "test";

            _iletisimRepository.Setup(x => x.Query())
                                           .Returns(new List<Iletisim> { new Iletisim() { /*TODO:propertyler buraya yazılacak IletisimId = 1, IletisimName = "test"*/ } }.AsQueryable());

            _iletisimRepository.Setup(x => x.Add(It.IsAny<Iletisim>())).Returns(new Iletisim());

            var handler = new CreateIletisimCommandHandler(_iletisimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Iletisim_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateIletisimCommand();
            //command.IletisimName = "test";

            _iletisimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Iletisim, bool>>>()))
                        .ReturnsAsync(new Iletisim() { /*TODO:propertyler buraya yazılacak IletisimId = 1, IletisimName = "deneme"*/ });

            _iletisimRepository.Setup(x => x.Update(It.IsAny<Iletisim>())).Returns(new Iletisim());

            var handler = new UpdateIletisimCommandHandler(_iletisimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _iletisimRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Iletisim_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteIletisimCommand();

            _iletisimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Iletisim, bool>>>()))
                        .ReturnsAsync(new Iletisim() { /*TODO:propertyler buraya yazılacak IletisimId = 1, IletisimName = "deneme"*/});

            _iletisimRepository.Setup(x => x.Delete(It.IsAny<Iletisim>()));

            var handler = new DeleteIletisimCommandHandler(_iletisimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _iletisimRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

