using AutoMapper;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.UseCases.Livros;
using QuerUmLivro.Infra.Services.Adapters;
using QuerUmLivro.Infra.Services.DTOs.Interesses;
using QuerUmLivro.Infra.Services.Interfaces;

namespace QuerUmLivro.Infra.Services.Services
{
    public class InteresseService : IInteresseService
    {
        private readonly IInteresseGateway _interesseGateway;
        private readonly ILivroGateway _livroGateway;
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly IMapper _mapper;
        public InteresseService(
            IInteresseGateway interesseGateway,
            IMapper mapper,
            ILivroGateway livroGateway,
            IUsuarioGateway usuarioGateway)
        {
            _interesseGateway = interesseGateway;
            _mapper = mapper;
            _livroGateway = livroGateway;
            _usuarioGateway = usuarioGateway;
        }
        public AprovarInteresseDto AprovarInteresse(AprovarInteresseDto aprovarInteresseDto)
        {            
            var interesse = _interesseGateway.ObterPorId(aprovarInteresseDto.Id) ?? 
                throw new DomainValidationException("Interesse não encontrado");
            
            interesse.Livro = _livroGateway.ObterPorId(interesse.LivroId);

            var aprovarInteresseUseCase = new AprovarInteresseUseCase(interesse,_livroGateway,_usuarioGateway);

            interesse = aprovarInteresseUseCase.AprovarInteresse();

            return _mapper.Map<AprovarInteresseDto>(_interesseGateway.Alterar(interesse));
        }

        public InteresseDto ManifestarInteresse(ManifestarInteresseDto interesseDto)
        {
            var interesse = InteresseAdapter.FromDto(interesseDto); 
            
            var livro = _livroGateway.ObterPorId(interesse.LivroId) ??
                throw new DomainValidationException("Livro não encontrado");

            interesse.Livro = livro;

            var manifestarInteresseUseCase = new ManifestarInteresseUseCase(interesse, _interesseGateway, _livroGateway, _usuarioGateway);
            
            interesse = manifestarInteresseUseCase.ManifestarInteresse();

            return _mapper.Map<InteresseDto>(_interesseGateway.Cadastrar(interesse));
        }
    }
}
