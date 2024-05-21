using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Specifications.Interesses;

namespace QuerUmLivro.Test.Domain.Specifications.Interesses
{
    public class InteresseLivroExisteSpecTests
    {   
        private readonly Faker<Interesse> _interesseFaker;

        public InteresseLivroExisteSpecTests()
        {
            var livroGatewayMock = new Mock<ILivroGateway>();            

            _interesseFaker = new Faker<Interesse>()
                .CustomInstantiator(f => new Interesse(
                    f.Random.Int(1, 100),
                    f.Random.Int(1, 100),
                    f.Lorem.Sentence(),
                    StatusInteresse.EmAnalise,
                    f.Date.Past()));
        }

        [Fact]
        public void ReturnsTrue_WhenLivroExists()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();
            var livro = new Livro("Livro Teste", 1);
            var livroGatewayMock = new Mock<ILivroGateway>();
            livroGatewayMock.Setup(gateway => gateway.ObterPorId(interesse.LivroId)).Returns(livro);
            var spec = new InteresseLivroExisteSpec(livroGatewayMock.Object);

            // Act
            var result = spec.IsSatisfiedBy(interesse);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalse_WhenLivroDoesNotExist()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();
            var livroGatewayMock = new Mock<ILivroGateway>();
            livroGatewayMock.Setup(gateway => gateway.ObterPorId(interesse.LivroId)).Returns((Livro)null);
            var spec = new InteresseLivroExisteSpec(livroGatewayMock.Object);

            // Act
            var result = spec.IsSatisfiedBy(interesse);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ErrorMessage_ShouldReturnCorrectMessage()
        {
            // Arrange
            var livroGatewayMock = new Mock<ILivroGateway>();
            var spec = new InteresseLivroExisteSpec(livroGatewayMock.Object);

            // Act
            var errorMessage = spec.ErrorMessage;

            // Assert
            errorMessage.Should().Be("Livro não existe.");
        }
    }
}
