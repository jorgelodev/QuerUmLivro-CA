using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Entities;

namespace QuerUmLivro.Domain.Specifications.Livros
{
    public class LivroDoadorEhDoadorDoLivroSpec : ISpecification<Livro>
    {
        public string ErrorMessage => "Doador informado não pode aprovar pois não é doador do Livro.";
        
        private readonly Usuario _doador;
        public LivroDoadorEhDoadorDoLivroSpec(Usuario doador)
        {
            _doador = doador;
        }

        public bool IsSatisfiedBy(Livro livro)
        {
            return livro.DoadorId == _doador.Id;
        }
    }
}
