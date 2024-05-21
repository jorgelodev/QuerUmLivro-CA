using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Usuarios
{
    public class UsuarioEmailUnicoSpec : ISpecification<Usuario>
    {
        private readonly IUsuarioGateway _usuarioGateway;
        public string ErrorMessage => "E-mail já está sendo utilizado.";

        public UsuarioEmailUnicoSpec(IUsuarioGateway usuarioGateway)
        {
            _usuarioGateway = usuarioGateway;
        }

        public bool IsSatisfiedBy(Usuario usuario)
        {
            return !_usuarioGateway.EmailJaUtilizado(usuario);
        }
    }
}
