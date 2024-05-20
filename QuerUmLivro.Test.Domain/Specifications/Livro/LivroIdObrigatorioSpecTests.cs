using Bogus;
using FluentAssertions;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Specifications.Livros;


namespace QuerUmLivro.Test.Domain.Specifications.Livros
{
    public class LivroIdObrigatorioSpecTests
    {
        private readonly LivroIdObrigatorioSpec _specification;
        private readonly Faker<Livro> _livroFaker;

        public LivroIdObrigatorioSpecTests()
        {
            _specification = new LivroIdObrigatorioSpec();

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName()))
                .RuleFor(l => l.Id, f => f.Random.Int(1, 100));
        }

        [Fact]
        public void Should_ReturnFalse_When_IdIsZero()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            livro.Id = 0;

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Should_ReturnTrue_When_IdIsGreaterThanZero()
        {
            // Arrange
            var livro = _livroFaker.Generate();

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeTrue();
        }
       
    }
}
