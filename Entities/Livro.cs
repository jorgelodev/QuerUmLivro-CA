namespace QuerUmLivro.Domain.Entities
{
    public class Livro : EntityBase
    {        
        public string Nome { get; set; }
        public int DoadorId { get; set; }
        public Usuario Doador { get; set; }
        public bool Disponivel { get; set; }

        public ICollection<Interesse> Interesses { get; set; }

    }
}
