using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.UseCases.Livros;

namespace QuerUmLivro.Test.Domain.UseCases.Interesses
{
    public class AprovarInteresseUseCaseTests
    {
        private readonly Mock<ILivroGateway> _livroGatewayMock;
        private readonly Mock<IUsuarioGateway> _usuarioGatewayMock;
        private readonly Faker<Interesse> _interesseFaker;
        private readonly Livro _livroFaker;
        private readonly Usuario _doadorFaker;
        private readonly Usuario _interessadoFaker;

        public AprovarInteresseUseCaseTests()
        {
            _livroGatewayMock = new Mock<ILivroGateway>();
            _usuarioGatewayMock = new Mock<IUsuarioGateway>();

            _doadorFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Person.UserName, new Email(f.Person.Email)))
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100))
                .Generate();

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName(), _doadorFaker.Id))
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100))
                .RuleFor(l => l.Doador, _doadorFaker)
                .RuleFor(l => l.Disponivel, f => true)
                .Generate();

            _interessadoFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Person.UserName, new Email(f.Person.Email)))
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100))
                .Generate();

            _interesseFaker = new Faker<Interesse>()
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100))
                .CustomInstantiator(f => new Interesse(
                   _livroFaker.Id,
                    _interessadoFaker.Id,
                    f.Lorem.Sentence(),
                    StatusInteresse.EmAnalise,
                    DateTime.Now)
                { 
                    Livro = _livroFaker ,
                    Interessado = _interessadoFaker
                });
        }

        [Fact]
        public void AprovarInteresse_Should_ThrowException_When_DoadorIdIsZero()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();
            interesse.Livro.DoadorId = 0;

            _usuarioGatewayMock.Setup(g => g.ObterPorId(interesse.InteressadoId)).Returns(interesse.Interessado);

            _livroGatewayMock.Setup(g => g.ObterPorId(interesse.LivroId)).Returns(interesse.Livro);            

            var useCase = new AprovarInteresseUseCase(interesse, _livroGatewayMock.Object, _usuarioGatewayMock.Object);

            // Act
            Action action = () => useCase.AprovarInteresse();

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Id do doador deve ser informado.");
        }

        [Fact]
        public void AprovarInteresse_Should_ThrowException_When_InteressadoDoesNotExist()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();

            _usuarioGatewayMock.Setup(g => g.ObterPorId(interesse.InteressadoId)).Returns((Usuario)null);

            _livroGatewayMock.Setup(g => g.ObterPorId(interesse.LivroId)).Returns(interesse.Livro);

            var useCase = new AprovarInteresseUseCase(interesse, _livroGatewayMock.Object, _usuarioGatewayMock.Object);

            // Act
            Action action = () => useCase.AprovarInteresse();

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Interessado não existe. Informe um usuário cadastrado.");
        }

        [Fact]
        public void AprovarInteresse_Should_ThrowException_When_LivroIsNotAvailable()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();
            interesse.Livro.Disponivel = false;

            _usuarioGatewayMock.Setup(g => g.ObterPorId(interesse.InteressadoId)).Returns(interesse.Interessado);

            _livroGatewayMock.Setup(g => g.ObterPorId(interesse.LivroId)).Returns(interesse.Livro);            

            var useCase = new AprovarInteresseUseCase(interesse, _livroGatewayMock.Object, _usuarioGatewayMock.Object);

            // Act
            Action action = () => useCase.AprovarInteresse();

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Livro não está mais disponível.");
        }

        [Fact]
        public void AprovarInteresse_Should_NotThrowException_When_AllSpecificationsAreSatisfied()
        {
            // Arrange
            var interesse = _interesseFaker.Generate();

            _usuarioGatewayMock.Setup(g => g.ObterPorId(interesse.InteressadoId)).Returns(interesse.Interessado);

            _livroGatewayMock.Setup(g => g.ObterPorId(interesse.LivroId)).Returns(interesse.Livro);

            var useCase = new AprovarInteresseUseCase(interesse, _livroGatewayMock.Object, _usuarioGatewayMock.Object);

            // Act & Assert
            useCase.Invoking(u => u.AprovarInteresse()).Should().NotThrow<DomainValidationException>();
        }
    }
}
