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
    [Route("api/Temas")]
    [Produces("application/json")]
    public class TemaControlador : ControllerBase
    {
        #region Atributos

        private readonly ITema _repositorio;

        #endregion


        #region Construtores

        public TemaControlador(ITema repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Criar novo Produto
        /// </summary>
        /// <param name="tema">Construtor para criar temas</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Tema
        /// {
        /// "Id": { 
        ///     "Id": n°
        /// }
        /// "descricao": "Descrição do tema",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        /// <response code="400">Tema não cadastrado</response>
        [HttpPost]
        public async Task<ActionResult> NovoTemaAsync([FromBody] Tema tema)
        {
            try
            {
                await _repositorio.NovoTemaAsync(tema);
                return Created($"api/Temas", tema);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Pegar Todos os Temas
        /// </summary>
        /// <param name="tema">Todos os Temas</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Todos os temas</response>
        /// <response code="404">Temas não encotrados</response>
        [HttpGet]
        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            var lista = await _repositorio.PegarTodosTemasAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary>
        /// Pegar Tema Pelo Id
        /// </summary>
        /// <param name="idTema">Id do Tema</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna tema pelo Id</response>
        /// <response code="404">Id do tema não pode ser nulo</response>
        [HttpGet("id/{idTema}")]
        public async Task<ActionResult> PegarTemaPeloIdAsync([FromRoute] int idTema)
        {
            try
            {
                return Ok(await _repositorio.PegarTemaPeloIdAsync(idTema));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar produto
        /// </summary>
        /// <param name="tema">Construtor para atualizar tema</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT /api/tema
        /// {
        /// "id": n°,
        /// "descricao": "Descricao do tema"
        /// }
        ///
        /// </remarks>
        /// <response code="200">Retorna tema atualizado</response>
        /// <response code="400">Tema nao localizado</response>
        [HttpPut]
        public async Task<ActionResult> AtualizarTemaAsync([FromBody] Tema tema)
        {
            try
            {
                await _repositorio.AtualizarTemaAsync(tema);
                return Ok(tema);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deletar Produto
        /// </summary>
        /// <param name="idTema">Deletar Tema</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// DELETE /api/Tema
        /// {
        /// "Id": "valor do Id",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna confirmação de tema deletado</response>
        /// <response code="401">Id não encontrado</response>
        [HttpDelete("id/{idTema}")]
        public async Task<ActionResult> DeletarTemaAsync([FromRoute] int idTema)
        {
            try
            {
                await _repositorio.DeletarTemaAsync(idTema);
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