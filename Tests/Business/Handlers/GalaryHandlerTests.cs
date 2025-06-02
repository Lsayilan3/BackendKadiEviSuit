
using Business.Handlers.Galaries.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Galaries.Queries.GetGalaryQuery;
using Entities.Concrete;
using static Business.Handlers.Galaries.Queries.GetGalariesQuery;
using static Business.Handlers.Galaries.Commands.CreateGalaryCommand;
using Business.Handlers.Galaries.Commands;
using Business.Constants;
using static Business.Handlers.Galaries.Commands.UpdateGalaryCommand;
using static Business.Handlers.Galaries.Commands.DeleteGalaryCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class GalaryHandlerTests
    {
        Mock<IGalaryRepository> _galaryRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _galaryRepository = new Mock<IGalaryRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Galary_GetQuery_Success()
        {
            //Arrange
            var query = new GetGalaryQuery();

            _galaryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Galary, bool>>>())).ReturnsAsync(new Galary()
//propertyler buraya yazılacak
//{																		
//GalaryId = 1,
//GalaryName = "Test"
//}
);

            var handler = new GetGalaryQueryHandler(_galaryRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.GalaryId.Should().Be(1);

        }

        [Test]
        public async Task Galary_GetQueries_Success()
        {
            //Arrange
            var query = new GetGalariesQuery();

            _galaryRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Galary, bool>>>()))
                        .ReturnsAsync(new List<Galary> { new Galary() { /*TODO:propertyler buraya yazılacak GalaryId = 1, GalaryName = "test"*/ } });

            var handler = new GetGalariesQueryHandler(_galaryRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Galary>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Galary_CreateCommand_Success()
        {
            Galary rt = null;
            //Arrange
            var command = new CreateGalaryCommand();
            //propertyler buraya yazılacak
            //command.GalaryName = "deneme";

            _galaryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Galary, bool>>>()))
                        .ReturnsAsync(rt);

            _galaryRepository.Setup(x => x.Add(It.IsAny<Galary>())).Returns(new Galary());

            var handler = new CreateGalaryCommandHandler(_galaryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _galaryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Galary_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateGalaryCommand();
            //propertyler buraya yazılacak 
            //command.GalaryName = "test";

            _galaryRepository.Setup(x => x.Query())
                                           .Returns(new List<Galary> { new Galary() { /*TODO:propertyler buraya yazılacak GalaryId = 1, GalaryName = "test"*/ } }.AsQueryable());

            _galaryRepository.Setup(x => x.Add(It.IsAny<Galary>())).Returns(new Galary());

            var handler = new CreateGalaryCommandHandler(_galaryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Galary_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateGalaryCommand();
            //command.GalaryName = "test";

            _galaryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Galary, bool>>>()))
                        .ReturnsAsync(new Galary() { /*TODO:propertyler buraya yazılacak GalaryId = 1, GalaryName = "deneme"*/ });

            _galaryRepository.Setup(x => x.Update(It.IsAny<Galary>())).Returns(new Galary());

            var handler = new UpdateGalaryCommandHandler(_galaryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _galaryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Galary_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteGalaryCommand();

            _galaryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Galary, bool>>>()))
                        .ReturnsAsync(new Galary() { /*TODO:propertyler buraya yazılacak GalaryId = 1, GalaryName = "deneme"*/});

            _galaryRepository.Setup(x => x.Delete(It.IsAny<Galary>()));

            var handler = new DeleteGalaryCommandHandler(_galaryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _galaryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

