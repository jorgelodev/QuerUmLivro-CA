using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.UseCases.Livros;

namespace QuerUmLivro.Test.Domain.UseCases.Livros
{
    public class DeletarLivroUseCaseTests
    {
        private readonly Mock<IInteresseGateway> _interesseGatewayMock;
        private readonly Faker<Livro> _livroFaker;
        private readonly Faker<Interesse> _interesseFaker;

        public DeletarLivroUseCaseTests()
        {
            _interesseGatewayMock = new Mock<IInteresseGateway>();

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName()))
                .RuleFor(l => l.Id, f => f.Random.Int(1, 100));

            _interesseFaker = new Faker<Interesse>()
            .CustomInstantiator(f => new Interesse(
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                f.Lorem.Sentence(),
                StatusInteresse.EmAnalise,
                f.Date.Past()))
            .RuleFor(i => i.LivroId, (f, i) => i.Livro.Id)
            .RuleFor(i => i.InteressadoId, (f, i) => i.Interessado.Id);
        }

        [Fact]
        public void Deletar_Should_ThrowException_When_LivroIdIsZero()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            livro.Id = 0;

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(livro.Id)).Returns(new List<Interesse>());

            var useCase = new DeletarLivroUseCase(livro, _interesseGatewayMock.Object);

            // Act
            Action action = () => useCase.Deletar();

            // Assert
            action.Should().Throw<DomainValidationException>()               
            .Which.ValidationErrors.Should().Contain("Id do Livro não informado");
        }

        [Fact]
        public void Deletar_Should_ThrowException_When_LivroIsBeingUsed()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            var interesse = _interesseFaker.Generate();
            interesse.LivroId = livro.Id;

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(livro.Id)).Returns(new[] { interesse });

            var useCase = new DeletarLivroUseCase(livro, _interesseGatewayMock.Object);

            // Act
            Action action = () => useCase.Deletar();

            // Assert
            action.Should().Throw<DomainValidationException>()         
            .Which.ValidationErrors.Should().Contain("Livro possui interesse, não pode ser excluído");
        }

        [Fact]
        public void Deletar_Should_NotThrowException_When_LivroIsNotBeingUsed()
        {
            // Arrange
            var livro = _livroFaker.Generate();

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(livro.Id)).Returns(new List<Interesse>());


            var useCase = new DeletarLivroUseCase(livro, _interesseGatewayMock.Object);

            // Act & Assert
            useCase.Invoking(x => x.Deletar()).Should().NotThrow();
        }
    }
}
