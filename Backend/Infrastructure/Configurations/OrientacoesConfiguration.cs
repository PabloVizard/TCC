using Entities.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class OrientacoesConfiguration : IEntityTypeConfiguration<Orientacoes>
    {
        public void Configure(EntityTypeBuilder<Orientacoes> builder)
        {
            builder.ToTable("Orientacoes");
            builder.HasKey(p => p.id);
            builder.Property(u => u.id).ValueGeneratedOnAdd();
        }
    }
}
