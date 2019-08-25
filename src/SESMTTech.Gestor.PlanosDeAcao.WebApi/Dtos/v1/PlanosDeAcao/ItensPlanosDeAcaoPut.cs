using System;
using System.ComponentModel.DataAnnotations;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Dtos.v1.PlanosDeAcao
{
    public class ItensPlanosDeAcaoPut
    {
        [Required]
        public DateTime? Prazo { get; set; }

        public string Descricao { get; set; }
        public string Acao { get; set; }
    }
}