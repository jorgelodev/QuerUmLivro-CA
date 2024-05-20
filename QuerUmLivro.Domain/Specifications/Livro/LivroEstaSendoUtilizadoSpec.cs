using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Livros
{
    public class LivroEstaSendoUtilizadoSpec : ISpecification<Livro>
    {
        public string ErrorMessage => "Livro possui interesse, não pode ser excluído";
        private readonly IInteresseGateway _interesseGateway;
        public LivroEstaSendoUtilizadoSpec(IInteresseGateway interesseGateway)
        {
            _interesseGateway = interesseGateway;
        }
        public bool IsSatisfiedBy(Livro entidade)
        {
            var interesse = _interesseGateway
                .ObterPorLivro(entidade.Id)
                .FirstOrDefault();

            return interesse == null;


        }
    }
}
