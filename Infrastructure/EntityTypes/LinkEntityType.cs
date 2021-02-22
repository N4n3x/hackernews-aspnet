namespace HN.Infrastructure.EntityTypes
{
  using HN.Domain;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class LinkEntityType : IEntityTypeConfiguration<Link>
  {
    public void Configure(EntityTypeBuilder<Link> builder)
    {
      builder.HasKey(o => o.Id);
      builder.Property(o => o.Url).IsRequired().HasMaxLength(250);
      builder.Property(o => o.CreatedAt).IsRequired();
      // builder.HasIndex(o => o.Url).IsUnique();
    }
  }
}