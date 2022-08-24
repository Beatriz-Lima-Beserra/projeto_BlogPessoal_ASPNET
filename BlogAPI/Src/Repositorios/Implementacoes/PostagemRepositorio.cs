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
    /// <para>Resumo: Classe responsavel por implementar IPostagem</para>
    /// <para>Criado por: Beatriz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 23/08/2022</para>
    /// </summary>
    public class PostagemRepositorio : IPostagem
    {

        #region Atributos

        private readonly BlogPessoalContexto _contexto;

        #endregion

        #region Construtor

        public PostagemRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todas postagens</para>
        /// </summary>
        /// <param name="List">Lista das postagens</param>
        /// <return>PostagemModelo</return>
        public async Task<List<Postagem>> PegarTodosPostagensAsync()
        {
            return await _contexto.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar uma postagem pelo id</para>
        /// </summary>
        /// <param name="id">Postagem do usuario</param>
        /// <return>PostagemModelo</return>
        public async Task<Postagem> PegarPostagemPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("id do Tema não encontrado");

            return await _contexto.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .FirstOrDefaultAsync(p => p.Id == id);

            //Função Auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Postagens.FirstOrDefault(t => t.Id == id);

                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova postagem</para>
        /// </summary>
        /// <param name="postagem">Construtor para cadastrar postagem</param>
        public async Task NovoPostagemAsync(Postagem postagem)
        {
            if (!ExisteIdCriador(postagem.Criador.Id)) throw new Exception("id do Usuario não encontrado");

            if (!ExisteIdTema(postagem.Tema.Id)) throw new Exception("id do Tema não encontrado");

            await _contexto.Postagens.AddAsync(new Postagem
            {
                Titulo = postagem.Titulo,
                Descricao = postagem.Descricao,
                Foto = postagem.Foto,
                Criador = _contexto.Usuarios.FirstOrDefault(t => t.Id == postagem.Criador.Id),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Id == postagem.Tema.Id)
            });
            await _contexto.SaveChangesAsync();

            //Função Auxiliar
            bool ExisteIdCriador(int id)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(t => t.Id == id);

                return auxiliar != null;
            }

            bool ExisteIdTema(int id)
            {
                var auxiliar = _contexto.Temas.FirstOrDefault(t => t.Id == id);

                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar postagem</para>
        /// </summary>
        /// <param> 'name="postagem">Construtor para atualizar postagem</param>
        /// <exception cref="Exception">Postagem já existe</exception>
        /// <exception cref="Exception">Postagem já existe</exception>
        public async Task AtualizarPostagemAsync(Postagem postagem)
        {
            if (!ExisteIdTema(postagem.Tema.Id)) throw new Exception("id do Tema não encontrado");

            var auxiliar = await PegarPostagemPeloIdAsync(postagem.Id);
            auxiliar.Titulo = postagem.Titulo;
            auxiliar.Descricao = postagem.Descricao;
            auxiliar.Foto = postagem.Foto;
            auxiliar.Tema = _contexto.Temas.FirstOrDefault(t => t.Id == postagem.Tema.Id);
            _contexto.Postagens.Update(auxiliar);
            await _contexto.SaveChangesAsync();

            //Função Auxiliar
            bool ExisteIdTema(int id)
            {
                var auxiliar = _contexto.Temas.FirstOrDefault(t => t.Id == id);

                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar postagem</para>
        /// </summary>
        /// <param name="id">Id da postagem</param>
        public async Task DeletarPostagemAsync(int id)
        {
            _contexto.Postagens.Remove(await PegarPostagemPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        // Funções auxiliares
        public Task<List<Postagem>> PegarTodasPostagensAsync()
        {
            throw new NotImplementedException();
        }

        public Task NovaPostagemAsync(Postagem postagem)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
