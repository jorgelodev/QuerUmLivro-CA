using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Specifications.Interesses;

namespace QuerUmLivro.Test.Domain.Specifications.Interesses
{
    public class InteresseInteressadoNaoManifestouSpecTests
    {
        private readonly InteresseInteressadoNaoManifestouSpec _specification;
        private readonly Faker<Interesse> _interesseFaker;

        public InteresseInteressadoNaoManifestouSpecTests()
        {
            var interesseGatewayMock = new Mock<IInteresseGateway>();
            _specification = new InteresseInteressadoNaoManifestouSpec(interesseGatewayMock.Object);

            _interesseFaker = new Faker<Interesse>()
                .CustomInstantiator(f => new Interesse(
                    f.Random.Int(1, 100),
                    f.Random.Int(1, 100),
                    f.Lorem.Sentence(),
                    StatusInteresse.EmAnalise,
                    f.Date.Past()));
        }

        [Fact]
        public void ReturnsTrue_WhenInteressadoHasNotManifestedInterest()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();
            var interesseGatewayMock = new Mock<IInteresseGateway>();
            interesseGatewayMock.Setup(gateway => gateway.ObterPorLivroEInteressado(interesse)).Returns((Interesse)null);
            var spec = new InteresseInteressadoNaoManifestouSpec(interesseGatewayMock.Object);

            // Act
            var result = spec.IsSatisfiedBy(interesse);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalse_WhenInteressadoHasManifestedInterest()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();
            var interesseEncontrado = new Interesse(
                interesse.LivroId,
                interesse.InteressadoId,
                interesse.Justificativa,
                interesse.Status,
                interesse.Data);

            var interesseGatewayMock = new Mock<IInteresseGateway>();
            interesseGatewayMock.Setup(gateway => gateway.ObterPorLivroEInteressado(interesse)).Returns(interesseEncontrado);
            var spec = new InteresseInteressadoNaoManifestouSpec(interesseGatewayMock.Object);

            // Act
            var result = spec.IsSatisfiedBy(interesse);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ErrorMessage_ShouldReturnCorrectMessage()
        {
            // Arrange
            var interesseGatewayMock = new Mock<IInteresseGateway>();
            var spec = new InteresseInteressadoNaoManifestouSpec(interesseGatewayMock.Object);

            // Act
            var errorMessage = spec.ErrorMessage;

            // Assert
            errorMessage.Should().Be("Interessado já manifestou interesse nesse livro.");
        }
    }
}
