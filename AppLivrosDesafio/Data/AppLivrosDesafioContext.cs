using AppLivrosDesafio.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppLivrosDesafio.Data;

public class AppLivrosDesafioContext : IdentityDbContext<AppLivrosUser>
{
    public AppLivrosDesafioContext(DbContextOptions<AppLivrosDesafioContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
       

        builder.ApplyConfiguration(new AppLivrosUserEntityConfiguration());
    }
    public class AppLivrosUserEntityConfiguration : IEntityTypeConfiguration<AppLivrosUser>
    {
        public void Configure(EntityTypeBuilder<AppLivrosUser> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(255);            
        }

    }
}
