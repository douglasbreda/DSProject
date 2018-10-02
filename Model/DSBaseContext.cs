using Microsoft.EntityFrameworkCore;

namespace DSProject.Model
{
    public class DSBaseContext : DbContext
    {
        public virtual DbSet<Integrant> Integrants { get; set; }

        public DSBaseContext(DbContextOptions<DSBaseContext> options)
        :base(options) { }
    }
}