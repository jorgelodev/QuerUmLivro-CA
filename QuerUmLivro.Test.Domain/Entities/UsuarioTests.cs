using Bogus;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Test.Domain.Entities
{
    public class UsuarioTests
    {
        private readonly Usuario _usuarioFaker;
        private readonly Email _emailFaker;

        public UsuarioTests()
        {
            _emailFaker = new Faker<Email>()
                .CustomInstantiator(f => new Email(f.Internet.Email()))
                .Generate();

            _usuarioFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Name.FullName(), _emailFaker))
                .RuleFor(u => u.Desativado, f => f.Random.Bool())
                .Generate();
        }

        [Fact]
        public void Usuario_ShouldBeCreated_WhenValidParameters()
        {
            // Arrange
            var nome = _usuarioFaker.Nome;
            var email = _emailFaker;

            // Act
            var usuario = new Usuario(nome, email);

            // Arrenge/Act/Assert
            Assert.Equal(nome, _usuarioFaker.Nome);
            Assert.Equal(email, _usuarioFaker.Email);
            Assert.False(usuario.Desativado);
            Assert.Null(usuario.Interesses);
            Assert.Null(usuario.Livros);
        }

        [Fact]
        public void Usuario_ShouldThrowDomainValidationException_WhenNomeIsInvalid()
        {
            // Arrange
            var invalidNome = "";
            var email = _emailFaker;

            // Act & Assert
            var exception = Assert.Throws<DomainValidationException>(() => new Usuario(invalidNome, email));
            Assert.Contains("Nome é obrigatório e com máximo de 100 caracteres.", exception.ValidationErrors);
        }

        [Fact]
        public void Usuario_ShouldThrowDomainValidationException_WhenNomeIsTooLong()
        {
            // Arrange
            var invalidNome = new string('a', 101);
            var email = _emailFaker;

            // Act & Assert
            var exception = Assert.Throws<DomainValidationException>(() => new Usuario(invalidNome, email));
            Assert.Contains("Nome é obrigatório e com máximo de 100 caracteres.", exception.ValidationErrors);
        }
           
    }
}
