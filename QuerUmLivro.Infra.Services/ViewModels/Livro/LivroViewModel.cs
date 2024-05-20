namespace QuerUmLivro.Infra.Services.ViewModels.Livros
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int DoadorId { get; set; }
        public bool Disponivel { get; set; }
    }
}
