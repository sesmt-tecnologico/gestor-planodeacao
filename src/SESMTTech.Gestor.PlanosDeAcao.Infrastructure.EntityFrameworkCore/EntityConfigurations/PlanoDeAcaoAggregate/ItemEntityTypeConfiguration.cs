using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using System;

namespace SESMTTech.Gestor.PlanosDeAcao.Infrastructure.EntityFrameworkCore.EntityConfigurations.PlanoDeAcaoAggregate
{
    internal class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Itens");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.CreationDate).IsRequired();
            builder.Property(b => b.CreationUser).HasMaxLength(100).IsRequired(false);
            builder.Property(b => b.Numero).IsRequired();
            builder.Property(b => b.Codigo).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Descricao).IsRequired(false);
            builder.Property(b => b.Acao).IsRequired(false);
            builder.Property(b => b.Prazo).IsRequired();
            builder.Property(b => b.Status).IsRequired();
            builder.Property(b => b.DataRealizacao).IsRequired(false);
            builder.Property<Guid>("PlanoDeAcaoId").IsRequired();

            builder.HasIndex(s => s.Codigo).IsUnique();

            var navigation = builder.Metadata.FindNavigation(nameof(Item.Responsaveis));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}