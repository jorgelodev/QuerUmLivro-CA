using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Specifications.Livros;


namespace QuerUmLivro.Test.Domain.Specifications.Livros
{
    public class LivroEstaSendoUtilizadoSpecTests
    {
        private readonly Mock<IInteresseGateway> _interesseGatewayMock;
        private readonly LivroEstaSendoUtilizadoSpec _specification;
        private readonly Faker<Livro> _livroFaker;
        private readonly Faker<Interesse> _interesseFaker;

        public LivroEstaSendoUtilizadoSpecTests()
        {
            _interesseGatewayMock = new Mock<IInteresseGateway>();
            _specification = new LivroEstaSendoUtilizadoSpec(_interesseGatewayMock.Object);

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName()))
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100));

            _interesseFaker = new Faker<Interesse>()
            .CustomInstantiator(f => new Interesse(
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                f.Lorem.Sentence(), StatusInteresse.EmAnalise,
                f.Date.Past()))
            .RuleFor(i => i.LivroId, (f, i) => i.Livro.Id)
            .RuleFor(i => i.InteressadoId, (f, i) => i.Interessado.Id);
        }

        [Fact]
        public void ReturnFalse_When_LivroIsBeingUsed()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            var interesse = _interesseFaker.Generate();
            interesse.LivroId = livro.Id;

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(livro.Id))
                .Returns(new List<Interesse> { interesse }.ToList());

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrue_When_LivroIsNotBeingUsed()
        {
            // Arrange
            var livro = _livroFaker.Generate();

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(livro.Id))
                .Returns(Enumerable.Empty<Interesse>().ToList());

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeTrue();
        }

    
    }
}