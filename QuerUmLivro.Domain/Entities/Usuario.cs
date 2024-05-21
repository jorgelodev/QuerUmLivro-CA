
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }       
        public Email Email { get; set; }
        public bool Desativado { get; set; }
        public ICollection<Interesse> Interesses { get; set; }
        public ICollection<Livro> Livros { get; set; }

        public Usuario(string nome, Email email)
        {
            var validationResult = new List<string>();
            if (string.IsNullOrWhiteSpace(nome) || nome.Length > 100)
                validationResult.Add("Nome é obrigatório e com máximo de 100 caracteres.");

            if (validationResult.Any())
                throw new DomainValidationException(validationResult);

            Nome = nome;
            Email = email;
        }


    }
}
