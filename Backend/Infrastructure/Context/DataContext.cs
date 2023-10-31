using Entities.Entity;
using Infrastructure.Configurations;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public  class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuarios> usuarios { get; set; }

        public DbSet<PreRegistro> preregistro { get; set; }

        public DbSet<Turmas> turmas { get; set; }
        public DbSet<Orientacoes> orientacoes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Settings.connectionDev, new MySqlServerVersion(new Version()));
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrientacoesConfiguration());
            modelBuilder.ApplyConfiguration(new PreRegistroConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new TurmasConfiguration());
        }
    }
    
}
