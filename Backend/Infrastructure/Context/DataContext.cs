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
        public DbSet<UsuarioTurma> usuarioTurmas { get; set; }
        public DbSet<Tarefas> tarefas { get; set; }
        public DbSet<TarefaAluno> tarefaaluno { get; set; }
        public DbSet<Projetos> projetos { get; set; }
        public DbSet<Compromissos> compromissos { get; set; }


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
            modelBuilder.ApplyConfiguration(new UsuarioTurmaConfiguration());
            modelBuilder.ApplyConfiguration(new TarefasConfiguration());
            modelBuilder.ApplyConfiguration(new TarefaAlunoConfiguration());
            modelBuilder.ApplyConfiguration(new ProjetosConfiguration());
            modelBuilder.ApplyConfiguration(new CompromissosConfiguration());
        }
    }
    
}
