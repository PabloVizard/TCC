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
    public class TarefaAlunoConfiguration : IEntityTypeConfiguration<TarefaAluno>
    {
        public void Configure(EntityTypeBuilder<TarefaAluno> builder)
        {
            builder.ToTable("TarefaAluno");

            builder.HasKey(u => u.id);
            builder.Property(u => u.id).ValueGeneratedOnAdd();

            builder.Property<int>("idTarefa");

            builder.HasOne<Tarefas>()
                   .WithMany()
                   .HasForeignKey("idTarefa")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
