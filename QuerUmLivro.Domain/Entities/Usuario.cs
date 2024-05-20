
using QuerUmLivro.Domain.Entities.ValueObjects;

namespace QuerUmLivro.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }       
        public Email Email { get; set; }
        public bool Desativado { get; set; }
        public ICollection<Interesse> Interesses { get; set; }
        public ICollection<Livro> Livros { get; set; }

    }
}
