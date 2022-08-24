using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Src.Modelos;
using BlogAPI.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Postagens")]
    [Produces("application/json")]
    public class PostagemControlador : ControllerBase
    {
        #region Atributos

        private readonly IPostagem _repositorio;

        #endregion


        #region Construtores

        public PostagemControlador(IPostagem repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Criar nova Postagem 
        /// </summary>
        /// <param name="postagem">Contrutor para criar postagem</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Postagem
        /// {
        /// "titulo": "Aprendendo sobre crud",
        /// "descricao": "Explicação sobre crud de uma api",
        /// "foto": "URLFOTO",
        /// "Criador": { 
        ///     "Id": n°
        /// "Tema": { 
        ///     "Id": n°
        /// }
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="401">E-mail ja cadastrado</response>
        [HttpPost]
        public async Task<ActionResult> NovoPostagemAsync([FromBody] Postagem postagem)
        {
            try
            {
                await _repositorio.NovoPostagemAsync(postagem);
                return Created($"api/Postagens", postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Pegar Todas Postagens
        /// </summary>
        /// <param name="postagem">Postagem do usuario</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna as postagens</response>
        /// <response code="404">Postagem não existente</response>
        [HttpGet]
        public async Task<ActionResult> PegarTodasPostagensAsync()
        {
            var lista = await _repositorio.PegarTodosPostagensAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary>
        /// Pegar Postagens Pelo Id
        /// </summary>
        /// <param name="idPostagem">Id da Postagem</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna postagem pelo Id</response>
        /// <response code="404">Id da postagem não pode ser nulo</response>
        [HttpGet("id/{idPostagem}")]
        public async Task<ActionResult> PegarPostagemPeloIdAsync([FromRoute] int idPostagem)
        {
            try
            {
                return Ok(await _repositorio.PegarPostagemPeloIdAsync(idPostagem));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar Postagem
        /// </summary>
        /// <param name="postagem">Construtor para atualizar postagem</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT /api/Postagem
        /// {
        /// "titulo": "Aprendendo sobre crud",
        /// "descricao": "Explicação sobre crud de uma api",
        /// "foto": "URLFOTO",
        /// "Criador": { 
        ///     "Id": n°
        /// "Tema": { 
        ///     "Id": n°
        /// }
        /// }
        ///
        /// </remarks>
        /// <response code="200">Retorna tema atualizado</response>
        /// <response code="400">Tema nao localizado</response>
        [HttpPut]
        public async Task<ActionResult> AtualizarPostagemAsync([FromBody] Postagem postagem)
        {
            try
            {
                await _repositorio.AtualizarPostagemAsync(postagem);
                return Ok(postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deletar Postagem
        /// </summary>
        /// <param name="idPostagem">Deletar Postagem</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// DELETE /api/Postagem
        /// {
        /// "Id": "valor do Id",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna confirmação de postagem deletada</response>
        /// <response code="401">Id não encontrado</response>
        [HttpDelete("id/{idPostagem}")]
        public async Task<ActionResult> DeletarPostagemAsync([FromRoute] int idPostagem)
        {
            try
            {
                await _repositorio.DeletarPostagemAsync(idPostagem);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        #endregion
    }
}