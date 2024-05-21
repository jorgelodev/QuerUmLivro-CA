using Bogus;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Specifications.Livros;


namespace QuerUmLivro.Test.Domain.Specifications.Livros
{
    public class LivroDoadorEhDoadorDoLivroSpecTests
    {
        private readonly Faker<Usuario> _usuarioFaker;
        private readonly Faker<Livro> _livroFaker;

        public LivroDoadorEhDoadorDoLivroSpecTests()
        {
            _usuarioFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Name.FullName(), new Email(f.Internet.Email())))
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100));

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName(),f.Random.Int(1, 100)));
        }

        [Fact]
        public void IsSatisfiedBy_ShouldReturnTrue_WhenDoadorIsTheSameAsLivroDoador()
        {
            // Arrange
            var doador = _usuarioFaker.Generate();
            var livro = _livroFaker.Generate();
            livro.DoadorId = doador.Id;

            var spec = new LivroDoadorEhDoadorDoLivroSpec(doador);

            // Act
            var result = spec.IsSatisfiedBy(livro);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsSatisfiedBy_ShouldReturnFalse_WhenDoadorIsNotTheSameAsLivroDoador()
        {
            // Arrange
            var doador = _usuarioFaker.Generate();
            var livro = _livroFaker.Generate();

            var spec = new LivroDoadorEhDoadorDoLivroSpec(doador);

            // Act
            var result = spec.IsSatisfiedBy(livro);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ErrorMessage_ShouldReturnCorrectMessage()
        {
            // Arrange
            var doador = _usuarioFaker.Generate();
            var spec = new LivroDoadorEhDoadorDoLivroSpec(doador);

            // Act
            var errorMessage = spec.ErrorMessage;

            // Assert
            Assert.Equal("Doador informado não pode aprovar pois não é doador do Livro.", errorMessage);
        }
    }
}
