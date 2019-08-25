using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using System;

namespace SESMTTech.Gestor.PlanosDeAcao.Infrastructure.EntityFrameworkCore.EntityConfigurations.PlanoDeAcaoAggregate
{
    internal class ResponsavelEntityTypeConfiguration : IEntityTypeConfiguration<Responsavel>
    {
        public void Configure(EntityTypeBuilder<Responsavel> builder)
        {
            builder.ToTable("Responsaveis");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.CreationDate).IsRequired();
            builder.Property(b => b.CreationUser).HasMaxLength(100).IsRequired(false);
            builder.Property(b => b.NomeCompleto).HasMaxLength(200).IsRequired();
            builder.Property(b => b.Email).HasMaxLength(100).IsRequired(false);
            builder.Property<Guid>("ItemId").IsRequired();
        }
    }
}