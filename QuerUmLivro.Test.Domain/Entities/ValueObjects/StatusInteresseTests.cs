using FluentAssertions;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Test.Domain.Entities.ValueObjects
{
    public class StatusInteresseTests
    {
        [Fact]
        public void EmAnalise_Should_Have_Correct_Values()
        {
            // Arrange & Act
            var status = StatusInteresse.EmAnalise;

            // Assert
            status.Value.Should().Be(StatusInteresse.StatusInteresseEnum.EM_ANALISE);
            status.Text.Should().Be("Em Análise");
        }

        [Fact]
        public void Aprovado_Should_Have_Correct_Values()
        {
            // Arrange & Act
            var status = StatusInteresse.Aprovado;

            // Assert
            status.Value.Should().Be(StatusInteresse.StatusInteresseEnum.APROVADO);
            status.Text.Should().Be("Aprovado");
        }

        [Fact]
        public void Reprovado_Should_Have_Correct_Values()
        {
            // Arrange & Act
            var status = StatusInteresse.Reprovado;

            // Assert
            status.Value.Should().Be(StatusInteresse.StatusInteresseEnum.REPROVADO);
            status.Text.Should().Be("Reprovado");
        }

        [Fact]
        public void From_Should_Return_Correct_Status()
        {
            // Arrange & Act
            var statusEmAnalise = StatusInteresse.From(StatusInteresse.StatusInteresseEnum.EM_ANALISE);
            var statusAprovado = StatusInteresse.From(StatusInteresse.StatusInteresseEnum.APROVADO);
            var statusReprovado = StatusInteresse.From(StatusInteresse.StatusInteresseEnum.REPROVADO);

            // Assert
            statusEmAnalise.Should().Be(StatusInteresse.EmAnalise);
            statusAprovado.Should().Be(StatusInteresse.Aprovado);
            statusReprovado.Should().Be(StatusInteresse.Reprovado);
        }

        [Fact]
        public void From_Should_Throw_Exception_For_Invalid_Value()
        {
            // Arrange
            var invalidValue = (StatusInteresse.StatusInteresseEnum)999;

            // Act
            Action act = () => StatusInteresse.From(invalidValue);

            // Assert
            act.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Valor inválido para StatusInteresseEnum");
        }

        [Fact]
        public void Equals_Should_Return_True_For_Same_Status()
        {
            // Arrange
            var status1 = StatusInteresse.EmAnalise;
            var status2 = StatusInteresse.EmAnalise;

            // Act & Assert
            status1.Should().Be(status2);
        }

        [Fact]
        public void Equals_Should_Return_False_For_Different_Status()
        {
            // Arrange
            var status1 = StatusInteresse.EmAnalise;
            var status2 = StatusInteresse.Aprovado;

            // Act & Assert
            status1.Should().NotBe(status2);
        }

        [Fact]
        public void GetHashCode_Should_Return_Same_HashCode_For_Same_Status()
        {
            // Arrange
            var status1 = StatusInteresse.EmAnalise;
            var status2 = StatusInteresse.EmAnalise;

            // Act & Assert
            status1.GetHashCode().Should().Be(status2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_Should_Return_Different_HashCodes_For_Different_Status()
        {
            // Arrange
            var status1 = StatusInteresse.EmAnalise;
            var status2 = StatusInteresse.Aprovado;

            // Act & Assert
            status1.GetHashCode().Should().NotBe(status2.GetHashCode());
        }

        [Fact]
        public void ToString_Should_Return_Correct_Text()
        {
            // Arrange
            var status = StatusInteresse.EmAnalise;

            // Act
            var result = status.ToString();

            // Assert
            result.Should().Be("Em Análise");
        }
    }

}
