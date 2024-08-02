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
    public class AulasConfiguration : IEntityTypeConfiguration<Aulas>
    {
        public void Configure(EntityTypeBuilder<Aulas> builder)
        {
            builder.ToTable("Aulas");

            builder.HasKey(u => u.id);
            builder.Property(u => u.id).ValueGeneratedOnAdd();

        }
    }
}
