using Microsoft.EntityFrameworkCore;

namespace MicroservicesWebAPI.Data
{
    public class MicroservicesWebAPIContext : DbContext
    {
        public MicroservicesWebAPIContext (DbContextOptions<MicroservicesWebAPIContext> options)
            : base(options)
        {
        }

         public DbSet<MicroservicesWebAPI.Customer> Customer { get; set; }

   

        public DbSet<MicroservicesWebAPI.Transaction> Transaction { get; set; }
    }
}
