using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Infra.Services;
using QuerUmLivro.Infra.Services.Adapters;
using QuerUmLivro.Infra.Services.DTOs.Usuarios;

namespace QuerUmLivro.Test.Infra.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioGateway> _usuarioGatewayMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Faker<Usuario> _usuarioFaker;

        public UsuarioServiceTests()
        {
            _usuarioGatewayMock = new Mock<IUsuarioGateway>();
            _mapperMock = new Mock<IMapper>();

            _usuarioFaker = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario(f.Person.UserName, new Email(f.Person.Email)))
                .RuleFor(u => u.Id, f => f.Random.Int(1, 100));
        }

        [Fact]
        public void Alterar_Should_ThrowException_When_UsuarioNotFound()
        {
            // Arrange
            var alteraUsuarioDto = new AlteraUsuarioDto { Id = 1, Nome = "Nome", Email = "jorge@gmail.com" };
            var usuario = _mapperMock.Object.Map<Usuario>(alteraUsuarioDto);

            _usuarioGatewayMock.Setup(g => g.ObterPorId(alteraUsuarioDto.Id)).Returns((Usuario)null);

            var service = new UsuarioService(_usuarioGatewayMock.Object, _mapperMock.Object);

            // Act
            Action action = () => service.Alterar(alteraUsuarioDto);

            // Assert
            action.Should().Throw<DomainValidationException>()
                 .Which.ValidationErrors.Should().Contain("Usuário não encontrado");                
        }

        [Fact]
        public void Alterar_Should_ReturnAlteraUsuarioDto_When_Successful()
        {
            // Arrange
            var alteraUsuarioDto = new AlteraUsuarioDto { Id = 1, Nome = "Nome Alterado", Email = "jorge@gmail.com" };
            var usuario = _usuarioFaker.Generate();
            usuario.Id = alteraUsuarioDto.Id;

            _usuarioGatewayMock.Setup(g => g.ObterPorId(alteraUsuarioDto.Id)).Returns(usuario);
            _usuarioGatewayMock.Setup(g => g.Alterar(It.IsAny<Usuario>())).Returns(usuario);
            _mapperMock.Setup(m => m.Map<AlteraUsuarioDto>(It.IsAny<Usuario>())).Returns(alteraUsuarioDto);

            var service = new UsuarioService(_usuarioGatewayMock.Object, _mapperMock.Object);

            // Act
            var result = service.Alterar(alteraUsuarioDto);

            // Assert
            result.Should().BeEquivalentTo(alteraUsuarioDto);
        }

        [Fact]
        public void Cadastrar_Should_ReturnUsuarioDto_When_Successful()
        {
            // Arrange
            var cadastraUsuarioDto = new CadastraUsuarioDto { Nome = "Jorge Oliveira", Email = "jorge@gmail.com" };
            var usuario = UsuarioAdapter.FromDto(cadastraUsuarioDto);
            var usuarioDto = new UsuarioDto { Id = 1, Nome = "Jorge Oliveira", Email = "jorge@gmail.com" };

            _usuarioGatewayMock.Setup(g => g.Cadastrar(It.IsAny<Usuario>())).Returns(usuario);
            _mapperMock.Setup(m => m.Map<UsuarioDto>(It.IsAny<Usuario>())).Returns(usuarioDto);

            var service = new UsuarioService(_usuarioGatewayMock.Object, _mapperMock.Object);

            // Act
            var result = service.Cadastrar(cadastraUsuarioDto);

            // Assert
            result.Should().BeEquivalentTo(usuarioDto);
        }

        [Fact]
        public void Desativar_Should_ThrowException_When_UsuarioNotFound()
        {
            // Arrange
            var usuarioId = 1;
            _usuarioGatewayMock.Setup(g => g.ObterPorId(usuarioId)).Returns((Usuario)null);

            var service = new UsuarioService(_usuarioGatewayMock.Object, _mapperMock.Object);

            // Act
            Action action = () => service.Desativar(usuarioId);

            // Assert
            action.Should().Throw<DomainValidationException>()
                .Which.ValidationErrors.Should().Contain("Usuário não encontrado");
                
        }

        [Fact]
        public void Desativar_Should_ReturnUsuarioDto_When_Successful()
        {
            // Arrange
            var usuario = _usuarioFaker.Generate();
            var usuarioDto = new UsuarioDto { Id = usuario.Id, Nome = usuario.Nome};

            _usuarioGatewayMock.Setup(g => g.ObterPorId(usuario.Id)).Returns(usuario);
            _usuarioGatewayMock.Setup(g => g.Alterar(It.IsAny<Usuario>())).Returns(usuario);
            _mapperMock.Setup(m => m.Map<UsuarioDto>(It.IsAny<Usuario>())).Returns(usuarioDto);

            var service = new UsuarioService(_usuarioGatewayMock.Object, _mapperMock.Object);

            // Act
            var result = service.Desativar(usuario.Id);

            // Assert
            result.Should().BeEquivalentTo(usuarioDto);
        }

        [Fact]
        public void ObterPorId_Should_ReturnUsuarioDto_When_Successful()
        {
            // Arrange
            var usuario = _usuarioFaker.Generate();
            var usuarioDto = new UsuarioDto { Id = usuario.Id, Nome = usuario.Nome };

            _usuarioGatewayMock.Setup(g => g.ObterPorId(usuario.Id)).Returns(usuario);
            _mapperMock.Setup(m => m.Map<UsuarioDto>(It.IsAny<Usuario>())).Returns(usuarioDto);

            var service = new UsuarioService(_usuarioGatewayMock.Object, _mapperMock.Object);

            // Act
            var result = service.ObterPorId(usuario.Id);

            // Assert
            result.Should().BeEquivalentTo(usuarioDto);
        }
    }
}
