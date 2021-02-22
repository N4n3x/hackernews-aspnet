namespace HN.Application
{
  using System.Linq;
  using HN.Domain;

  public interface IHNContext
  {
    IQueryable<Link> Links { get; }
  }
}