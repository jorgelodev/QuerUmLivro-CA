namespace QuerUmLivro.Infra.Services.DTOs.Livros
{
    public class LivroDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int DoadorId { get; set; }
        public bool Disponivel { get; set; }

    }
}
