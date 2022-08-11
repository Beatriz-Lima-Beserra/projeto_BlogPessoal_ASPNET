using BlogAPI.Src.Contextos;
using BlogAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


namespace BlogTeste.Contextos
{

    /// <summary>
    /// <para>Resumo: Classe para texte unitario de contexto de usuario</para>
    /// <para>Criado por: Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 03/08/2022</para>
    /// </summary>

    [TestClass]
    public class UsuarioContextoTeste
    {
        #region Atributos

        private BlogPessoalContexto _contexto;

        #endregion

        #region Métodos

        [TestMethod]
        public void InserirNovoUsuarioRetornaUsuarioInserido()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT1")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema

            _contexto.Usuarios.Add(new Usuario
            {

                Nome = "Beatriz Lima",
                Email = "beatrizlima@email.com",
                Senha = "123456",
                Foto = "URLFOTOBIA",

            });
            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquiso pelo e-mail do usuario adicionado
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Email == "beatrizlima@email.com");

            // ENTÃO - Então deve retornar resultado nao nulo
            Assert.IsNotNull(resultado);

        }

        public void LerListaDeUsuariosRetornaTresUsuarios()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT2")
                .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono 3 usuarios novos no sistema

            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Leticia Oliveira",
                Email = "letoliveira@email.com",
                Senha = "134652",
                Foto = "URLFOTOLETICIAOLIVEIRA",
            });

            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Maria Aurea",
                Email = "Aurea@email.com",
                Senha = "134652",
                Foto = "URLFOTOMARIAAUREA",
            });

            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Jessica Lima",
                Email = "jehlima@email.com",
                Senha = "134652",
                Foto = "URLFOTOJESSICALIMA",
            });

            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquisar por todos os usuarios

            var resultado = _contexto.Usuarios.ToList();

            // ENTÃO - Então deve retornar uma lista com 3 usuarios

            Assert.AreEqual(3, resultado.Count); 
        }

        [TestMethod]
        public void AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT3")
                .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Jose Ribamar",
                Email = "jribamar@email.com",
                Senha = "134652",
                Foto = "URLFOTOJOSERIBAMAR",
            });

            _contexto.SaveChanges();
            // E - E altero seu nome para Zenildo Rosa
            var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Email == "jribamar@email.com");
            auxiliar.Nome = "Jose Ribamar";

            _contexto.Usuarios.Update(auxiliar);

            _contexto.SaveChanges();

            // QUANDO - Quando pesquiso pelo nome Zenildo Rosa
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Nome == "Jose Ribamar");

            // ENTÃO - Então deve retornar resultado nao nulo
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void DeletaUsuarioRetornaUsuarioInesistente()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT4")
                .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Rafaela Oliveira",
                Email = "rafaoliveira@email.com",
                Senha = "134652",
                Foto = "URLFOTORAFAELAOLIVEIRA",
            });

            _contexto.SaveChanges();
            // QUANDO - Quando deleto o usuario inserido
            var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Email == "rafaoliveira@email.com");

            _contexto.Usuarios.Remove(auxiliar);

            _contexto.SaveChanges();

            // E - E pesquiso pelo nome Neusa Boaz
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Nome == "Rafaela Oliveira");

            // ENTÃO - Então deve retornar resultado nulo
            Assert.IsNull(resultado);
        }

        #endregion

    }
}