using System.Threading.Tasks;
using BlogAPI.Src.Modelos;
namespace BlogAPI.Src.Servicos
{
/// <summary>
/// <para>Resumo: Interface Responsavel por representar ações de
///autenticação</para>
/// <para>Criado por: Beatriz</para>
/// <para>Versão: 1.0</para>
/// <para>Data: 23/08/2022</para>
/// </summary>
public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(Usuario usuario);
        string GerarToken(Usuario usuario);
    }
}