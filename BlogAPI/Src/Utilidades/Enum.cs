using System.Text.Json.Serialization;

namespace BlogAPI.Src.Utilidades
{

    /// <summary>
    /// <para>Resumo: Classe responsável por enumerar as possíveis condições do tipo de usuário</para>
    /// <para>Criado por: Beatriz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 23/08/2022</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        NORMAL,
        ADMINISTRADOR
    }
}
