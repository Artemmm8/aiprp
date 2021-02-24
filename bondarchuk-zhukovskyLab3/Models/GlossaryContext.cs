using Microsoft.EntityFrameworkCore;

namespace bondarchuk_zhukovskyLab3.Models
{
    public class GlossaryContext : DbContext
    {
        public DbSet<Glossary> Glossaries { get; set; }

        public GlossaryContext(DbContextOptions<GlossaryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}