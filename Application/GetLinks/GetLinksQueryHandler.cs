namespace HN.Application
{
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  using MediatR;

  public class GetLinksQueryHandler : IRequestHandler<GetLinksQuery, LinkDto[]>
  {
    private readonly IHNContext _context;

    public GetLinksQueryHandler(IHNContext context)
    {
      _context = context;
    }

    public Task<LinkDto[]> Handle(GetLinksQuery request, CancellationToken cancellationToken)
    {
      var links = from link in _context.Links
                  orderby link.CreatedAt descending
                  select new LinkDto
                  {
                    Id = link.Id,
                    Url = link.Url,
                    CreatedAt = link.CreatedAt
                  };

      // var links = _context.Links.Select(link => new LinkDto
      // {
      //   Id = link.Id,
      //   Url = link.Url,
      //   CreatedAt = link.CreatedAt
      // }).OrderByDescending(dto => dto.CreatedAt);

      return Task.FromResult(links.ToArray());
    }
  }
}