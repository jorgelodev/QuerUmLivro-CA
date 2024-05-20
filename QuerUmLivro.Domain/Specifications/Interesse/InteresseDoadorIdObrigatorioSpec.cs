
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Interesses
{
    public class InteresseDoadorIdObrigatorioSpec : ISpecification<Interesse>
    {
        public string ErrorMessage => "Id do doador deve ser informado.";
        public bool IsSatisfiedBy(Interesse interesse)
        {
            return interesse.Livro.DoadorId > 0;
        }
    }
}
