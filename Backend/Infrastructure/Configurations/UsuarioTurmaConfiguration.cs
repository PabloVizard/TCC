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
    public class UsuarioTurmaConfiguration : IEntityTypeConfiguration<UsuarioTurma>
    {
        public void Configure(EntityTypeBuilder<UsuarioTurma> builder)
        {
            builder.ToTable("UsuarioTurma");

            builder.HasKey(u => u.id);
            builder.Property(u => u.id).ValueGeneratedOnAdd();

        }
    }
}
