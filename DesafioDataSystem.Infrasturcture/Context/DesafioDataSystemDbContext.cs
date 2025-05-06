using DesafioDataSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioDataSystem.Infrasturcture.Context
{
    public class DesafioDataSystemDbContext : DbContext
    { 
        public DesafioDataSystemDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DesafioDataSystemDbContext).Assembly);

            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.ToTable("Tarefas");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Titulo)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(t => t.Descricao)
                      .HasMaxLength(500)
                      .IsRequired(false);

                entity.Property(t => t.DataCriacao)
                      .HasColumnType("datetime");

                entity.Property(t => t.DataConclusao)
                      .HasColumnType("datetime");

                entity.Property(t => t.Status)
                      .HasConversion<int>() // Armazena o enum como string no banco
                      .IsRequired();
            });
        }
    }
}
