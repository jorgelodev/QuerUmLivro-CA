using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.UseCases.Livros;

namespace QuerUmLivro.Test.Domain.UseCases.Livros
{
    public class AlterarLivroUseCaseTests
    {
        private readonly Mock<ILivroGateway> _livroGatewayMock;
        private readonly Faker<Livro> _livroFaker;

        public AlterarLivroUseCaseTests()
        {
            _livroGatewayMock = new Mock<ILivroGateway>();

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName()))
                .RuleFor(l => l.Id, f => f.Random.Int(1, 100))
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100));
        }

        [Fact]
        public void Alterar_Should_ThrowException_When_LivroIdIsZero()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            livro.Id = 0;

            var useCase = new AlterarLivroUseCase(livro, _livroGatewayMock.Object);

            // Act
            Action action = () => useCase.Alterar();

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Id do Livro não informado");
        }

        [Fact]
        public void Alterar_Should_ThrowException_When_DoadorIdIsZero()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            livro.DoadorId = 0;

            var useCase = new AlterarLivroUseCase(livro, _livroGatewayMock.Object);

            // Act
            Action action = () => useCase.Alterar();

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Doador é obrigatório");
        }

        [Fact]
        public void Alterar_Should_ThrowException_When_DoadorIsChanged()
        {
            // Arrange
            var livroOriginal = _livroFaker.Generate();
            var livroAlterado = new Livro(livroOriginal.Nome)
            {
                Id = livroOriginal.Id,
                DoadorId = livroOriginal.DoadorId + 1 // Alterando o DoadorId
            };

            _livroGatewayMock.Setup(g => g.ObterPorId(livroOriginal.Id)).Returns(livroOriginal);

            var useCase = new AlterarLivroUseCase(livroAlterado, _livroGatewayMock.Object);

            // Act
            Action action = () => useCase.Alterar();

            // Assert
            action.Should().Throw<DomainValidationException>()
            .Which.ValidationErrors.Should().Contain("Doador não pode ser alterado");
        }

        [Fact]
        public void Alterar_Should_NotThrowException_When_AllSpecificationsAreSatisfied()
        {
            // Arrange
            var livro = _livroFaker.Generate();

            _livroGatewayMock.Setup(g => g.ObterPorId(livro.Id)).Returns(livro);

            var useCase = new AlterarLivroUseCase(livro, _livroGatewayMock.Object);

            // Act
            Action action = () => useCase.Alterar();

            // Assert
            action.Should().NotThrow<DomainValidationException>();
        }
    }
}
