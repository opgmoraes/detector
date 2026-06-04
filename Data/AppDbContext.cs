using Microsoft.EntityFrameworkCore;
using Detector.Entidades;

namespace Detector.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LogRequisicao> LogsRequisicao { get; set; } = null!;
        public DbSet<LogRequisicaoResposta> LogsRequisicaoResposta { get; set; } = null!;
        public DbSet<LogFeedback> LogsFeedback { get; set; } = null!;
        public DbSet<DadoTreinamento> DadosTreinamento { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LogRequisicao>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Input).IsRequired();
            });

            modelBuilder.Entity<LogRequisicaoResposta>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Input).IsRequired();
                e.Property(x => x.Resposta).IsRequired();
            });

            modelBuilder.Entity<LogFeedback>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Input).IsRequired();
                e.Property(x => x.RespostaIA).IsRequired();
                e.Property(x => x.TipoFeedback).IsRequired();
            });

            modelBuilder.Entity<DadoTreinamento>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Text).IsRequired();
                e.Property(x => x.Label).IsRequired();
            });
        }
    }
}
