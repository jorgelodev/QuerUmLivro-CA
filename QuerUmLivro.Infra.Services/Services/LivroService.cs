using AutoMapper;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.UseCases.Livros;
using QuerUmLivro.Infra.Services.DTOs.Livros;
using QuerUmLivro.Infra.Services.Interfaces;

namespace QuerUmLivro.Infra.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroGateway _livroGateway;
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly IInteresseGateway _interesseGateway;
        private readonly IMapper _mapper;
        public LivroService(
            ILivroGateway livroGateway,
            IMapper mapper,
            IUsuarioGateway usuarioGateway,
            IInteresseGateway interesseGateway)
        {
            _livroGateway = livroGateway;
            _mapper = mapper;
            _usuarioGateway = usuarioGateway;
            _interesseGateway = interesseGateway;
        }

        public AlteraLivroDto Alterar(AlteraLivroDto alteraLivroDto)
        {
            var livroModificado = _mapper.Map<Livro>(alteraLivroDto);

            var livro = _livroGateway.ObterPorId(livroModificado.Id);

            if (livro == null)
                throw new DomainValidationException("Livro não encontrado");

            livro.Nome = livroModificado.Nome;            
            
            var alterarLivroUseCase = new AlterarLivroUseCase(livro, _livroGateway);

            alterarLivroUseCase.Alterar();  

            return _mapper.Map<AlteraLivroDto>(_livroGateway.Alterar(livro));
         
        }

        public LivroDto Cadastrar(CadastraLivroDto livroDto)
        {
            var livro = _mapper.Map<Livro>(livroDto);

            var cadastrarLivroUseCase = new CadastrarLivroUseCase(livro, _usuarioGateway);

            livro = cadastrarLivroUseCase.Cadastrar();

            return _mapper.Map<LivroDto>(_livroGateway.Cadastrar(livro));
        }

        public LivroDto Deletar(int id)
        {
            var livro = _livroGateway.ObterPorId(id);

            if(livro == null)            
                throw new DomainValidationException("Livro não encontrado");                
            

            var deletarLivroUseCase = new DeletarLivroUseCase(livro, _interesseGateway);

            deletarLivroUseCase.Deletar();           

            return _mapper.Map<LivroDto>(_livroGateway.Deletar(id));
        }

        public LivroDto ObterPorId(int id)
        {
            var livro = _livroGateway.ObterPorId(id);

            return _mapper.Map<LivroDto>(livro);
        }

        public ICollection<LivroDto> ObterPorDoador(int idUsuario)
        {
            var livros = _livroGateway.ObterPorDoador(idUsuario);

            return _mapper.Map<ICollection<LivroDto>>(livros);
        }

        public ICollection<LivroDto> Disponiveis()
        {
            var livros = _livroGateway.Disponiveis();

            return _mapper.Map<ICollection<LivroDto>>(livros);
        }

        public ICollection<LivroComInteressesDto> ObterComInteresse(int idDoador)
        {
            var livros = _livroGateway.ObterComInteresse(idDoador);

            return _mapper.Map<ICollection<LivroComInteressesDto>>(livros);
        }
        
    }
}
