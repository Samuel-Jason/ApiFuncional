using ApiTesta.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTesta.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options){
            
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasKey(c => c.CategoriaId);

            modelBuilder.Entity<Categoria>().
                Property(c => c.Nome).
                    HasMaxLength(100).
                        IsRequired();

            modelBuilder.Entity<Produto>().
                Property(c => c.Nome).
                    HasMaxLength(100).
                      IsRequired();

            modelBuilder.Entity<Produto>().
                Property(c => c.Preco).
                    HasPrecision(12, 2);

            modelBuilder.Entity<Categoria>()
                .HasMany(g => g.Produtos)
                    .WithOne(c => c.Categoria)
                      .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria
                {
                    CategoriaId = 1,
                    Nome = "Material escolar"
                },
                new Categoria
                {
                    CategoriaId = 2,
                    Nome = "Acessórios"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
