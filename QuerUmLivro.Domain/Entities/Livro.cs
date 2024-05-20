using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Domain.Entities
{
    public class Livro : EntityBase
    {        
        public string Nome { get; set; }
        public int DoadorId { get; set; }
        public Usuario Doador { get; set; }
        public bool Disponivel { get; set; }

        public ICollection<Interesse> Interesses { get; set; }

        public Livro(string nome)
        {
            Nome = nome;

            var validationResult = new List<string>();
            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length > 100)
                validationResult.Add("Nome é obrigatório e com máximo de 100 caracteres.");

            if (validationResult.Any())
                throw new DomainValidationException(validationResult);
        }      

    }
}
