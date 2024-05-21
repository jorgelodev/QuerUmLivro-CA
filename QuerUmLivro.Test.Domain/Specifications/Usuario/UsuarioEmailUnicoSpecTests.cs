using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Specifications.Usuarios;

namespace QuerUmLivro.Test.Domain.Specifications.Usuarios
{
    public class UsuarioEmailUnicoSpecTests
    {
        private readonly UsuarioEmailUnicoSpec _specification;
        private readonly Faker<Usuario> _usuarioFaker;

        public UsuarioEmailUnicoSpecTests()
        {
            var usuarioGatewayMock = new Mock<IUsuarioGateway>();
            _specification = new UsuarioEmailUnicoSpec(usuarioGatewayMock.Object);

            _usuarioFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Person.FullName, new Email(f.Person.Email)));
        }

        [Fact]
        public void IsSatisfiedBy_ReturnsTrue_WhenEmailIsUnique()
        {
            // Arrange
            var usuario = _usuarioFaker.Generate();
            var usuarioGatewayMock = new Mock<IUsuarioGateway>();
            usuarioGatewayMock.Setup(gateway => gateway.EmailJaUtilizado(usuario)).Returns(false);
            var spec = new UsuarioEmailUnicoSpec(usuarioGatewayMock.Object);

            // Act
            var result = spec.IsSatisfiedBy(usuario);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSatisfiedBy_ReturnsFalse_WhenEmailIsNotUnique()
        {
            // Arrange
            var usuario = _usuarioFaker.Generate();
            var usuarioGatewayMock = new Mock<IUsuarioGateway>();
            usuarioGatewayMock.Setup(gateway => gateway.EmailJaUtilizado(usuario)).Returns(true);
            var spec = new UsuarioEmailUnicoSpec(usuarioGatewayMock.Object);

            // Act
            var result = spec.IsSatisfiedBy(usuario);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ErrorMessage_ShouldReturnCorrectMessage()
        {
            // Arrange
            var usuarioGatewayMock = new Mock<IUsuarioGateway>();
            var spec = new UsuarioEmailUnicoSpec(usuarioGatewayMock.Object);

            // Act
            var errorMessage = spec.ErrorMessage;

            // Assert
            errorMessage.Should().Be("E-mail já está sendo utilizado.");
        }
    }
}
