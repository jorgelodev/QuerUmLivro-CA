using Bogus;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Specifications.Interesses;

namespace QuerUmLivro.Test.Domain.Specifications.Interesses
{
    public class InteresseInteressadoExisteSpecTests
    {
        private readonly Faker<Usuario> _usuarioFaker;

        public InteresseInteressadoExisteSpecTests()
        {
            _usuarioFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Person.FullName, new Email(f.Person.Email)))
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100));
        }

        [Fact]
        public void ReturnsTrue_WhenInteressadoExists()
        {
            // Arrange
            var interessado = _usuarioFaker.Generate();
            var usuarioGatewayMock = new Mock<IUsuarioGateway>();
            usuarioGatewayMock.Setup(gateway => gateway.ObterPorId(interessado.Id)).Returns(interessado);

            var spec = new InteresseInteressadoExisteSpec(usuarioGatewayMock.Object);
            var interesse = new Interesse(1, interessado.Id, "Justificativa", StatusInteresse.EmAnalise, DateTime.Now);

            // Act
            var result = spec.IsSatisfiedBy(interesse);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ReturnsFalse_WhenInteressadoDoesNotExist()
        {
            // Arrange
            var usuarioGatewayMock = new Mock<IUsuarioGateway>();
            usuarioGatewayMock.Setup(gateway => gateway.ObterPorId(It.IsAny<int>())).Returns((Usuario)null);

            var spec = new InteresseInteressadoExisteSpec(usuarioGatewayMock.Object);
            var interesse = new Interesse(1, 9999, "Justificativa", StatusInteresse.EmAnalise, DateTime.Now);

            // Act
            var result = spec.IsSatisfiedBy(interesse);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ErrorMessage_ShouldReturnCorrectMessage()
        {
            // Arrange
            var usuarioGatewayMock = new Mock<IUsuarioGateway>();
            var spec = new InteresseInteressadoExisteSpec(usuarioGatewayMock.Object);

            // Act
            var errorMessage = spec.ErrorMessage;

            // Assert
            Assert.Equal("Interessado não existe. Informe um usuário cadastrado.", errorMessage);
        }
    }
}
