using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Livros
{
    public class LivroIdObrigatorioSpec : ISpecification<Livro>
    {
        public string ErrorMessage => "Id do Livro não informado";

        public bool IsSatisfiedBy(Livro livro)
        {
            return livro.Id > 0;
        }
    }
}
