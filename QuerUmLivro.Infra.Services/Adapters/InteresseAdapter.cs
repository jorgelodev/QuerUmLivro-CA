using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Infra.Services.DTOs.Interesses;
using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Infra.Services.Adapters
{
    public class InteresseAdapter
    {
        public static Interesse FromDto(InteresseDto dto)
        {
            StatusInteresseDto statusInteresseDto = dto.Status ??
                throw new DomainValidationException("Status não encontrado");

            StatusInteresse status;

            switch (dto.Status.Value)
            {
                case StatusInteresseDto.StatusInteresseEnum.APROVADO:
                    status = StatusInteresse.Aprovado;
                    break;
                case StatusInteresseDto.StatusInteresseEnum.REPROVADO:
                    status = StatusInteresse.Reprovado;
                    break;
                default:
                    status = StatusInteresse.EmAnalise;
                    break;
            }

            return new Interesse(dto.LivroId, dto.InteressadoId, dto.Justificativa, status, dto.Data);
        }

        public static Interesse FromDto(ManifestarInteresseDto dto)
        {
            return new Interesse(dto.LivroId, dto.InteressadoId, dto.Justificativa, StatusInteresse.EmAnalise, DateTime.Now);
        }

        public static InteresseDto ToDto(Interesse interesse)
        {
            StatusInteresseDto status;

            switch (interesse.Status.Value)
            {
                case StatusInteresse.StatusInteresseEnum.APROVADO:
                    status = StatusInteresseDto.Aprovado;
                    break;
                case StatusInteresse.StatusInteresseEnum.REPROVADO:
                    status = StatusInteresseDto.Reprovado;
                    break;
                default:
                    status = StatusInteresseDto.EmAnalise;
                    break;
            }

            return new InteresseDto
            {
                LivroId = interesse.LivroId,
                InteressadoId = interesse.InteressadoId,
                Justificativa = interesse.Justificativa,
                Status = status,
                Aprovado = interesse.Aprovado,
                Data = interesse.Data
            };
        }
    }

    public static class InteresseFactory
    {
        public static Interesse Create(int livroId, int interessadoId, string justificativa, StatusInteresse status, DateTime data)
        {
            return new Interesse(livroId, interessadoId, justificativa, status, data);
        }

        public static Interesse CreateFromDto(InteresseDto dto)
        {
            return InteresseAdapter.FromDto(dto);
        }
    }
}
