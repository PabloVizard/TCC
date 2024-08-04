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
    public class FaltasConfiguration : IEntityTypeConfiguration<Faltas>
    {
        public void Configure(EntityTypeBuilder<Faltas> builder)
        {
            builder.ToTable("Faltas");

            builder.HasKey(u => u.id);
            builder.Property(u => u.id).ValueGeneratedOnAdd();

            builder.Property<int>("idAula");

            builder.HasOne<Tarefas>()
                   .WithMany()
                   .HasForeignKey("idAula")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
