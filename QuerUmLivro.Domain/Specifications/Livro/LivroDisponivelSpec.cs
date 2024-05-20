using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Specifications;


namespace QuerUmLivro.Domain.Specifications.Livros
{
    internal class LivroDisponivelSpec : ISpecification<Livro>
    {
        public string ErrorMessage => "Livro não está mais disponível.";

        public bool IsSatisfiedBy(Livro entidade)
        {
            return entidade.Disponivel;
        }
    }
}
