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
    public class TurmasConfiguration : IEntityTypeConfiguration<Turmas>
    {
        public void Configure(EntityTypeBuilder<Turmas> builder)
        {
            builder.ToTable("Turmas");

            builder.HasKey(u => u.id);
            builder.Property(u => u.id).ValueGeneratedOnAdd();

        }
    }
}
