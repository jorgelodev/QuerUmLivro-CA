using AutoMapper;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.UseCases.Livros;
using QuerUmLivro.Infra.Services.Adapters;
using QuerUmLivro.Infra.Services.DTOs.Usuarios;
using QuerUmLivro.Infra.Services.Interfaces;


namespace QuerUmLivro.Infra.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly IMapper _mapper;
        public UsuarioService(
            IUsuarioGateway usuarioGateway,
            IMapper mapper)
        {
            _usuarioGateway = usuarioGateway;
            _mapper = mapper;
        }

        public AlteraUsuarioDto Alterar(AlteraUsuarioDto alteraUsuarioDto)
        {
            var usuario = _usuarioGateway.ObterPorId(alteraUsuarioDto.Id) ??
                throw new DomainValidationException("Usuário não encontrado"); ;            

            usuario.Nome = alteraUsuarioDto.Nome;
            usuario.Email = new Email(alteraUsuarioDto.Email);            

            var alterarUsuarioUseCase = new AlterarUsuarioUseCase(usuario, _usuarioGateway);

            usuario = alterarUsuarioUseCase.Alterar();

            return _mapper.Map<AlteraUsuarioDto>(_usuarioGateway.Alterar(usuario));

        }

        public UsuarioDto Cadastrar(CadastraUsuarioDto usuarioDto)
        {
            var usuario = UsuarioAdapter.FromDto(usuarioDto);

            var cadastrarUsuarioUseCase = new CadastrarUsuarioUseCase(usuario, _usuarioGateway);

            usuario = cadastrarUsuarioUseCase.Cadastrar();     

            return _mapper.Map<UsuarioDto>(_usuarioGateway.Cadastrar(usuario));
        }


        public UsuarioDto Desativar(int id)
        {
            var usuario = _usuarioGateway.ObterPorId(id) ??
                throw new DomainValidationException("Usuário não encontrado");

            var desativarUsuarioUseCase = new DesativarUsuarioUseCase(usuario);

            usuario = desativarUsuarioUseCase.Desativar();            

            return _mapper.Map<UsuarioDto>(_usuarioGateway.Alterar(usuario));
        }

        public UsuarioDto ObterPorId(int id)
        {
            var usuario = _usuarioGateway.ObterPorId(id);

            return _mapper.Map<UsuarioDto>(usuario);
        }
    }
}
