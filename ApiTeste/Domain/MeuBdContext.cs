using Microsoft.EntityFrameworkCore;

namespace ApiTeste.Domain
{
    public class MeuDbContext : DbContext
    {
        public  MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
        {
            
        } 

        public DbSet<UserModel> pessoa { get; set; }
    }

}

