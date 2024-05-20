using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Livros
{
    public class LivroDoadorObrigatorioSpec : ISpecification<Livro>
    {
        public string ErrorMessage => "Doador é obrigatório";

        public bool IsSatisfiedBy(Livro entidade)
        {
            return entidade.DoadorId > 0;
        }
    }
}
