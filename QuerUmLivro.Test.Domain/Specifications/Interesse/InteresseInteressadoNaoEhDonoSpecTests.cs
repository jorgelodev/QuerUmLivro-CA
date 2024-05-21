using Bogus;
using FluentAssertions;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Specifications.Interesses;

namespace QuerUmLivro.Test.Domain.Specifications.Interesses
{
    public class InteresseInteressadoNaoEhDonoSpecTests
    {
        private readonly InteresseInteressadoNaoEhDonoSpec _specification;
        private readonly Faker<Livro> _livroFaker;
        private readonly Faker<Usuario> _usuarioFaker;
        private readonly Faker<Interesse> _interesseFaker;

        public InteresseInteressadoNaoEhDonoSpecTests()
        {
            _specification = new InteresseInteressadoNaoEhDonoSpec();

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName(), f.Random.Int(1, 100)))
                .RuleFor(l => l.Id, f => f.Random.Int(1, 100))
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100));

            _usuarioFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Person.FullName, new Email(f.Person.Email)))
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100));

            _interesseFaker = new Faker<Interesse>()
                .CustomInstantiator(f => new Interesse(
                    f.Random.Int(1, 100),
                    f.Random.Int(1, 100),
                    f.Lorem.Sentence(),
                    StatusInteresse.EmAnalise,
                    f.Date.Past()))
                .RuleFor(i => i.Livro, _livroFaker.Generate())
                .RuleFor(i => i.Interessado, _usuarioFaker.Generate());
        }

        [Fact]
        public void ReturnsTrue_WhenInteressadoIsNotTheOwner()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();

            // Act
            var result = _specification.IsSatisfiedBy(interesse);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalse_WhenInteressadoIsTheOwner()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            var interesse = new Interesse(livro.Id, livro.DoadorId, "Justificativa", StatusInteresse.EmAnalise, DateTime.Now);
            interesse.Livro = livro;
            // Act
            var result = _specification.IsSatisfiedBy(interesse);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ErrorMessage_ShouldReturnCorrectMessage()
        {
            // Act
            var errorMessage = _specification.ErrorMessage;

            // Assert
            errorMessage.Should().Be("Dono do livro não pode manifestar interesse no próprio livro.");
        }
    }
}
