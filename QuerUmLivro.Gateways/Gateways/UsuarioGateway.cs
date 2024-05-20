using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Repositories;


namespace QuerUmLivro.Gateways.Gateways
{
    public class UsuarioGateway : IUsuarioGateway
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioGateway(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Alterar(Usuario usuario)
        {
            return _usuarioRepository.Alterar(usuario);
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            return _usuarioRepository.Cadastrar(usuario);
        }

        public bool EmailJaUtilizado(Usuario usuario)
        {
            var usuarioComMesmoEmail = _usuarioRepository
                .Buscar(u => u.Email.Equals(usuario.Email) && u.Id != usuario.Id)
                .FirstOrDefault();

            return usuarioComMesmoEmail != null;
        }

        public Usuario ObterPorId(int id)
        {
            return _usuarioRepository.ObterPorId(id);
        }
    }
}
