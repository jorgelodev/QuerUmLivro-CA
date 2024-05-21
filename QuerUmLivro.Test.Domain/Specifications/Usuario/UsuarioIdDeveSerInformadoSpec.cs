using Bogus;
using FluentAssertions;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Specifications;
using QuerUmLivro.Domain.Specifications.Usuarios;
using Xunit;

namespace QuerUmLivro.Test.Domain.Specifications
{
    public class UsuarioIdDeveSerInformadoSpecTests
    {
        private readonly UsuarioIdDeveSerInformadoSpec _specification;
        private readonly Faker<Usuario> _usuarioFaker;

        public UsuarioIdDeveSerInformadoSpecTests()
        {
            _specification = new UsuarioIdDeveSerInformadoSpec();
            _usuarioFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Person.FullName, new Email(f.Person.Email)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void IsSatisfiedBy_ReturnsTrue_WhenIdIsGreaterThanZero(int id)
        {
            // Arrange
            var usuario = _usuarioFaker.Generate();
            usuario.Id = id;

            // Act
            var result = _specification.IsSatisfiedBy(usuario);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSatisfiedBy_ReturnsFalse_WhenIdIsZero()
        {
            // Arrange
            var usuario = _usuarioFaker.Generate();
            usuario.Id = 0;

            // Act
            var result = _specification.IsSatisfiedBy(usuario);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSatisfiedBy_ReturnsFalse_WhenIdIsNegative()
        {
            // Arrange
            var usuario = _usuarioFaker.Generate();
            usuario.Id = -1;

            // Act
            var result = _specification.IsSatisfiedBy(usuario);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ErrorMessage_ShouldReturnCorrectMessage()
        {
            // Act
            var errorMessage = _specification.ErrorMessage;

            // Assert
            errorMessage.Should().Be("Id usuário não informado");
        }
    }
}
