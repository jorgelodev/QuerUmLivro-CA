using QuerUmLivro.Infra.Services.DTOs.Interesses;

namespace QuerUmLivro.Infra.Services.DTOs.Livros
{
    public class LivroComInteressesDto
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public int DoadorId { get; set; }
        public bool Disponivel { get; set; }        
        public ICollection<InteresseDto> Interesses { get; set; }
   
    }
}
