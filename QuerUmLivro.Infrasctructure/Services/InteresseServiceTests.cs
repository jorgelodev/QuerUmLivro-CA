using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Infra.Services.Adapters;
using QuerUmLivro.Infra.Services.DTOs.Interesses;
using QuerUmLivro.Infra.Services.Services;

namespace QuerUmLivro.Test.Infra.Services
{
    public class InteresseServiceTests
    {
        private readonly Mock<IInteresseGateway> _interesseGatewayMock;
        private readonly Mock<ILivroGateway> _livroGatewayMock;
        private readonly Mock<IUsuarioGateway> _usuarioGatewayMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Faker<Interesse> _interesseFaker;
        private readonly Livro _livroFaker;
        private readonly Usuario _doadorFaker;
        private readonly Usuario _interessadoFaker;

        public InteresseServiceTests()
        {
            _interesseGatewayMock = new Mock<IInteresseGateway>();
            _livroGatewayMock = new Mock<ILivroGateway>();
            _usuarioGatewayMock = new Mock<IUsuarioGateway>();
            _mapperMock = new Mock<IMapper>();

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
                    Livro = _livroFaker,
                    Interessado = _interessadoFaker
                });
        }

        [Fact]
        public void AprovarInteresse_Should_ThrowException_When_InteresseNotFound()
        {
            // Arrange
            var aprovarInteresseDto = new AprovarInteresseDto { Id = 1 };

            _interesseGatewayMock.Setup(g => g.ObterPorId(aprovarInteresseDto.Id)).Returns((Interesse)null);

            var service = new InteresseService(_interesseGatewayMock.Object, _mapperMock.Object, _livroGatewayMock.Object, _usuarioGatewayMock.Object);

            // Act
            Action action = () => service.AprovarInteresse(aprovarInteresseDto);

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Interesse não encontrado");
        }

        [Fact]
        public void AprovarInteresse_Should_ReturnAprovarInteresseDto_When_Successful()
        {
            // Arrange
            var aprovarInteresseDto = new AprovarInteresseDto { Id = 1 };
            var interesse = _interesseFaker.Generate();

            _interesseGatewayMock.Setup(g => g.ObterPorId(aprovarInteresseDto.Id)).Returns(interesse);
            _livroGatewayMock.Setup(g => g.ObterPorId(interesse.LivroId)).Returns(interesse.Livro);
            _interesseGatewayMock.Setup(g => g.Alterar(interesse)).Returns(interesse);
            _mapperMock.Setup(m => m.Map<AprovarInteresseDto>(It.IsAny<Interesse>())).Returns(aprovarInteresseDto);
            _usuarioGatewayMock.Setup(g => g.ObterPorId(interesse.InteressadoId)).Returns(interesse.Interessado);

            var service = new InteresseService(_interesseGatewayMock.Object, _mapperMock.Object, _livroGatewayMock.Object, _usuarioGatewayMock.Object);

            // Act
            var result = service.AprovarInteresse(aprovarInteresseDto);

            // Assert
            result.Should().BeEquivalentTo(aprovarInteresseDto);
        }

        [Fact]
        public void ManifestarInteresse_Should_ThrowException_When_LivroNotFound()
        {
            // Arrange
            var interesseDto = new ManifestarInteresseDto { LivroId = 1, InteressadoId = 1, Justificativa = "Gostaria de ter o livro" };
            var interesse = InteresseAdapter.FromDto(interesseDto);
            var interesseCadastrado = _interesseFaker.Generate();
            interesse.Interessado = _interessadoFaker;

            _livroGatewayMock.Setup(g => g.ObterPorId(interesseDto.LivroId)).Returns((Livro)null);
            _interesseGatewayMock.Setup(g => g.Cadastrar(interesse)).Returns(interesseCadastrado);            
            _usuarioGatewayMock.Setup(g => g.ObterPorId(interesse.InteressadoId)).Returns(interesse.Interessado);

            var service = new InteresseService(_interesseGatewayMock.Object, _mapperMock.Object, _livroGatewayMock.Object, _usuarioGatewayMock.Object);

            // Act
            Action action = () => service.ManifestarInteresse(interesseDto);

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Livro não encontrado");
        }

        [Fact]
        public void ManifestarInteresse_Should_ReturnInteresseDto_When_Successful()
        {
            // Arrange
            var interesseDto = new ManifestarInteresseDto { LivroId = 1, InteressadoId = 1, Justificativa = "Gostaria de ter o livro" };
            var interesse = InteresseAdapter.FromDto(interesseDto);
            var interesseCadastrado = _interesseFaker.Generate();
            interesse.Interessado = _interessadoFaker;

            _livroGatewayMock.Setup(g => g.ObterPorId(interesseDto.LivroId)).Returns(_livroFaker);
            _interesseGatewayMock.Setup(g => g.Cadastrar(interesse)).Returns(interesseCadastrado);
            _mapperMock.Setup(m => m.Map<InteresseDto>(It.IsAny<Interesse>())).Returns(new InteresseDto { Id = interesseCadastrado.Id });
            _usuarioGatewayMock.Setup(g => g.ObterPorId(interesse.InteressadoId)).Returns(interesse.Interessado);

            var service = new InteresseService(_interesseGatewayMock.Object, _mapperMock.Object, _livroGatewayMock.Object, _usuarioGatewayMock.Object);

            // Act
            var result = service.ManifestarInteresse(interesseDto);

            // Assert
            result.Id.Should().Be(interesseCadastrado.Id);
        }
    }
}
