

using QuerUmLivro.Infra.Services.ViewModels.Interesses;

namespace QuerUmLivro.Infra.Services.ViewModels.Livros
{
    public class LivroComInteressesViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int DoadorId { get; set; }
        public bool Disponivel { get; set; }
        public ICollection<InteresseViewModel> Interesses { get; set; }
    }
}
