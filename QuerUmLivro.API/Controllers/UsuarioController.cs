using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuerUmLivro.Infra.Services.DTOs.Usuarios;
using QuerUmLivro.Infra.Services.Interfaces;
using QuerUmLivro.Infra.Services.ViewModels.Usuarios;

namespace QuerUmLivro.API.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastro de usuário.
        /// </summary>
        /// <param name="cadastraUsuarioViewModel">ViewModel para cadastro de usuário.</param>        
        /// <remarks>
        /// 
        /// Informe o nome e e-mail para realizar o cadastro do usuário. 
        /// 
        /// </remarks>
        /// <response code="200">Cadastro Realizado com sucesso</response>
        /// <response code="400">Cadastro não realizado, é retornado mensagem com o(s) motivo(s).</response>
        [HttpPost]
        public IActionResult Cadastrar(CadastraUsuarioViewModel cadastraUsuarioViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var usuarioCadastrado = _usuarioService.Cadastrar(_mapper.Map<CadastraUsuarioDto>(cadastraUsuarioViewModel));

            //if (!usuarioCadastrado.ValidationResult.IsValid)
            //    AdicionarErroProcessamento(usuarioCadastrado.ValidationResult);

            return Ok(usuarioCadastrado);
            
        }

        /// <summary>
        /// Alteração de usuário.
        /// </summary>
        /// <param name="alteraUsuarioViewModel">ViewModel para alterar usuário.</param>        
        /// <remarks>
        /// 
        /// Informe o nome, e-mail e id do usuário para realizar a alteração. 
        /// 
        /// </remarks>
        /// <response code="200">Alteração Realizada com sucesso</response>
        /// <response code="400">Alteração não realizada, é retornado mensagem com o(s) motivo(s).</response>
        [HttpPut]
        public IActionResult Alterar(AlteraUsuarioViewModel alteraUsuarioViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var alteraUsuarioDto = _usuarioService.Alterar(_mapper.Map<AlteraUsuarioDto>(alteraUsuarioViewModel));

            return Ok(alteraUsuarioDto);

        }

        /// <summary>
        /// Consulta um usuário por id.
        /// </summary>
        /// <param name="id">Id do Usuario.</param>        
        /// <remarks>
        /// 
        /// Envia id do Usuário para api.        
        /// 
        /// </remarks>
        /// <response code="200">Retorna sucesso com o Usuário localizado</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpGet("{id}")]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            var usuarioViewModel = _mapper.Map<UsuarioViewModel>(_usuarioService.ObterPorId(id));

            return Ok(usuarioViewModel);
        }

        /// <summary>
        /// Desativa um usuário.
        /// </summary>
        /// <param name="id">Id do usuário para desativar.</param>        
        /// <remarks>
        /// 
        /// Informe o id do usuário para desativar. 
        /// 
        /// </remarks>
        /// <response code="200">Desativação realizada com sucesso</response>
        /// <response code="400">Desativação não realizada, é retornado mensagem com o(s) motivo(s).</response>
        [HttpPut("desativar/{id}")]
        public IActionResult Desativar([FromRoute] int id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var usuarioDto = _usuarioService.Desativar(id);

            return Ok(usuarioDto);

        }
    }
}
