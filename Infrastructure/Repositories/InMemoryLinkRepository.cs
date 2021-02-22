namespace HN.Infrastructure.Repositories
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using HN.Domain;

  public class InMemoryLinkRepository : ILinkRepository
  {
    private readonly List<Link> _links = new List<Link>();

    public Task AddAsync(Link link)
    {
      _links.Add(link);
      return Task.CompletedTask;
    }
  }
}