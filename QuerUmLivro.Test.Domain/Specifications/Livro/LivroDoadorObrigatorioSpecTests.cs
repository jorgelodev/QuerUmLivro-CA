using Bogus;
using FluentAssertions;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Specifications.Livros;


namespace QuerUmLivro.Test.Domain.Specifications.Livros
{
    public class LivroDoadorObrigatorioSpecTests
    {
        private readonly LivroDoadorObrigatorioSpec _specification;
        private readonly Faker<Livro> _livroFaker;

        public LivroDoadorObrigatorioSpecTests()
        {
            _specification = new LivroDoadorObrigatorioSpec();

            _livroFaker = new Faker<Livro>()
                  .CustomInstantiator(f => new Livro(f.Commerce.ProductName()));
        }

        [Fact]
        public void ReturnFalse_When_DoadorIdIsNotInformed()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            livro.DoadorId = 0;

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeFalse();
        }
   

        [Fact]
        public void ReturnTrue_When_DoadorIdIsInformed()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            livro.DoadorId = 1;

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeTrue();
        }
    }
}