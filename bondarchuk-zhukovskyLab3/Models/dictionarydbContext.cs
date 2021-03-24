using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace bondarchuk_zhukovskyLab3
{
    public partial class dictionarydbContext : DbContext
    {
        public dictionarydbContext()
        {
        }

        public dictionarydbContext(DbContextOptions<dictionarydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DictionaryBook> DictionaryBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=dictionarydb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<DictionaryBook>(entity =>
            {
                entity.ToTable("DictionaryBook");

                entity.Property(e => e.EnglishWord)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RussianWord)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
