using Bogus;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Specifications.Interesses;

namespace QuerUmLivro.Test.Domain.Specifications.Interesses
{
    public class InteresseDoadorIdObrigatorioSpecTests
    {
        private readonly Faker<Interesse> _interesseFaker;
        private readonly Livro _livroFaker;

        public InteresseDoadorIdObrigatorioSpecTests()
        {
            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName(), f.Random.Int(1, 100)))
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100))
                .Generate();

            _interesseFaker = new Faker<Interesse>()
            .CustomInstantiator(f => new Interesse(
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                f.Lorem.Sentence(), StatusInteresse.EmAnalise,
                f.Date.Past()))
            .RuleFor(i => i.LivroId, f => _livroFaker.Id)
            .RuleFor(i => i.Livro, f => _livroFaker)
            .RuleFor(i => i.InteressadoId, (f, i) => f.Random.Int(1, 100));            
        }

        [Fact]
        public void ShouldReturnTrue_WhenDoadorIdIsGreaterThanZero()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();
            interesse.Livro.DoadorId = 1; // Simulando um ID de doador válido
            var spec = new InteresseDoadorIdObrigatorioSpec();

            // Act
            var result = spec.IsSatisfiedBy(interesse);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalse_WhenDoadorIdIsZero()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();
            interesse.Livro.DoadorId = 0; // Simulando um ID de doador inválido
            var spec = new InteresseDoadorIdObrigatorioSpec();

            // Act
            var result = spec.IsSatisfiedBy(interesse);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnFalse_WhenDoadorIdIsNegative()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();
            interesse.Livro.DoadorId = -1; // Simulando um ID de doador inválido
            var spec = new InteresseDoadorIdObrigatorioSpec();

            // Act
            var result = spec.IsSatisfiedBy(interesse);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ErrorMessage_ShouldReturnCorrectMessage()
        {
            // Arrange
            var spec = new InteresseDoadorIdObrigatorioSpec();

            // Act
            var errorMessage = spec.ErrorMessage;

            // Assert
            Assert.Equal("Id do doador deve ser informado.", errorMessage);
        }
    }
}
