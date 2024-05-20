using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.UseCases.Livros;

namespace QuerUmLivro.Test.Domain.UseCases.Livros
{
    public class CadastrarLivroUseCaseTests
    {
        private readonly Mock<IUsuarioGateway> _usuarioGatewayMock;
        private readonly Faker<Livro> _livroFaker;

        public CadastrarLivroUseCaseTests()
        {
            _usuarioGatewayMock = new Mock<IUsuarioGateway>();

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName()))
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100));
        }

        [Fact]
        public void Cadastrar_Should_ThrowException_When_DoadorIdIsNotInformed()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            livro.DoadorId = 0;

            var useCase = new CadastrarLivroUseCase(livro, _usuarioGatewayMock.Object);

            // Act
            Action action = () => useCase.Cadastrar();

            // Assert
            action.Should().Throw<DomainValidationException>()                
            .Which.ValidationErrors.Should().Contain("Doador é obrigatório");
        }

        [Fact]
        public void Cadastrar_Should_ThrowException_When_DoadorDoesNotExist()
        {
            // Arrange
            var livro = _livroFaker.Generate();

            _usuarioGatewayMock.Setup(g => g.ObterPorId(livro.DoadorId)).Returns((Usuario)null);

            var useCase = new CadastrarLivroUseCase(livro, _usuarioGatewayMock.Object);

            // Act
            Action action = () => useCase.Cadastrar();

            // Assert
            action.Should().Throw<DomainValidationException>()               
            .Which.ValidationErrors.Should().Contain("Doador não existe. Informe um usuário cadastrado.");
        }

        [Fact]
        public void Cadastrar_Should_SetDisponivelToTrue()
        {
            // Arrange
            var livro = _livroFaker.Generate();

            _usuarioGatewayMock.Setup(g => g.ObterPorId(livro.DoadorId)).Returns(new Usuario());

            var useCase = new CadastrarLivroUseCase(livro, _usuarioGatewayMock.Object);

            // Act
            var result = useCase.Cadastrar();

            // Assert
            result.Disponivel.Should().BeTrue();
        }

        [Fact]
        public void Cadastrar_Should_ReturnLivro()
        {
            // Arrange
            var livro = _livroFaker.Generate();

            _usuarioGatewayMock.Setup(g => g.ObterPorId(livro.DoadorId)).Returns(new Usuario());

            var useCase = new CadastrarLivroUseCase(livro, _usuarioGatewayMock.Object);

            // Act
            var result = useCase.Cadastrar();

            // Assert
            result.Should().BeEquivalentTo(livro);
        }
    }
}