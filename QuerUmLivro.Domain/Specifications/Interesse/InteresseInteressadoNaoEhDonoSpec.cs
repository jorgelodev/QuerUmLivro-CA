using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Entities;

namespace QuerUmLivro.Domain.Specifications.Interesses
{
    public class InteresseInteressadoNaoEhDonoSpec : ISpecification<Interesse>
    {
        public string ErrorMessage => "Dono do livro não pode manifestar interesse no próprio livro.";
        public bool IsSatisfiedBy(Interesse interesse)
        {
            return interesse.Livro.DoadorId != interesse.InteressadoId;
        }
    }
}
