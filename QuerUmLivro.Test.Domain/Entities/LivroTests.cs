using FluentAssertions;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Exceptions;


namespace QuerUmLivro.Test.Domain.Entities
{
    public class LivroTests
    {
        [Fact]
        public void Livro_Should_ThrowException_When_NomeIsNullOrWhitespace()
        {
            // Arrange
            string nomeInvalido = null;

            // Act
            Action act = () => new Livro(nomeInvalido);

            // Assert
            act.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Nome é obrigatório e com máximo de 100 caracteres.");
        }

        [Fact]
        public void Livro_Should_ThrowException_When_NomeExceedsMaxLength()
        {
            // Arrange
            string nomeInvalido = new string('a', 101);

            // Act
            Action act = () => new Livro(nomeInvalido);

            // Assert
            act.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Nome é obrigatório e com máximo de 100 caracteres.");
        }

        [Fact]
        public void Livro_Should_NotThrowException_When_NomeIsValid()
        {
            // Arrange
            string nomeValido = "Nome do Livro";

            // Act
            Action act = () => new Livro(nomeValido);

            // Assert
            act.Should().NotThrow<DomainValidationException>();
        }
        
    }
}
