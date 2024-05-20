using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Livros
{
    public class LivroDoadorNaoPodeSerAlteradoSpec : ISpecification<Livro>
    {
        public string ErrorMessage => "Doador não pode ser alterado";
        private readonly ILivroGateway _livroGateway;
        public LivroDoadorNaoPodeSerAlteradoSpec(ILivroGateway livroGateway)
        {
            _livroGateway = livroGateway;
        }
        public bool IsSatisfiedBy(Livro entidade)
        {
            var livro = _livroGateway.ObterPorId(entidade.Id);

            if (livro == null)
                return false;

            return livro.DoadorId == entidade.DoadorId;
        }
    }
}
