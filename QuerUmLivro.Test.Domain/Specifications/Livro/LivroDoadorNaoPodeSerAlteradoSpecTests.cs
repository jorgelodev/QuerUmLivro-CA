using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Specifications.Livros;

namespace QuerUmLivro.Test.Domain.Specifications.Livros
{
    public class LivroDoadorNaoPodeSerAlteradoSpecTests
    {
        private readonly Mock<ILivroGateway> _livroGatewayMock;
        private readonly LivroDoadorNaoPodeSerAlteradoSpec _specification;
        private readonly Faker<Livro> _livroFaker;

        public LivroDoadorNaoPodeSerAlteradoSpecTests()
        {
            _livroGatewayMock = new Mock<ILivroGateway>();
            _specification = new LivroDoadorNaoPodeSerAlteradoSpec(_livroGatewayMock.Object);

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName(), f.Random.Int(1, 100)))
                .RuleFor(l => l.Id, f => f.Random.Int(1, 100))
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100));
        }

        [Fact]
        public void Should_ReturnFalse_When_LivroDoesNotExist()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            _livroGatewayMock.Setup(g => g.ObterPorId(livro.Id)).Returns((Livro)null);

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Should_ReturnFalse_When_DoadorIsDifferent()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            var livroFromGateway = _livroFaker.Generate();
            livroFromGateway.Id = livro.Id;
            livroFromGateway.DoadorId = livro.DoadorId + 1;

            _livroGatewayMock.Setup(g => g.ObterPorId(livro.Id)).Returns(livroFromGateway);

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Should_ReturnTrue_When_DoadorIsSame()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            var livroFromGateway = _livroFaker.Generate();
            livroFromGateway.Id = livro.Id;
            livroFromGateway.DoadorId = livro.DoadorId;

            _livroGatewayMock.Setup(g => g.ObterPorId(livro.Id)).Returns(livroFromGateway);

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeTrue();
        }


    }
}
