using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;

namespace QuerUmLivro.Domain.Specifications.Interesses
{
    public class InteresseInteressadoExisteSpec : ISpecification<Interesse>
    {
        private readonly IUsuarioGateway _usuarioGateway;
        public string ErrorMessage => "Interessado não existe. Informe um usuário cadastrado.";
        public InteresseInteressadoExisteSpec(IUsuarioGateway usuarioGateway)
        {
            _usuarioGateway = usuarioGateway;
        }

        public bool IsSatisfiedBy(Interesse entidade)
        {
            var usuarioEncontrado = _usuarioGateway.ObterPorId(entidade.InteressadoId);

            return usuarioEncontrado != null;
        }
    }
}
