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
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.idUsuarios);
            builder.Property(u => u.idUsuarios).ValueGeneratedOnAdd();

            builder.HasOne(u => u.cpf)
                   .WithMany()
                   .HasForeignKey(u => u.cpfPreRegistro)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
