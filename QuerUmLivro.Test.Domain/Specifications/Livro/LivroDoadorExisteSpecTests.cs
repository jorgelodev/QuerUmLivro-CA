using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Specifications.Livros;


namespace QuerUmLivro.Test.Domain.Specifications.Livros
{
    public class LivroDoadorExisteSpecTests
    {
        private readonly LivroDoadorExisteSpec _specification;
        private readonly Mock<IUsuarioGateway> _usuarioGatewayMock;
        private readonly Faker<Livro> _livroFaker;
        private readonly Faker<Usuario> _usuarioFaker;

        public LivroDoadorExisteSpecTests()
        {
            _usuarioGatewayMock = new Mock<IUsuarioGateway>();
            _specification = new LivroDoadorExisteSpec(_usuarioGatewayMock.Object);

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName()))
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100));

            _usuarioFaker = new Faker<Usuario>()
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100))
                .RuleFor(u => u.Nome, f => f.Person.FullName);
        }

        [Fact]
        public void ReturnFalse_When_DoadorDoesNotExist()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            _usuarioGatewayMock.Setup(g => g.ObterPorId(livro.DoadorId)).Returns((Usuario)null);

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrue_When_DoadorExists()
        {
            // Arrange
            var livro = _livroFaker.Generate();

            var usuario = _usuarioFaker.Generate();
            usuario.Id = livro.DoadorId;

            _usuarioGatewayMock.Setup(g => g.ObterPorId(livro.DoadorId)).Returns(usuario);

            // Act
            var result = _specification.IsSatisfiedBy(livro);

            // Assert
            result.Should().BeTrue();
        }

    }

}