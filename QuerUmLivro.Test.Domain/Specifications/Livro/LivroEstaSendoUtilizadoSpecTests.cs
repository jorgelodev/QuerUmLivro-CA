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
        private readonly Livro _livroFaker;
        private readonly Interesse _interesseFaker;
        private readonly Usuario _usuarioFaker;

        public LivroEstaSendoUtilizadoSpecTests()
        {
            _interesseGatewayMock = new Mock<IInteresseGateway>();
            _specification = new LivroEstaSendoUtilizadoSpec(_interesseGatewayMock.Object);

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName(), f.Random.Int(1, 100)))
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100))
                .Generate();

            _usuarioFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Person.FullName, new Email(f.Person.Email)))
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100));

            _interesseFaker = new Faker<Interesse>()
            .CustomInstantiator(f => new Interesse(
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                f.Lorem.Sentence(), StatusInteresse.EmAnalise,
                f.Date.Past()))
            .RuleFor(i => i.LivroId, (f, i) => _livroFaker.Id)
            .RuleFor(i => i.InteressadoId, (f, i) => _usuarioFaker.Id)
            .RuleFor(i => i.Interessado, (f, i) => _usuarioFaker)
            .Generate();
        }

        [Fact]
        public void ReturnFalse_When_LivroIsBeingUsed()
        {
            // Arrange

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(_livroFaker.Id))
                .Returns(new List<Interesse> { _interesseFaker }.ToList());

            // Act
            var result = _specification.IsSatisfiedBy(_livroFaker);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrue_When_LivroIsNotBeingUsed()
        {
            // Arrange   

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(_livroFaker.Id))
                .Returns(Enumerable.Empty<Interesse>().ToList());

            // Act
            var result = _specification.IsSatisfiedBy(_livroFaker);

            // Assert
            result.Should().BeTrue();
        }

    
    }
}