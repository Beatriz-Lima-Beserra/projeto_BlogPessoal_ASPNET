﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

/// <summary>
/// <para>Resumo: Classe responsavel por representar tb_usuarios no banco.
/// <para >
/// <para>Criado por: Beatriz</para>
/// <para>Versão: 1.0</para>
/// <para>Data: 02/08/2022</para>
/// </summary>

namespace BlogAPI.Src.Modelos
{

    [Table("tb_usuarios")]
    public class Usuario
    {
        #region Atributos

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Foto { get; set; }

        [JsonIgnore, InverseProperty("Criador")]
        public List<Postagem> MinhasPostagens { get; set; }

        #endregion

    }
}
