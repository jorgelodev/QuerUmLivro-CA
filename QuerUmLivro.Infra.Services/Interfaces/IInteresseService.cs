

using QuerUmLivro.Infra.Services.DTOs.Interesses;

namespace QuerUmLivro.Infra.Services.Interfaces
{
    public interface IInteresseService
    {
        InteresseDto ManifestarInteresse(ManifestarInteresseDto interesseDto);
        AprovarInteresseDto AprovarInteresse(AprovarInteresseDto aprovarInteresseDto);

    }
}
