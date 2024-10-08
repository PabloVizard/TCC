﻿using Entities.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class PreRegistroConfiguration : IEntityTypeConfiguration<PreRegistro>
    {
        public void Configure(EntityTypeBuilder<PreRegistro> builder)
        {
            builder.ToTable("Preregistro");
            builder.HasKey(p => p.id);
            builder.Property(u => u.id).ValueGeneratedOnAdd();

        }
    }
}
