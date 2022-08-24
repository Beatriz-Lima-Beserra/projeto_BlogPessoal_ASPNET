using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Src.Contextos;
using BlogAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Src.Repositorios.Implentacoes
{

    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar ITema</para>
    /// <para>Criado por: Beatriz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 02/08/2022</para>
    /// </summary>
    public class TemaRepositorio : ITema
    {

        #region Atributos

        private readonly BlogPessoalContexto _contexto;

        #endregion

        #region Construtor

        public TemaRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos temas</para>
        /// </summary>
        /// <param name="List">Lista dos temas</param>
        /// <return>TemaModelo</return>
        public async Task<List<Tema>> PegarTodosTemasAsync()
        {
            return await _contexto.Temas.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um tema pelo id</para>
        /// </summary>
        /// <param name="id">Email do usuario</param>
        /// <return>UsuarioModelo</return>
        public async Task<Tema> PegarTemaPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("id do Tema não encontrado");

            return await _contexto.Temas.FirstOrDefaultAsync(t => t.Id == id);

            //Função Auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Temas.FirstOrDefault(t => t.Id == id);

                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo tema</para>
        /// </summary>
        /// <param name="tema">Construtor para cadastrar tema</param>
        public async Task NovoTemaAsync(Tema tema)
        {
            if (await ExisteDescricao(tema.Descricao)) throw new Exception("Descrição já existente no sistema!");

            await _contexto.Temas.AddAsync(new Tema
            {
                Descricao = tema.Descricao
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar tema</para>
        /// </summary>
        /// <param> 'name="tema">Construtor para atualizar tema</param>
        /// <exception cref="Exception">Tema já existe</exception>
        /// <exception cref="Exception">Tema já existe</exception>
        public async Task AtualizarTemaAsync(Tema tema)
        {
            if (await ExisteDescricao(tema.Descricao)) throw new Exception("Descrição já existente no sistema!");

            var auxiliar = await PegarTemaPeloIdAsync(tema.Id);
            auxiliar.Descricao = tema.Descricao;
            _contexto.Temas.Update(auxiliar);
            await _contexto.SaveChangesAsync();

        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um tema</para>
        /// </summary>
        /// <param name="id">Id do tema</param>
        public async Task DeletarTemaAsync(int id)
        {
            _contexto.Temas.Remove(await PegarTemaPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        // Funções auxiliares
        private async Task<bool> ExisteDescricao(string descricao)
        {
            var auxiliar = await _contexto.Temas.FirstOrDefaultAsync(t => t.Descricao == descricao);

            return auxiliar != null;
        }

        public Task NovaPostagemAsync(Postagem postagem)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
