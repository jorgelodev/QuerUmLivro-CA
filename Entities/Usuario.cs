

namespace QuerUmLivro.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }       
        public string Email { get; set; }
        public ICollection<Interesse> Interesses { get; set; }
        public ICollection<Livro> Livros { get; set; }

    }
}
