using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Infra.Services.DTOs.Interesses;
using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Infra.Services.Adapters
{
    public class InteresseAdapter
    {  
        public static Interesse FromDto(ManifestarInteresseDto dto)
        {
            return new Interesse(dto.LivroId, dto.InteressadoId, dto.Justificativa, StatusInteresse.EmAnalise, DateTime.Now);
        }        
    }   
}
