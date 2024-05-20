using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Livros
{
    public class LivroDoadorExisteSpec : ISpecification<Livro>
    {
        private readonly IUsuarioGateway _usuarioGateway;
        public string ErrorMessage => "Doador não existe. Informe um usuário cadastrado.";

        public LivroDoadorExisteSpec(IUsuarioGateway usuarioGateway)
        {
            _usuarioGateway = usuarioGateway;
        }
        public bool IsSatisfiedBy(Livro entidade)
        {
            var usuarioEncontrado = _usuarioGateway.ObterPorId(entidade.DoadorId);

            return usuarioEncontrado != null;
        }
    }
}
