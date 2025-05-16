using GestionDesLivres.Application.Commands.Livres;
using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;

namespace GestionDesLivres.Tests.Application.Command.Livres
{
    public class AjouterLivreCommandHandlerTests
    {
        private readonly Mock<ILivreRepository> _livreRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEntityValidationService<Livre>> _entityValidationServiceMock;
        private readonly Mock<ICategorieRepository> _mockCategorieRepository;

        private readonly AjouterLivreCommandHandler _handler;

        public AjouterLivreCommandHandlerTests()
        {
            _livreRepositoryMock = new Mock<ILivreRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _entityValidationServiceMock = new Mock<IEntityValidationService<Livre>>();
            _mockCategorieRepository = new Mock<ICategorieRepository>();

            _unitOfWorkMock.Setup(u => u.Categories).Returns(_mockCategorieRepository.Object);

            _handler = new AjouterLivreCommandHandler(
                _livreRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _entityValidationServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_AjouteLivreCorrectement_RetourneGuid()
        {
            // Arrange
            var command = new AjouterLivreCommand("Titre Test", "Auteur Test", Guid.NewGuid(), 10);

            _livreRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Livre>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(uow => uow.CompleteAsync())
                .ReturnsAsync(1);

            _entityValidationServiceMock.Setup(service =>
                service.VerifierExistenceAsync(It.IsAny<Expression<Func<Livre, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _livreRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Livre>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        }
    }

}


