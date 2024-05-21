using FluentAssertions;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Test.Domain.Entitie.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Should_Create_Email_When_Valid_Email_Is_Provided()
        {
            // Arrange
            var validEmail = "jorge@gmail.com";

            // Act
            var email = new Email(validEmail);

            // Assert
            email.EnderecoEmail.Should().Be(validEmail);
        }

        [Fact]
        public void Should_Throw_DomainValidationException_When_Email_Is_NullOrEmpty()
        {
            // Arrange
            var invalidEmail = "";

            // Act
            Action action = () => new Email(invalidEmail);

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Email é obrigatório.");
        }

        [Fact]
        public void Should_Throw_DomainValidationException_When_Email_Is_Invalid()
        {
            // Arrange
            var invalidEmail = "email.invalido";

            // Act
            Action action = () => new Email(invalidEmail);

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Formato de email inválido.");
        }

        [Fact]
        public void Emails_With_Same_Address_Should_Be_Equal()
        {
            // Arrange
            var email1 = new Email("jorge@gmail.com");
            var email2 = new Email("jorge@gmail.com");

            // Act & Assert
            email1.Should().Be(email2);
        }

        [Fact]
        public void Emails_With_Different_Addresses_Should_Not_Be_Equal()
        {
            // Arrange
            var email1 = new Email("jorge@gmail.com");
            var email2 = new Email("jorgelo@gmail.com");

            // Act & Assert
            email1.Should().NotBe(email2);
        }

        [Fact]
        public void Should_Return_Email_Address_When_ToString_Is_Called()
        {
            // Arrange
            var emailAddress = "jorge@gmail.com";
            var email = new Email(emailAddress);

            // Act
            var result = email.ToString();

            // Assert
            result.Should().Be(emailAddress);
        }

        [Fact]
        public void Should_Return_Same_HashCode_For_Emails_With_Same_Address()
        {
            // Arrange
            var email1 = new Email("jorge@gmail.com");
            var email2 = new Email("jorge@gmail.com");

            // Act & Assert
            email1.GetHashCode().Should().Be(email2.GetHashCode());
        }

        [Fact]
        public void Should_Return_Different_HashCode_For_Emails_With_Different_Addresses()
        {
            // Arrange
            var email1 = new Email("jorge@gmail.com");
            var email2 = new Email("jorgelo@gmail.com");

            // Act & Assert
            email1.GetHashCode().Should().NotBe(email2.GetHashCode());
        }
    }
}
