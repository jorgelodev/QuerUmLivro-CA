using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Infra.Services;
using QuerUmLivro.Infra.Services.DTOs.Livros;

namespace QuerUmLivro.Test.Infra.Services
{
    public class LivroServiceTests
    {
        private readonly Mock<ILivroGateway> _livroGatewayMock;
        private readonly Mock<IUsuarioGateway> _usuarioGatewayMock;
        private readonly Mock<IInteresseGateway> _interesseGatewayMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly LivroService _livroService;
        private readonly Faker<Livro> _livroFaker;
        private readonly Faker<AlteraLivroDto> _alteraLivroDtoFaker;
        private readonly Faker<CadastraLivroDto> _cadastraLivroDtoFaker;
        private readonly Faker<LivroDto> _livroDtoFaker;
        private readonly Usuario _usuarioFaker;

        public LivroServiceTests()
        {
            _livroGatewayMock = new Mock<ILivroGateway>();
            _usuarioGatewayMock = new Mock<IUsuarioGateway>();
            _interesseGatewayMock = new Mock<IInteresseGateway>();
            _mapperMock = new Mock<IMapper>();
            _livroService = new LivroService(
                _livroGatewayMock.Object,
                _mapperMock.Object,
                _usuarioGatewayMock.Object,
                _interesseGatewayMock.Object);

            _usuarioFaker = new Faker<Usuario>()
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100))
                .RuleFor(u => u.Nome, f => f.Person.FullName)
                .Generate();

            _livroFaker = new Faker<Livro>()
                .CustomInstantiator(f => new Livro(f.Commerce.ProductName()))
                .RuleFor(l => l.Id, f => f.Random.Int(1, 100))                
                .RuleFor(l => l.DoadorId, f => _usuarioFaker.Id)
                .RuleFor(l => l.Doador, f => _usuarioFaker)
                .RuleFor(l => l.Disponivel, f => f.Random.Bool());

            _alteraLivroDtoFaker = new Faker<AlteraLivroDto>()
                .RuleFor(a => a.Id, f => f.Random.Int(1, 100))
                .RuleFor(a => a.Nome, f => f.Commerce.ProductName());

            _cadastraLivroDtoFaker = new Faker<CadastraLivroDto>()
                .RuleFor(c => c.Nome, f => f.Commerce.ProductName())
                .RuleFor(c => c.DoadorId, f => f.Random.Int(1, 100));

            _livroDtoFaker = new Faker<LivroDto>()
                .RuleFor(l => l.Id, f => f.Random.Int(1, 100))
                .RuleFor(l => l.Nome, f => f.Commerce.ProductName())
                .RuleFor(l => l.DoadorId, f => f.Random.Int(1, 100))
                .RuleFor(l => l.Disponivel, f => f.Random.Bool());

            _usuarioFaker = new Faker<Usuario>()
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100))
                .RuleFor(u => u.Nome, f => f.Person.FullName);
        }

        [Fact]
        public void Alterar_Should_UpdateBook_When_BookIsValid()
        {
            // Arrange
            var alteraLivroDto = _alteraLivroDtoFaker.Generate();
            var livro = _livroFaker.Generate();
            livro.Id = alteraLivroDto.Id;

            _mapperMock.Setup(m => m.Map<Livro>(alteraLivroDto)).Returns(livro);
            _livroGatewayMock.Setup(g => g.ObterPorId(livro.Id)).Returns(livro);
            _mapperMock.Setup(m => m.Map<AlteraLivroDto>(It.IsAny<Livro>())).Returns(alteraLivroDto);
            _livroGatewayMock.Setup(g => g.Alterar(It.IsAny<Livro>())).Returns(livro);

            // Act
            var result = _livroService.Alterar(alteraLivroDto);

            // Assert
            result.Should().BeEquivalentTo(alteraLivroDto);
        }

        [Fact]
        public void Cadastrar_Should_ReturnLivroDto_When_BookIsValid()
        {
            // Arrange
            var cadastraLivroDto = _cadastraLivroDtoFaker.Generate();
            var livro = _livroFaker.Generate();
            var livroDto = _livroDtoFaker.Generate();

            _mapperMock.Setup(m => m.Map<Livro>(cadastraLivroDto)).Returns(livro);
            _usuarioGatewayMock.Setup(u => u.ObterPorId(livro.DoadorId)).Returns(_usuarioFaker);
;            _livroGatewayMock.Setup(g => g.Cadastrar(It.IsAny<Livro>())).Returns(livro);      
            _mapperMock.Setup(m => m.Map<LivroDto>(It.IsAny<Livro>())).Returns(livroDto);

            // Act
            var result = _livroService.Cadastrar(cadastraLivroDto);

            // Assert
            result.Should().BeEquivalentTo(livroDto);
        }

        [Fact]
        public void Deletar_Should_ThrowException_When_BookDoesNotExist()
        {
            // Arrange
            var livroId = _livroFaker.Generate().Id;

            _livroGatewayMock.Setup(g => g.ObterPorId(livroId)).Returns((Livro)null);

            // Act
            Action action = () => _livroService.Deletar(livroId);

            // Assert
            action.Should().Throw<DomainValidationException>()              
            .Which.ValidationErrors.Should().Contain("Livro não encontrado");
        }

        [Fact]
        public void Deletar_Should_DeleteBook_When_BookExists()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            var livroDto = _livroDtoFaker.Generate();

            _livroGatewayMock.Setup(g => g.ObterPorId(livro.Id)).Returns(livro);
            _livroGatewayMock.Setup(g => g.Deletar(livro.Id)).Returns(livro);
            _interesseGatewayMock.Setup(g => g.ObterPorLivro(livro.Id)).Returns(new List<Interesse>());
            _mapperMock.Setup(m => m.Map<LivroDto>(It.IsAny<Livro>())).Returns(livroDto);

            // Act
            var result = _livroService.Deletar(livro.Id);

            // Assert
            result.Should().BeEquivalentTo(livroDto);
        }

        [Fact]
        public void ObterPorId_Should_ReturnLivroDto_When_BookExists()
        {
            // Arrange
            var livro = _livroFaker.Generate();
            var livroDto = _livroDtoFaker.Generate();

            _livroGatewayMock.Setup(g => g.ObterPorId(livro.Id)).Returns(livro);
            _mapperMock.Setup(m => m.Map<LivroDto>(It.IsAny<Livro>())).Returns(livroDto);

            // Act
            var result = _livroService.ObterPorId(livro.Id);

            // Assert
            result.Should().BeEquivalentTo(livroDto);
        }

        [Fact]
        public void ObterPorDoador_Should_ReturnListOfLivroDto_When_BooksExist()
        {
            // Arrange
            var livros = _livroFaker.Generate(3);
            var livroDtos = _livroDtoFaker.Generate(3);

            _livroGatewayMock.Setup(g => g.ObterPorDoador(It.IsAny<int>())).Returns(livros);
            _mapperMock.Setup(m => m.Map<ICollection<LivroDto>>(livros)).Returns(livroDtos);

            // Act
            var result = _livroService.ObterPorDoador(_usuarioFaker.Id);

            // Assert
            result.Should().BeEquivalentTo(livroDtos);
        }

        [Fact]
        public void Disponiveis_Should_ReturnListOfLivroDto_When_BooksExist()
        {
            // Arrange
            var livros = _livroFaker.Generate(3);
            var livroDtos = _livroDtoFaker.Generate(3);

            _livroGatewayMock.Setup(g => g.Disponiveis()).Returns(livros);
            _mapperMock.Setup(m => m.Map<ICollection<LivroDto>>(livros)).Returns(livroDtos);

            // Act
            var result = _livroService.Disponiveis();

            // Assert
            result.Should().BeEquivalentTo(livroDtos);
        }

        [Fact]
        public void ObterComInteresse_Should_ReturnListOfLivroComInteressesDto_When_BooksExist()
        {
            // Arrange
            var livros = _livroFaker.Generate(3);
            var livroComInteressesDtos = new Faker<LivroComInteressesDto>().Generate(3);

            _livroGatewayMock.Setup(g => g.ObterComInteresse(It.IsAny<int>())).Returns(livros);
            _mapperMock.Setup(m => m.Map<ICollection<LivroComInteressesDto>>(livros)).Returns(livroComInteressesDtos);

            // Act
            var result = _livroService.ObterComInteresse(_usuarioFaker.Id);

            // Assert
            result.Should().BeEquivalentTo(livroComInteressesDtos);
        }
    }
}
