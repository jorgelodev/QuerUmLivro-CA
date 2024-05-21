using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Infra.Services.DTOs.Usuarios;

namespace QuerUmLivro.Infra.Services.Adapters
{
    public class UsuarioAdapter
    {  
        public static Usuario FromDto(CadastraUsuarioDto dto)
        {
            return new Usuario(dto.Nome, new Email(dto.Email));
        }
       
    }   
}
