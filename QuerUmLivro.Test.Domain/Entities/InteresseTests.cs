using FluentAssertions;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Test.Domain.Entities
{
    public class InteresseTests
    {
        [Fact]
        public void Interesse_Should_ThrowException_When_LivroIdIsInvalid()
        {
            // Arrange
            var livroId = 0;
            var interessadoId = 1;
            var justificativa = "Justificativa válida";
            var status = StatusInteresse.EmAnalise;
            var data = DateTime.Now;

            // Act
            Action act = () => new Interesse(livroId, interessadoId, justificativa, status, data);

            // Assert
            act.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Livro é obrigatório.");
        }

        [Fact]
        public void Interesse_Should_ThrowException_When_InteressadoIdIsInvalid()
        {
            // Arrange
            var livroId = 1;
            var interessadoId = 0;
            var justificativa = "Justificativa válida";
            var status = StatusInteresse.EmAnalise;
            var data = DateTime.Now;

            // Act
            Action act = () => new Interesse(livroId, interessadoId, justificativa, status, data);

            // Assert
            act.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Interessado é obrigatório.");
        }

        [Fact]
        public void Interesse_Should_ThrowException_When_JustificativaIsInvalid()
        {
            // Arrange
            var livroId = 1;
            var interessadoId = 1;
            var justificativa = "";
            var status = StatusInteresse.EmAnalise;
            var data = DateTime.Now;

            // Act
            Action act = () => new Interesse(livroId, interessadoId, justificativa, status, data);

            // Assert
            act.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Justificativa é obrigatória e com máximo de 500 caracteres.");
        }

        [Fact]
        public void Interesse_Should_CreateInstance_When_AllParametersAreValid()
        {
            // Arrange
            var livroId = 1;
            var interessadoId = 1;
            var justificativa = "Justificativa válida";
            var status = StatusInteresse.EmAnalise;
            var data = DateTime.Now;

            // Act
            var interesse = new Interesse(livroId, interessadoId, justificativa, status, data);

            // Assert
            interesse.LivroId.Should().Be(livroId);
            interesse.InteressadoId.Should().Be(interessadoId);
            interesse.Justificativa.Should().Be(justificativa);
            interesse.Status.Should().Be(status);
            interesse.Data.Should().Be(data);
        }

        [Fact]
        public void Aprovar_Should_ChangeStatusToAprovado_When_StatusIsEmAnalise()
        {
            // Arrange
            var interesse = new Interesse(1, 1, "Justificativa válida", StatusInteresse.EmAnalise, DateTime.Now);

            // Act
            interesse.Aprovar();

            // Assert
            interesse.Status.Should().Be(StatusInteresse.Aprovado);
        }

        [Fact]
        public void Aprovar_Should_ThrowException_When_StatusIsNotEmAnalise()
        {
            // Arrange
            var interesse = new Interesse(1, 1, "Justificativa válida", StatusInteresse.Aprovado, DateTime.Now);

            // Act
            Action act = () => interesse.Aprovar();

            // Assert
            act.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Interesse não está em análise.");
        }

        [Fact]
        public void Reprovar_Should_ChangeStatusToReprovado_When_StatusIsEmAnalise()
        {
            // Arrange
            var interesse = new Interesse(1, 1, "Justificativa válida", StatusInteresse.EmAnalise, DateTime.Now);

            // Act
            interesse.Reprovar();

            // Assert
            interesse.Status.Should().Be(StatusInteresse.Reprovado);
        }

        [Fact]
        public void Reprovar_Should_ThrowException_When_StatusIsNotEmAnalise()
        {
            // Arrange
            var interesse = new Interesse(1, 1, "Justificativa válida", StatusInteresse.Aprovado, DateTime.Now);

            // Act
            Action act = () => interesse.Reprovar();

            // Assert
            act.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Interesse não está em análise.");
        }
    }
}