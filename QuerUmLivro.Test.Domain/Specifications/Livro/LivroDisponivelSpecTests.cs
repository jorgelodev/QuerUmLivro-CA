using Bogus;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Specifications.Livros;
using Xunit;

namespace QuerUmLivro.Test.Domain.Specifications.Livros
{
    public class LivroDisponivelSpecTests
    {
        private readonly Faker<Livro> _livroFaker;

        public LivroDisponivelSpecTests()
        {
            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName(), f.Random.Int(1, 100)))
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100));
        }

        [Fact]
        public void IsSatisfiedBy_ShouldReturnTrue_WhenLivroIsDisponivel()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            livro.Disponivel = true;
            var spec = new LivroDisponivelSpec();

            // Act
            var result = spec.IsSatisfiedBy(livro);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsSatisfiedBy_ShouldReturnFalse_WhenLivroIsNotDisponivel()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            livro.Disponivel = false;
            var spec = new LivroDisponivelSpec();

            // Act
            var result = spec.IsSatisfiedBy(livro);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ErrorMessage_ShouldReturnCorrectMessage()
        {
            // Arrange
            var spec = new LivroDisponivelSpec();

            // Act
            var errorMessage = spec.ErrorMessage;

            // Assert
            Assert.Equal("Livro não está mais disponível.", errorMessage);
        }
    }
}
