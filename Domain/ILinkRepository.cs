namespace HN.Domain
{
  using System.Threading.Tasks;

  public interface ILinkRepository
  {
    Task AddAsync(Link link);
  }
}