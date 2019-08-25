using System.ComponentModel.DataAnnotations;

namespace SESMTTech.Gestor.PlanosDeAcao.Infrastructure.EntityFrameworkCore
{
    public class PlanosDeAcaoSqlConfiguration
    {
        [Required]
        public string ConnectionString { get; set; }

        public bool EnableMigrations { get; set; } = false;
    }
}