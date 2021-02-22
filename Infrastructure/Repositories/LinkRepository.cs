using System.Threading.Tasks;
using HN.Domain;

namespace HN.Infrastructure.Repositories
{
  public class LinkRepository : ILinkRepository
  {
    private readonly HNDbContext _context;

    public LinkRepository(HNDbContext context)
    {
      _context = context;
    }

    public async Task AddAsync(Link link)
    {
      await _context.Links.AddAsync(link);
      await _context.SaveChangesAsync();
    }
  }
}