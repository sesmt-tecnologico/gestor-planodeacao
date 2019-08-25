using System;
using System.ComponentModel.DataAnnotations;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Dtos.v1.PlanosDeAcao
{
    public class ConcluirItensPlanosDeAcaoPost
    {
        [Required]
        public DateTime? DataRealizacao { get; set; }
    }
}