namespace HN.Infrastructure
{
  using System.Linq;
  using HN.Application;
  using HN.Domain;
  using Microsoft.EntityFrameworkCore;

  public class HNDbContext : DbContext, IHNContext
  {
    public DbSet<Link> Links { get; set; }

    // - Exposer un DbSet<Comment>
    // - Définir le IEntityTypeConfiguration<Comment>
    // - Générer la migration correspondante :
    //    - Dans le projet /Infrastructure/
    //    - dotnet ef migrations add CreateCommentEntity
    // - Exposer le IQueryable<Comment> dans IHNContext
    // - Implémenter le ICommentRepository dans /Repositories/

    IQueryable<Link> IHNContext.Links => Links;

    // IQueryable<Link> IHNContext.Links
    // {
    //   get { return Links; }
    // }

    public HNDbContext() : base()
    {

    }

    public HNDbContext(DbContextOptions<HNDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      //   modelBuilder.ApplyConfiguration(new LinkEntityType());
      modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlite("Data Source=:memory:");
      }
    }
  }
}
