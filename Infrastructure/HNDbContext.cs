namespace HN.Infrastructure
{
  using System.Linq;
  using HN.Application;
  using HN.Domain;
  using Microsoft.EntityFrameworkCore;

  public class HNDbContext : DbContext, IHNContext
  {
    public DbSet<Link> Links { get; set; }

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
