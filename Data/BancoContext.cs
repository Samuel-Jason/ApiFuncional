using ApiTesta.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTesta.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options){
            
        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
