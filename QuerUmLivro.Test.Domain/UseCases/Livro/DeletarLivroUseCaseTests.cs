using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.UseCases.Livros;

namespace QuerUmLivro.Test.Domain.UseCases.Livros
{
    public class DeletarLivroUseCaseTests
    {
        private readonly Mock<IInteresseGateway> _interesseGatewayMock;
        private readonly Livro _livroFaker;
        private readonly Interesse _interesseFaker;
        private readonly Usuario _usuarioFaker;
        public DeletarLivroUseCaseTests()
        {
            _interesseGatewayMock = new Mock<IInteresseGateway>();

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName(), f.Random.Int(1, 100)))  
                .RuleFor(l => l.Id, f => f.Random.Int(1, 100))
                .Generate();

            _usuarioFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Person.FullName, new Email(f.Person.Email)))
              .RuleFor(u => u.Id, f => f.Random.Int(1, 100))              
              .Generate();

            _interesseFaker = new Faker<Interesse>()
            .CustomInstantiator(f => new Interesse(
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                f.Lorem.Sentence(),
                StatusInteresse.EmAnalise,
                f.Date.Past()))
            .RuleFor(i => i.Livro, (f, i) => _livroFaker)
            .RuleFor(i => i.LivroId, (f, i) => _livroFaker.Id)
            .RuleFor(i => i.Interessado, (f, i) => _usuarioFaker)
            .RuleFor(i => i.InteressadoId, (f, i) => _usuarioFaker.Id)
            .Generate();
        }

        [Fact]
        public void Deletar_Should_ThrowException_When_LivroIdIsZero()
        {
            // Arrange            
            _livroFaker.Id = 0;

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(_livroFaker.Id)).Returns(new List<Interesse>());

            var useCase = new DeletarLivroUseCase(_livroFaker, _interesseGatewayMock.Object);

            // Act
            Action action = () => useCase.Deletar();

            // Assert
            action.Should().Throw<DomainValidationException>()               
            .Which.ValidationErrors.Should().Contain("Id do Livro não informado");
        }

        [Fact]
        public void Deletar_Should_ThrowException_When_LivroIsBeingUsed()
        {
            // Arrange

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(_livroFaker.Id)).Returns(new[] { _interesseFaker });

            var useCase = new DeletarLivroUseCase(_livroFaker, _interesseGatewayMock.Object);

            // Act
            Action action = () => useCase.Deletar();

            // Assert
            action.Should().Throw<DomainValidationException>()         
            .Which.ValidationErrors.Should().Contain("Livro possui interesse, não pode ser excluído");
        }

        [Fact]
        public void Deletar_Should_NotThrowException_When_LivroIsNotBeingUsed()
        {
            // Arrange           

            _interesseGatewayMock.Setup(g => g.ObterPorLivro(_livroFaker.Id)).Returns(new List<Interesse>());


            var useCase = new DeletarLivroUseCase(_livroFaker, _interesseGatewayMock.Object);

            // Act & Assert
            useCase.Invoking(x => x.Deletar()).Should().NotThrow();
        }
    }
}
