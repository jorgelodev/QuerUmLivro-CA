

namespace QuerUmLivro.Infra.Services.DTOs.Interesses
{
    public class InteresseDto
    {
       
        public int Id { get; set; }
        public int LivroId { get; set; }
        public int InteressadoId { get; set; }
        public string Justificativa { get; set; }
        public StatusInteresseDto Status { get; set; }       
        public bool Aprovado { get; set; }       
        public DateTime Data { get; set; }       
        
    }
}
