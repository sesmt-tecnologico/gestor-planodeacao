using System;
using System.ComponentModel.DataAnnotations;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Dtos.v1.PlanosDeAcao
{
    public class ItensPlanosDeAcaoPost
    {
        [Required]
        public DateTime? Prazo { get; set; }

        [Required]
        public string NomeCompletoResponsavel { get; set; }

        [Required]
        [EmailAddress]
        public string EmailResponsavel { get; set; }

        public string Descricao { get; set; }
        public string Acao { get; set; }
    }
}