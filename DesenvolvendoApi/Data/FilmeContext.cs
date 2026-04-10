using DesenvolvendoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DesenvolvendoApi.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> options) : base(options)
    {       
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Sessao>()
            .HasKey(s => new { s.FilmeId, s.CinemaId });

        builder.Entity<Sessao>()
        .HasOne(s => s.Filme)
        .WithMany(f => f.Sessoes)
        .HasForeignKey(s => s.FilmeId);

        builder.Entity<Sessao>()
        .HasOne(s => s.Cinema)
        .WithMany(c => c.Sessoes)
        .HasForeignKey(s => s.CinemaId);

        builder.Entity<Endereco>()
            .HasOne(e => e.Cinema)
            .WithOne(c => c.Endereco)
            .OnDelete(DeleteBehavior.Restrict);

    }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    
}