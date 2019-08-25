using System.ComponentModel.DataAnnotations;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Queries
{
    public class PlanosDeAcaoSqlConfiguration
    {
        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string DatabaseName { get; set; }
    }
}