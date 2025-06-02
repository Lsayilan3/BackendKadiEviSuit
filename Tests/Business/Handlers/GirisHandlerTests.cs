
using Business.Handlers.Girises.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Girises.Queries.GetGirisQuery;
using Entities.Concrete;
using static Business.Handlers.Girises.Queries.GetGirisesQuery;
using static Business.Handlers.Girises.Commands.CreateGirisCommand;
using Business.Handlers.Girises.Commands;
using Business.Constants;
using static Business.Handlers.Girises.Commands.UpdateGirisCommand;
using static Business.Handlers.Girises.Commands.DeleteGirisCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class GirisHandlerTests
    {
        Mock<IGirisRepository> _girisRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _girisRepository = new Mock<IGirisRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Giris_GetQuery_Success()
        {
            //Arrange
            var query = new GetGirisQuery();

            _girisRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Giris, bool>>>())).ReturnsAsync(new Giris()
//propertyler buraya yazılacak
//{																		
//GirisId = 1,
//GirisName = "Test"
//}
);

            var handler = new GetGirisQueryHandler(_girisRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.GirisId.Should().Be(1);

        }

        [Test]
        public async Task Giris_GetQueries_Success()
        {
            //Arrange
            var query = new GetGirisesQuery();

            _girisRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Giris, bool>>>()))
                        .ReturnsAsync(new List<Giris> { new Giris() { /*TODO:propertyler buraya yazılacak GirisId = 1, GirisName = "test"*/ } });

            var handler = new GetGirisesQueryHandler(_girisRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Giris>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Giris_CreateCommand_Success()
        {
            Giris rt = null;
            //Arrange
            var command = new CreateGirisCommand();
            //propertyler buraya yazılacak
            //command.GirisName = "deneme";

            _girisRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Giris, bool>>>()))
                        .ReturnsAsync(rt);

            _girisRepository.Setup(x => x.Add(It.IsAny<Giris>())).Returns(new Giris());

            var handler = new CreateGirisCommandHandler(_girisRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _girisRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Giris_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateGirisCommand();
            //propertyler buraya yazılacak 
            //command.GirisName = "test";

            _girisRepository.Setup(x => x.Query())
                                           .Returns(new List<Giris> { new Giris() { /*TODO:propertyler buraya yazılacak GirisId = 1, GirisName = "test"*/ } }.AsQueryable());

            _girisRepository.Setup(x => x.Add(It.IsAny<Giris>())).Returns(new Giris());

            var handler = new CreateGirisCommandHandler(_girisRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Giris_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateGirisCommand();
            //command.GirisName = "test";

            _girisRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Giris, bool>>>()))
                        .ReturnsAsync(new Giris() { /*TODO:propertyler buraya yazılacak GirisId = 1, GirisName = "deneme"*/ });

            _girisRepository.Setup(x => x.Update(It.IsAny<Giris>())).Returns(new Giris());

            var handler = new UpdateGirisCommandHandler(_girisRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _girisRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Giris_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteGirisCommand();

            _girisRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Giris, bool>>>()))
                        .ReturnsAsync(new Giris() { /*TODO:propertyler buraya yazılacak GirisId = 1, GirisName = "deneme"*/});

            _girisRepository.Setup(x => x.Delete(It.IsAny<Giris>()));

            var handler = new DeleteGirisCommandHandler(_girisRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _girisRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

